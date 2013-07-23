using System;
using SafetyProgram.DocumentObjectUi.ChemicalTableNs.Commands;

namespace SafetyProgram.DocumentObjectUi.ChemicalTableNs.ContextMenus
{
    public sealed class ChemicalTableContextMenuViewModel : IChemicalTableContextMenuViewModel
    {
        public ChemicalTableContextMenuViewModel(IChemicalTableCommands commands)
        { }

        public Commands.IChemicalTableCommands Commands
        {
            get { throw new NotImplementedException(); }
        }
    }
}
