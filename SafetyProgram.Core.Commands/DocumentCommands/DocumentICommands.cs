using System.Collections.Generic;
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

            this.Hotkeys = setHotkeys();
        }

        private List<InputBinding> setHotkeys()
        {
            return new List<InputBinding>()
            {
                //Delete Selection: DEL
                new InputBinding(
                    DeleteIDocObject,
                    new KeyGesture(Key.Delete)),
                new InputBinding(
                    InsertChemicalTable,
                    new KeyGesture(Key.Insert))
            };
        }

        public ICommand DeleteIDocObject { get; private set; }

        public ICommand InsertChemicalTable { get; private set; }

        public List<InputBinding> Hotkeys { get; private set; }
    }
}
