using System.Windows.Controls;
using Fluent;

using SafetyProgram.Base.Interfaces;
using SafetyProgram.DocumentObjects.ChemicalTableNs.Commands;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs.Ribbon
{
    internal sealed class ChemicalTableRibbonTab : IRibbonTabItem
    {
        public ChemicalTableRibbonTab(ChemicalTable table)
        {
            commands = table.Commands;
            view = new ChemicalTableRibbonView(this);
        }

        private readonly IChemicalTableCommands commands;
        public IChemicalTableCommands Commands
        {
            get
            {
                return commands;
            }
        }

        private readonly ChemicalTableRibbonView view;
        public RibbonTabItem View 
        { 
            get 
            { 
                return view; 
            } 
        }

        Control IViewable.View 
        {
            get 
            { 
                return view; 
            } 
        }
    }
}
