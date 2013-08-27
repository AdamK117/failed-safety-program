using System.Collections.Generic;
using System.Windows.Input;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Core.Models;

namespace SafetyProgram.Core.Commands
{
    public sealed class DocumentICommands : IDocumentICommands
    {
        public DocumentICommands(IDocument document, ICommandInvoker commandInvoker)
        {
            deleteIDocumentObject 
                = new DeleteIDocumentObjectICommand(document, commandInvoker);

            insertChemicalTable = null;

            hotkeys = setHotkeys();
        }

        private List<InputBinding> setHotkeys()
        {
            return new List<InputBinding>()
            {
                //Delete Selection: DEL
                new InputBinding(
                    DeleteIDocObject,
                    new KeyGesture(Key.Delete)
                )
            };
        }
        private readonly ICommand deleteIDocumentObject;
        public ICommand DeleteIDocObject
        {
            get { return deleteIDocumentObject; }
        }

        private readonly ICommand insertChemicalTable;
        public ICommand InsertChemicalTable
        {
            get { return insertChemicalTable; }
        }

        private readonly List<InputBinding> hotkeys;
        public List<InputBinding> Hotkeys
        {
            get { return hotkeys; }
        }
    }
}
