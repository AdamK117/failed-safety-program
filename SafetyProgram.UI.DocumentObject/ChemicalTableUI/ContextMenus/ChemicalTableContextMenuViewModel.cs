using SafetyProgram.Base;

namespace SafetyProgram.UI.DocumentObject.ChemicalTableUI
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
