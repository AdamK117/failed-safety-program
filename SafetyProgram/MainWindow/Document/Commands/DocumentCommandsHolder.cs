using System.Collections.Generic;
using System.Windows.Input;

namespace SafetyProgram.MainWindow.Document.Commands
{
    public class DocumentCommandsHolder
    {
        private readonly CoshhDocument document;

        private readonly InsertChemicalTableICommand insertChemicalTable;
        private readonly DeleteDocObjectICommand deleteDocObject;

        private readonly List<InputBinding> hotkeys;

        public DocumentCommandsHolder(CoshhDocument document)
        {
            this.document = document;

            insertChemicalTable = new InsertChemicalTableICommand(document);
            deleteDocObject = new DeleteDocObjectICommand(document);

            hotkeys = setHotkeys();
        }

        private List<InputBinding> setHotkeys()
        {
            return new List<InputBinding>()
            {
                //Document keybinds go here
                new InputBinding(
                    DeleteDocObject,
                    new KeyGesture(Key.Delete)
                )
            };
        }

        public InsertChemicalTableICommand InsertChemicalTable { get { return insertChemicalTable; } }
        public DeleteDocObjectICommand DeleteDocObject { get { return deleteDocObject; } }

        public List<InputBinding> Hotkeys { get { return hotkeys; } }
    }
}
