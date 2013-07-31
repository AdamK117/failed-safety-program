using SafetyProgram.Base;
using SafetyProgram.Core.Commands;

namespace SafetyProgram.UI.Document
{
    public sealed class DocumentContextMenuViewModel : IDocumentContextMenuViewModel
    {
        public DocumentContextMenuViewModel(IDocumentICommands commands)
        {
            Helpers.NullCheck(commands);

            this.commands = commands;
        }

        private readonly IDocumentICommands commands;
        public IDocumentICommands Commands
        {
            get { return commands; }
        }
    }
}
