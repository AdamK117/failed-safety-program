using SafetyProgram.Base.Interfaces;
using SafetyProgram.DocumentObjects.ChemicalTableNs.Commands;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs.ContextMenus
{
    internal interface IChemicalTableContextMenu : IContextMenu
    {
        IChemicalTableCommands Commands { get; }
    }
}
