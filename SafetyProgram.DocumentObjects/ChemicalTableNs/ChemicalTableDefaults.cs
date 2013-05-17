using System;
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
        public static ChemicalTable DefaultTable(IConfiguration appConfiguration, ICommandInvoker commandInvoker)
        {
            return new ChemicalTable(
                appConfiguration,
                DefaultChemicals(),
                DEFAULT_HEADER,
                DefaultCommandsConstructor(commandInvoker),
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

        public static Func<IChemicalTable, IChemicalTableCommands> DefaultCommandsConstructor(ICommandInvoker commandInvoker)
        {
            return (chemicalTable) =>
                {
                    return new ChemicalTableCommands(chemicalTable, commandInvoker);
                };
        }

        public static IContextMenu DefaultContextMenuConstructor(IChemicalTable chemicalTable)
        {
            return new ChemicalTableContextMenu(chemicalTable, (viewModel) => new ChemicalTableContextMenuView(viewModel));
        }

        public static IRibbonTabItem DefaultRibbonConstructor(IChemicalTable chemTable)
        {
            return new ChemicalTableRibbonTab(chemTable, (viewModel) => new ChemicalTableRibbonView(viewModel));
        }

        public static UserControl DefaultViewConstructor(IChemicalTable chemicalTable)
        {
            return new ChemicalTableView(chemicalTable);
        }
    }
}
