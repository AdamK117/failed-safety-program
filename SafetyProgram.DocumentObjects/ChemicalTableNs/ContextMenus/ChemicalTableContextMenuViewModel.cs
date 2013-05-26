using SafetyProgram.Base;
using SafetyProgram.DocumentObjects.ChemicalTableNs.Commands;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs.ContextMenus
{
    internal sealed class ChemicalTableContextMenuViewModel : IChemicalTableContextMenuViewModel
    {
        public ChemicalTableContextMenuViewModel(IChemicalTableCommands tableCommands)
        {
            Helpers.NullCheck(tableCommands);

            this.tableCommands = tableCommands;
        }

        private readonly IChemicalTableCommands tableCommands;
        public IChemicalTableCommands Commands
        {
            get { return tableCommands; }
        }
    }
}
