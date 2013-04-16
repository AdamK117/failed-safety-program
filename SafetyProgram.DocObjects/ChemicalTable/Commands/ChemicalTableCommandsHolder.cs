using System.Collections.Generic;
using System.Windows.Input;

namespace SafetyProgram.DocObjects.ChemicalTable.Commands
{
    public class ChemicalTableCommandsHolder
    {
        private readonly ChemicalTable table;

        private readonly AddNewChemicalICommand addNewChemicalICommand;
        private readonly DeleteSelectedICommand deleteSelectedICommand;
        private readonly DeleteTableICommand deleteTableICommand;
        private readonly CopySelectedICommand copySelectedICommand;
        private readonly PasteChemicalsICommand pasteChemicalsICommand;

        private readonly List<InputBinding> hotkeys;

        public ChemicalTableCommandsHolder(ChemicalTable table)
        {
            this.table = table;

            addNewChemicalICommand = new AddNewChemicalICommand(table);
            deleteSelectedICommand = new DeleteSelectedICommand(table);
            deleteTableICommand = new DeleteTableICommand(table);
            copySelectedICommand = new CopySelectedICommand(table);
            pasteChemicalsICommand = new PasteChemicalsICommand(table);

            hotkeys = setHotkeys();
        }

        private List<InputBinding> setHotkeys()
        {
            return new List<InputBinding>()
            {
                new InputBinding(
                    DeleteSelected,
                    new KeyGesture(Key.Delete)
                ),
                new InputBinding(
                    CopySelected,
                    new KeyGesture(Key.C, ModifierKeys.Control)
                ),
                new InputBinding(
                    PasteChemicals,
                    new KeyGesture(Key.V, ModifierKeys.Control)
                )
            };
        }

        /// <summary>
        /// Gets an ICommand that adds a random chemical to the ChemicalTable.
        /// </summary>
        public AddNewChemicalICommand AddNewChemical { get { return addNewChemicalICommand; } }

        /// <summary>
        /// Gets an ICommand that deletes chemicals selected within the ChemicalTable.
        /// </summary>
        public DeleteSelectedICommand DeleteSelected { get { return deleteSelectedICommand; } }

        /// <summary>
        /// Gets an ICommand that flags the ChemicalTable for deletion.
        /// </summary>
        public DeleteTableICommand DeleteTable { get { return deleteTableICommand; } }

        /// <summary>
        /// Gets an ICommand that copies the selected chemicals within the ChemicalTable
        /// </summary>
        public CopySelectedICommand CopySelected { get { return copySelectedICommand; } }

        /// <summary>
        /// Gets an ICommand that attempts to paste chemicals into the ChemicalTable.
        /// </summary>
        public PasteChemicalsICommand PasteChemicals { get { return pasteChemicalsICommand; } }

        /// <summary>
        /// Gets the hotkeys associated with the ChemicalTable.
        /// </summary>
        public List<InputBinding> Hotkeys { get { return hotkeys; } }
    }
}
