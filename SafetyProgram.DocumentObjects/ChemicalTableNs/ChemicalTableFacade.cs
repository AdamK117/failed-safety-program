using System.Collections.ObjectModel;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.DocumentObjects.ChemicalTableNs.Commands;
using SafetyProgram.DocumentObjects.ChemicalTableNs.ContextMenus;
using SafetyProgram.DocumentObjects.ChemicalTableNs.Ribbon;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs
{
    public static class ChemicalTableFacade
    {
        public static IChemicalTable ChemicalTable(IConfiguration appConfiguration, ICommandInvoker commandInvoker)
        {
            var loadedChemicals = new ObservableCollection<ICoshhChemicalObject>();
            var headerHolder = new Holder<string>("DefaultHeader");

            var selectedChemicals = new ObservableCollection<ICoshhChemicalObject>();
            var tableCommands = new ChemicalTableCommands(
                selectedChemicals,
                loadedChemicals,
                commandInvoker
            );

            return new ChemicalTable(
                headerHolder,
                loadedChemicals,
                new ChemicalTableRibbonView(
                    new ChemicalTableRibbonTabViewModel(
                        appConfiguration,
                        tableCommands
                    )
                ),
                new ChemicalTableView(
                    new ChemicalTableViewModel(
                        headerHolder,
                        new ChemicalTableContextMenuView(
                            new ChemicalTableContextMenuViewModel(
                                tableCommands
                            )
                        ),
                        loadedChemicals,
                        selectedChemicals,
                        tableCommands.Hotkeys
                    )
                )
            );
        }
    }
}
