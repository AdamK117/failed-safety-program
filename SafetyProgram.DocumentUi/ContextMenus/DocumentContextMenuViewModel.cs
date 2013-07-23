using SafetyProgram.Base;
using SafetyProgram.DocumentUi.Commands;

namespace SafetyProgram.DocumentUi.ContextMenus
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
