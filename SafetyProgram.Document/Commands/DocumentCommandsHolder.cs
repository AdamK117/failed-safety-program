using System.Collections.Generic;
using System.Windows.Input;

namespace SafetyProgram.Document.Commands
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
                //Delete Selection: DEL
                new InputBinding(
                    DeleteDocObject,
                    new KeyGesture(Key.Delete)
                )
            };
        }

        /// <summary>
        /// Gets an ICommand that inserts a ChemicalTable into the CoshhDocument
        /// </summary>
        public InsertChemicalTableICommand InsertChemicalTable { get { return insertChemicalTable; } }

        /// <summary>
        /// Gets an ICommand that deletes the currently selected DocObject in the CoshhDocument
        /// </summary>
        public DeleteDocObjectICommand DeleteDocObject { get { return deleteDocObject; } }

        /// <summary>
        /// Gets a list of InputBindings used by the CoshhDocument
        /// </summary>
        public List<InputBinding> Hotkeys { get { return hotkeys; } }
    }
}
