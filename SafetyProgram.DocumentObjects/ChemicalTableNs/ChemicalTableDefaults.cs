using System.Collections.ObjectModel;
using System.Windows.Controls;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.DocumentObjects.ChemicalTableNs.Commands;
using SafetyProgram.DocumentObjects.ChemicalTableNs.ContextMenus;
using SafetyProgram.DocumentObjects.ChemicalTableNs.Ribbon;
using SafetyProgram.ModelObjects;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs
{
    internal static class ChemicalTableDefaults
    {
        public static ChemicalTable DefaultTable(IConfiguration appConfiguration)
        {
            return new ChemicalTable(
                appConfiguration,
                DefaultChemicals(),
                DEFAULT_HEADER,
                DefaultCommandsConstructor,
                DefaultContextMenuConstructor,
                DefaultRibbonConstructor,
                DefaultViewConstructor
                );
        }

        public static ObservableCollection<ICoshhChemicalObject> DefaultChemicals()
        {
            return new ObservableCollection<ICoshhChemicalObject>();
        }

        public const string DEFAULT_HEADER = "Chemical Table";

        public static IChemicalTableCommands DefaultCommandsConstructor(ChemicalTable chemicalTable)
        {
            return new ChemicalTableCommands(chemicalTable);
        }

        public static IContextMenu DefaultContextMenuConstructor(ChemicalTable chemicalTable)
        {
            return new ChemicalTableContextMenu(chemicalTable);
        }

        public static IRibbonTabItem DefaultRibbonConstructor(ChemicalTable chemTable)
        {
            return new ChemicalTableRibbonTab(chemTable);
        }

        public static UserControl DefaultViewConstructor(ChemicalTable chemicalTable)
        {
            return new ChemicalTableView(chemicalTable);
        }
    }
}
