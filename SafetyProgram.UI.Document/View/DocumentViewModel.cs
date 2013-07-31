using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using SafetyProgram.Core.Commands;
using SafetyProgram.Core.Models;

namespace SafetyProgram.UI.Document.View
{
    public sealed class DocumentViewModel : IDocumentViewModel
    {
        private readonly IDocument document;

        public DocumentViewModel(IDocument document, IDocumentICommands documentCommands)
        {
            this.document = document;
            this.hotkeys = documentCommands.Hotkeys;

            contextMenu = new DocumentContextMenuView(
                new DocumentContextMenuViewModel(
                    documentCommands
                )
            );
        }

        public IFormat Format
        {
            get { return document.Format; }
        }

        private readonly ContextMenu contextMenu;

        public System.Windows.Controls.ContextMenu ContextMenu
        {
            get { return contextMenu; }
        }

        private List<InputBinding> hotkeys;

        public List<System.Windows.Input.InputBinding> Hotkeys
        {
            get { return hotkeys; }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
    }
}
