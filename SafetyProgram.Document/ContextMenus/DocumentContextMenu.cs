using System;
using System.Windows.Controls;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Document.Commands;

namespace SafetyProgram.Document.ContextMenus
{
    internal sealed class DocumentContextMenu : IContextMenu
    {
        private readonly IDocumentICommands documentCommands;
        private readonly ContextMenu view;

        /// <summary>
        /// Constructs an instance of a DocumentContextMenu for the CoshhDocument
        /// </summary>
        /// <param name="documentCommands"></param>
        public DocumentContextMenu(
            IDocumentICommands documentCommands, 
            Func<DocumentContextMenu, ContextMenu> viewConstructor
            )
        {
            if (documentCommands != null && viewConstructor != null)
            {
                this.documentCommands = documentCommands;
                view = viewConstructor(this);
            }
            else throw new ArgumentNullException();
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
        public IDocumentICommands DocumentCommands
        {
            get { return documentCommands; }
        }

        
    }
}
