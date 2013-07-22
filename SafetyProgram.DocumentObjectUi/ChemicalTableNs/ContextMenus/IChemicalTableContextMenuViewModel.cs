using SafetyProgram.DocumentObjectUi.ChemicalTableNs.Commands;

namespace SafetyProgram.DocumentObjectUi.ChemicalTableNs.ContextMenus
{
    /// <summary>
    /// Defines an interface for the view model of the chemical tables context menu.
    /// </summary>
    internal interface IChemicalTableContextMenuViewModel
    {
        /// <summary>
        /// Get the commands associated with the chemical table.
        /// </summary>
        IChemicalTableCommands Commands { get; }
    }
}
