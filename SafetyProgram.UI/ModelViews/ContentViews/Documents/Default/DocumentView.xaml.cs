using System.Windows.Controls;
using SafetyProgram.Base;

namespace SafetyProgram.UI.ModelViews.Documents.Default
{
    /// <summary>
    /// Interaction logic for CoshhDocumentView.xaml
    /// </summary>
    internal sealed partial class DocumentView : UserControl
    {
        public DocumentView(IDocumentViewModel viewModel)
        {
            Helpers.NullCheck(viewModel);

            this.DataContext = viewModel;

            InitializeComponent();
        }
    }
}
