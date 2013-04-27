using System.Windows.Controls;
using Fluent;

using SafetyProgram.Base.Interfaces;
using SafetyProgram.DocumentObjects.ChemicalTable.Commands;

namespace SafetyProgram.DocumentObjects.ChemicalTable.Ribbon
{
    internal sealed class ChemicalTableRibbonTab : IRibbonTabItem
    {
        private readonly ChemicalTableRibbonView view;

        public ChemicalTableRibbonTab(ChemicalTable table)
        {
            Commands = table.Commands;
            view = new ChemicalTableRibbonView(this);
        }

        public IChemicalTableCommands Commands { get; private set; }

        public RibbonTabItem View { get { return view; } }

        Control IViewable.View { get { return view; } }
    }
}
