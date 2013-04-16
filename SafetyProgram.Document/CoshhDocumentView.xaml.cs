using System.Windows.Controls;
using System.Windows.Input;
using SafetyProgram.DocObjects;

namespace SafetyProgram.Document
{
    /// <summary>
    /// Interaction logic for CoshhDocumentView.xaml
    /// </summary>
    public partial class CoshhDocumentView : UserControl
    {
        private readonly CoshhDocument viewModel;

        public CoshhDocumentView(CoshhDocument viewModel)
        {
            this.viewModel = viewModel;
            DataContext = viewModel;
            InitializeComponent();
        }

        //If the document (i.e. not a docObject) is clicked
        private void DocumentClicked(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is Control)
            {
                Control control = (Control)e.OriginalSource;
                switch (control.Name)
                {
                    case "DocumentWrapper":
                        viewModel.ClearSelection();
                        break;
                    case "DocumentHolder":
                        viewModel.ClearSelection();
                        break;
                    default:
                        break;
                }
            }            
        }

        //If a docobject is clicked
        private void DocObjectClicked(object sender, MouseButtonEventArgs e)
        {
            ContentControl contentControl = (ContentControl)sender;
            DocObject docObject = (DocObject)contentControl.DataContext;
            viewModel.Select(docObject);
        }
    }
}
