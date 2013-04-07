using System.Windows.Controls;

namespace SafetyProgram.MainWindow.Document.ContextMenus
{
    public class DocumentContextMenu
    {
        private readonly CoshhDocument document;
        private readonly ContextMenu view;

        public DocumentContextMenu(CoshhDocument document)
        {
            this.document = document;
            view = new DocumentContextMenuView(this);
        }

        public ContextMenu View
        {
            get { return view; }
        }
    }
}
