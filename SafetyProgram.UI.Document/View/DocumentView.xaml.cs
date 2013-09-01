using System.Windows.Controls;
using System.Windows.Input;
using SafetyProgram.Base;

namespace SafetyProgram.UI.Document
{
    /// <summary>
    /// Interaction logic for CoshhDocumentView.xaml
    /// </summary>
    public sealed partial class DocumentView : UserControl
    {
        private readonly IDocumentViewModel viewModel;

        public DocumentView(IDocumentViewModel viewModel)
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
                        viewModel.Selection.Clear();
                        break;
                    case "DocumentHolder":
                        viewModel.Selection.Clear();
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

        }
    }
}
