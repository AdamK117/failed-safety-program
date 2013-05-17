using SafetyProgram.Base.Interfaces;
using SafetyProgram.DocumentObjects.ChemicalTableNs.Commands;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs.Ribbon
{
    interface IChemicalTableRibbonTab : IRibbonTabItem
    {
        IChemicalTableCommands Commands { get; }
    }
}
