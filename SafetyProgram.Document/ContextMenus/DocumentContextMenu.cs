using System.Windows.Controls;
using SafetyProgram.Document.Commands;

namespace SafetyProgram.Document.ContextMenus
{
    public class DocumentContextMenu
    {
        private readonly DocumentCommandsHolder documentCommands;
        private readonly ContextMenu view;

        /// <summary>
        /// Constructs an instance of a DocumentContextMenu for the CoshhDocument
        /// </summary>
        /// <param name="documentCommands"></param>
        public DocumentContextMenu(DocumentCommandsHolder documentCommands)
        {
            this.documentCommands = documentCommands;

            view = new DocumentContextMenuView(this);
        }

        /// <summary>
        /// Gets the ContextMenu view
        /// </summary>
        public ContextMenu View
        {
            get { return view; }
        }

        /// <summary>
        /// Gets the CoshhDocument's DocumentCommandsHolder
        /// </summary>
        public DocumentCommandsHolder DocumentCommands
        {
            get { return documentCommands; }
        }
    }
}
