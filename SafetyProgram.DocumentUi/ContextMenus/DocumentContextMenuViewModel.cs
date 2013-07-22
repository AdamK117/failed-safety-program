using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SafetyProgram.DocumentUi.Commands;

namespace SafetyProgram.DocumentUi.ContextMenus
{
    public sealed class DocumentContextMenuViewModel : IDocumentContextMenuViewModel
    {
        public DocumentContextMenuViewModel(IDocumentICommands commands)
        {
            if (commands == null) throw new ArgumentNullException();
            else this.commands = commands;
        }

        private readonly IDocumentICommands commands;
        public IDocumentICommands Commands
        {
            get { return commands; }
        }
    }
}
