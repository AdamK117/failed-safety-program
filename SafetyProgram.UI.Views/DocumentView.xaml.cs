using SafetyProgram.Base;
using SafetyProgram.UI.ViewModels;
using System.Windows.Controls;

namespace SafetyProgram.UI.Views
{
    /// <summary>
    /// Interaction logic for CoshhDocumentView.xaml
    /// </summary>
    public sealed partial class DocumentView : UserControl
    {
        public DocumentView(IDocumentViewModel viewModel)
        {
            Helpers.NullCheck(viewModel);

            this.DataContext = viewModel;

            InitializeComponent();
        }
    }
}
