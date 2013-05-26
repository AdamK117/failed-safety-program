using SafetyProgram.DocumentObjects.ChemicalTableNs.Commands;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs.ContextMenus
{
    internal interface IChemicalTableContextMenuViewModel
    {
        IChemicalTableCommands Commands { get; }
    }
}
