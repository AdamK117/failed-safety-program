using System.Windows.Controls;

using SafetyProgram.Base.Interfaces;
using SafetyProgram.DocumentObjects.ChemicalTableNs.Commands;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs.ContextMenus
{
    internal sealed class ChemicalTableContextMenu : IContextMenu
    {
        private readonly ContextMenu view;

        public ChemicalTableContextMenu(ChemicalTable table)
        {
            Commands = table.Commands;
            view = new ChemicalTableContextMenuView(this);
        }

        public ContextMenu View { get { return view; } }

        Control IViewable.View { get { return view; } }

        public IChemicalTableCommands Commands { get; private set; }
    }
}
