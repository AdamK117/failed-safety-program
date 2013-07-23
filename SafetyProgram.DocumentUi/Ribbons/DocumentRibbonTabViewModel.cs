using SafetyProgram.Base;
using SafetyProgram.DocumentUi.Commands;

namespace SafetyProgram.DocumentUi.Ribbons
{
    public sealed class DocumentRibbonTabViewModel : IDocumentRibbonTabViewModel
    {
        public DocumentRibbonTabViewModel(IDocumentICommands commands)
        {
            Helpers.NullCheck(commands);
            
            this.commands = commands;
        }

        private readonly IDocumentICommands commands;
        public Commands.IDocumentICommands Commands
        {
            get { return commands; }
        }
    }
}
