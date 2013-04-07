using System.Collections.Generic;
using System.Windows.Input;
namespace SafetyProgram.MainWindow.Document.Controls.ChemicalTable.Commands
{
    public class ChemicalTableCommandsHolder
    {
        private readonly ChemicalTable table;

        private readonly AddNewChemicalICommand addNewChemicalICommand;
        private readonly DeleteSelectedICommand deleteSelectedICommand;
        private readonly DeleteTableICommand deleteTableICommand;

        private readonly List<InputBinding> hotkeys;

        public ChemicalTableCommandsHolder(ChemicalTable table)
        {
            this.table = table;

            addNewChemicalICommand = new AddNewChemicalICommand(table);
            deleteSelectedICommand = new DeleteSelectedICommand(table);
            deleteTableICommand = new DeleteTableICommand(table);

            hotkeys = setHotkeys();
        }

        private List<InputBinding> setHotkeys()
        {
            return new List<InputBinding>()
            {
                new InputBinding(
                    DeleteSelected,
                    new KeyGesture(Key.Delete)
                )
            };
        }

        public AddNewChemicalICommand AddNewChemical { get { return addNewChemicalICommand; } }
        public DeleteSelectedICommand DeleteSelected { get { return deleteSelectedICommand; } }
        public DeleteTableICommand DeleteTable { get { return deleteTableICommand; } }

        public List<InputBinding> Hotkeys { get { return hotkeys; } }
    }
}
