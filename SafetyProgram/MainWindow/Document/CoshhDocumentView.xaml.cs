using System.Windows.Controls;
using System.Windows.Input;
using SafetyProgram.MainWindow.Document.Controls;

namespace SafetyProgram.MainWindow.Document
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

        private void DocumentClicked(object sender, MouseButtonEventArgs e)
        {
            viewModel.Selected = null;
        }

        private void DocObjectClicked(object sender, MouseButtonEventArgs e)
        {
            ContentControl contentControl = (ContentControl)sender;
            if (viewModel.Selected != (DocObject)contentControl.DataContext)
            {
                viewModel.Selected = (DocObject)contentControl.DataContext;
            }            
        }
    }
}
