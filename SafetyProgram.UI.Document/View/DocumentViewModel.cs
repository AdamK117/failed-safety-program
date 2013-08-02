using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using SafetyProgram.Core.Commands;
using SafetyProgram.Core.Models;
using SafetyProgram.UI.DocumentObject;

namespace SafetyProgram.UI.Document.View
{
    /// <summary>
    /// Defines a standard implementation of an IDocumentViewmodel.
    /// </summary>
    public sealed class DocumentViewModel : IDocumentViewModel
    {
        private readonly IDocument document;

        /// <summary>
        /// Construct an instance of a document viewmodel.
        /// </summary>
        /// <param name="document">The underlying document model.</param>
        /// <param name="documentCommands">Commands that act on the document model.</param>
        public DocumentViewModel(ObservableCollection<IDocumentObjectUiController> documentObjects, IDocumentICommands documentCommands)
        {
            this.documentObjects = documentObjects;
            this.hotkeys = documentCommands.Hotkeys;

            contextMenu = new DocumentContextMenuView(
                new DocumentContextMenuViewModel(
                    documentCommands
                )
            );
        }

        /// <summary>
        /// Get the format of the document.
        /// </summary>
        public IFormat Format
        {
            get { return document.Format; }
        }

        private readonly ContextMenu contextMenu;

        /// <summary>
        /// Get the contextmenu for the document view.
        /// </summary>
        public ContextMenu ContextMenu
        {
            get { return contextMenu; }
        }

        private List<InputBinding> hotkeys;

        /// <summary>
        /// Get the hotkeys associated with the document.
        /// </summary>
        public List<InputBinding> Hotkeys
        {
            get { return hotkeys; }
        }

        private readonly ObservableCollection<IDocumentObjectUiController> documentObjects;

        /// <summary>
        /// Get the documentobjects in the document.
        /// </summary>
        public ObservableCollection<IDocumentObjectUiController> DocumentObjects
        {
            get { return documentObjects; }
        }

        /// <summary>
        /// Occurs when a property on the viewmodel changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;        
    }
}
