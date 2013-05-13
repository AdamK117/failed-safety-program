using System.Collections.Generic;
using System.Windows.Input;
using SafetyProgram.DocumentObjects;

namespace SafetyProgram.Document.Commands
{
    internal sealed class DocumentICommands : IDocumentICommands
    {
        public DocumentICommands(ICoshhDocument document)
        {
            InsertChemicalTable = new InsertIDocumentObjectICom(
                document, 
                () => DefaultDocumentObjects.ChemicalTable(document.AppConfiguration)
                );
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
