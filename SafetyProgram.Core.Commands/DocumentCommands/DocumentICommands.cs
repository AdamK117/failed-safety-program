using System.Windows.Input;
using SafetyProgram.Base;
using SafetyProgram.Core.Commands.DocumentCommands;
using SafetyProgram.Core.Models;

namespace SafetyProgram.Core.Commands
{
    public sealed class DocumentICommands : IDocumentICommands
    {
        public DocumentICommands(IDocument document, ICommandInvoker commandInvoker)
        {
            this.DeleteIDocObject
                = new DeleteIDocumentObjectICommand(document, commandInvoker);

            this.InsertChemicalTable = new InsertChemicalTableICommand(document, commandInvoker);
        }

        public ICommand DeleteIDocObject { get; private set; }

        public ICommand InsertChemicalTable { get; private set; }
    }
}
