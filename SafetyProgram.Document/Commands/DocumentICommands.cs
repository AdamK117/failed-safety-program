using System.Collections.Generic;
using System.Windows.Input;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.DocumentObjects.ChemicalTableNs;

namespace SafetyProgram.Document.Commands
{
    public sealed class DocumentICommands : IDocumentICommands
    {
        public DocumentICommands(IDocument document)
        {
            //InsertChemicalTable = new InsertIDocumentObjectICom(document, () => new ChemicalTable());
            DeleteIDocObject = new DeleteIDocObjectICom(document);

            Hotkeys = setHotkeys();
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

        public List<InputBinding> Hotkeys
        {
            get;
            private set;
        }

        public ICommand DeleteIDocObject
        {
            get;
            private set;
        }

        public ICommand InsertChemicalTable
        {
            get;
            private set;
        }
    }
}
