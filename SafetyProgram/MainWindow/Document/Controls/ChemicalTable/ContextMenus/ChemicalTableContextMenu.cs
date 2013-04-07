using System.Windows.Controls;
using SafetyProgram.MainWindow.Document.Controls.ChemicalTable.Commands;

namespace SafetyProgram.MainWindow.Document.Controls.ChemicalTable.ContextMenus
{
    public class ChemicalTableContextMenu : DocObjectContextMenu
    {
        private readonly ChemicalTable table;
        private readonly ContextMenu view;

        public ChemicalTableContextMenu(ChemicalTable table)
        {
            this.table = table;
            view = new ChemicalTableContextMenuView(this);
        }

        public override ContextMenu View
        {
            get { return view; }
        }

        public ChemicalTableCommandsHolder Commands
        {
            get { return table.Commands; }
        }
    }
}
