using Fluent;
using SafetyProgram.DocObjects.ChemicalTable.Commands;

namespace SafetyProgram.DocObjects.ChemicalTable.Ribbon
{
    public class ChemicalTableRibbonTab : IDocObjectRibbonTab
    {
        private readonly ChemicalTable table;
        private readonly ChemicalTableCommandsHolder commands;
        private readonly ChemicalTableRibbonView view;

        public ChemicalTableRibbonTab(ChemicalTable table)
        {
            this.table = table;
            commands = table.Commands;
            view = new ChemicalTableRibbonView(this);
        }

        public ChemicalTableCommandsHolder Commands
        {
            get
            {
                return commands; 
            }
        }

        public RibbonTabItem View
        {
            get 
            { 
                return view ; 
            }
        }
    }
}
