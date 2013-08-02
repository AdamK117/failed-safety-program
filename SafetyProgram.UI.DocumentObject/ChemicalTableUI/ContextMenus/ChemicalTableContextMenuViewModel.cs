using SafetyProgram.Base;

namespace SafetyProgram.UI.DocumentObject.ChemicalTableUI
{
    /// <summary>
    /// Defines an implementation of an IChemicalTableContextMenuViewModel.
    /// </summary>
    internal sealed class ChemicalTableContextMenuViewModel : IChemicalTableContextMenuViewModel
    {
        public ChemicalTableContextMenuViewModel(IChemicalTableCommands tableCommands)
        {
            Helpers.NullCheck(tableCommands);

            this.tableCommands = tableCommands;
        }

        private readonly IChemicalTableCommands tableCommands;

        /// <summary>
        /// Get a group of commands that act on the chemicaltable.
        /// </summary>
        public IChemicalTableCommands Commands
        {
            get { return tableCommands; }
        }
    }
}
