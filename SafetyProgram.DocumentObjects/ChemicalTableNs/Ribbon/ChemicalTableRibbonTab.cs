using System;
using System.Windows.Controls;
using Fluent;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.DocumentObjects.ChemicalTableNs.Commands;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs.Ribbon
{
    internal sealed class ChemicalTableRibbonTab : IChemicalTableRibbonTab
    {
        public ChemicalTableRibbonTab(
            IChemicalTable table,
            Func<IChemicalTableRibbonTab, RibbonTabItem> viewConstructor
            )
        {
            if (table != null && viewConstructor != null)
            {
                this.table = table;
                view = viewConstructor(this);
            }
            else throw new ArgumentNullException();
        }

        private readonly IChemicalTable table;
        public IChemicalTableCommands Commands
        {
            get
            {
                return table.Commands;
            }
        }

        private readonly RibbonTabItem view;
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
