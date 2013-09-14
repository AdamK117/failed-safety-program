using System.Windows.Controls;
using SafetyProgram.Base;

namespace SafetyProgram.UI.Views.ModelViews.DocumentViews
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
