using Fluent;
using SafetyProgram.MainWindow.Document.Controls.ChemicalTable.Commands;

namespace SafetyProgram.MainWindow.Document.Controls.ChemicalTable.Ribbon
{
    public class ChemicalTableRibbon : DocObjectRibbon
    {
        private readonly ChemicalTable table;

        private AddNewChemicalICommand testAddCommand;
        private DeleteSelectedICommand testDeleteCommand;
        private DeleteTableICommand testDeleteTableCommand;

        public ChemicalTableRibbon(ChemicalTable table)
        {
            this.table = table;
            testAddCommand = new AddNewChemicalICommand(table);
            testDeleteCommand = new DeleteSelectedICommand(table);
            testDeleteTableCommand = new DeleteTableICommand(table);
        }

        public override RibbonTabItem View
        {
            get { return new ChemicalTableRibbonView(this); }
        }

        public AddNewChemicalICommand AddIt
        {
            get { return testAddCommand; }
        }

        public DeleteSelectedICommand DeleteIt
        {
            get { return testDeleteCommand; }
        }

        public DeleteTableICommand DeleteTable
        {
            get { return testDeleteTableCommand; }
        }
    }
}
