using System.Windows.Controls;

namespace SafetyProgram.Document.ContextMenus
{
    /// <summary>
    /// Interaction logic for DocumentContextMenuView.xaml
    /// </summary>
    public sealed partial class DocumentContextMenuView : ContextMenu
    {
        public DocumentContextMenuView(IDocumentContextMenuViewModel viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
