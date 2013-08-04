using SafetyProgram.Core.Commands;
namespace SafetyProgram.UI.DocumentObject.ChemicalTableUI
{
    /// <summary>
    /// Defines a viewmodel for a chemical table's context menu.
    /// </summary>
    internal interface IChemicalTableContextMenuViewModel
    {
        /// <summary>
        /// Get a group of commands that may act on the chemical table.
        /// </summary>
        IChemicalTableCommands Commands { get; }
    }
}
