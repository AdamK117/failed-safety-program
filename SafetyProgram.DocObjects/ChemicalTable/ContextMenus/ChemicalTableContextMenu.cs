using System.Windows.Controls;
using SafetyProgram.DocObjects.ChemicalTable.Commands;

namespace SafetyProgram.DocObjects.ChemicalTable.ContextMenus
{
    public class ChemicalTableContextMenu : IDocObjectContextMenu
    {
        private readonly ChemicalTable table;
        private readonly ContextMenu view;

        public ChemicalTableContextMenu(ChemicalTable table)
        {
            this.table = table;
            view = new ChemicalTableContextMenuView(this);
        }

        public ContextMenu View
        {
            get 
            { 
                return view;
            }
        }

        public ChemicalTableCommandsHolder Commands
        {
            get { return table.Commands; }
        }
    }
}
