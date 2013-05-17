using System;
using System.Windows.Controls;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.DocumentObjects.ChemicalTableNs.Commands;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs.ContextMenus
{
    internal sealed class ChemicalTableContextMenu : IChemicalTableContextMenu
    {
        public ChemicalTableContextMenu(
            IChemicalTable chemicalTable,
            Func<IChemicalTableContextMenu, ContextMenu> viewConstructor
            )
        {
            if (chemicalTable != null && viewConstructor != null)
            {
                this.chemicalTable = chemicalTable;
                this.view = viewConstructor(this);
            }
            else throw new ArgumentNullException();            
        }

        private readonly ContextMenu view;
        public ContextMenu View 
        { 
            get { return view; } 
        }
        Control IViewable.View 
        { 
            get { return view; } 
        }

        private readonly IChemicalTable chemicalTable;
        public IChemicalTableCommands Commands
        {
            get
            {
                return chemicalTable.Commands;
            }
        }
    }
}
