using System.Windows.Controls;
using SafetyProgram.Document.Commands;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Document.ContextMenus
{
    public sealed class DocumentContextMenu : IContextMenu
    {
        private readonly DocumentICommands documentCommands;
        private readonly ContextMenu view;

        /// <summary>
        /// Constructs an instance of a DocumentContextMenu for the CoshhDocument
        /// </summary>
        /// <param name="documentCommands"></param>
        public DocumentContextMenu(DocumentICommands documentCommands)
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

        Control IViewable.View
        {
            get 
            {
                return view; 
            }
        }

        /// <summary>
        /// Gets the CoshhDocument's DocumentCommandsHolder
        /// </summary>
        public DocumentICommands DocumentCommands
        {
            get { return documentCommands; }
        }

        
    }
}
