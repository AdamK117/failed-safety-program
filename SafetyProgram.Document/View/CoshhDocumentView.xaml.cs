using System.Windows.Controls;
using System.Windows.Input;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Document
{
    /// <summary>
    /// Interaction logic for CoshhDocumentView.xaml
    /// </summary>
    public sealed partial class CoshhDocumentView : UserControl
    {
        private readonly ICoshhDocumentViewModel viewModel;

        public CoshhDocumentView(ICoshhDocumentViewModel viewModel)
        {
            Helpers.NullCheck(viewModel);

            this.viewModel = viewModel;
            this.DataContext = viewModel;

            InitializeComponent();
            this.InputBindings.AddRange(viewModel.Hotkeys);
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
                        viewModel.Body.DeSelectAll();
                        break;
                    case "DocumentHolder":
                        viewModel.Body.DeSelectAll();
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
            IDocumentObject docObject = (IDocumentObject)contentControl.DataContext;
            viewModel.Body.Select(docObject);
        }
    }
}
