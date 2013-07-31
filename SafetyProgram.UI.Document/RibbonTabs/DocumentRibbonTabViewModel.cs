using SafetyProgram.Base;
using SafetyProgram.Core.Commands;

namespace SafetyProgram.Document.Ribbons
{
    public sealed class DocumentRibbonTabViewModel : IDocumentRibbonTabViewModel
    {
        public DocumentRibbonTabViewModel(IDocumentICommands commands)
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
