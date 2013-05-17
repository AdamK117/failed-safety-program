using System.Windows.Controls;

namespace SafetyProgram.Document.ContextMenus
{
    /// <summary>
    /// Interaction logic for DocumentContextMenuView.xaml
    /// </summary>
    internal sealed partial class DocumentContextMenuView : ContextMenu
    {
        public DocumentContextMenuView(IDocumentContextMenu viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
