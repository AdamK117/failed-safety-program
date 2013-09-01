using System.Collections.ObjectModel;
using System.Windows.Controls;
using Fluent;
using SafetyProgram.Base;
using SafetyProgram.Core.Commands;
using SafetyProgram.Core.Models;

namespace SafetyProgram.UI.DocumentObject.ChemicalTableUI
{
    /// <summary>
    /// Defines a standard implementation of an IChemcialTableUiController.
    /// </summary>
    public sealed class ChemicalTableUiController : IChemicalTableUiController
    {
        /// <summary>
        /// Construct an instance of a ChemicalTableUiController. A controller for a chemical table ui object.
        /// </summary>
        /// <param name="chemicalTable">The underlying chemical table model.</param>
        /// <param name="configuration">The configuration used by the application.</param>
        /// <param name="commandInvoker">The command invoker used by the application.</param>
        public ChemicalTableUiController(IChemicalTable chemicalTable, 
            IApplicationConfiguration configuration, 
            ICommandInvoker commandInvoker)
        {
            this.model = chemicalTable;
            IChemicalTableCommands tableCommands = new ChemicalTableICommands(chemicalTable, commandInvoker);

            this.view = new ChemicalTableView(
                new ChemicalTableViewModel(
                    chemicalTable,
                    tableCommands));

            this.contextualTab = new ObservableCollection<RibbonTabItem>()
            {
                new ChemicalTableRibbonView(
                    new ChemicalTableRibbonTabViewModel(
                        configuration,
                        tableCommands))
            };
        }

        private readonly Control view;

        /// <summary>
        /// Get the view for the chemical table.
        /// </summary>
        public Control View
        {
            get { return view; }
        }

        private readonly ObservableCollection<RibbonTabItem> contextualTab;

        /// <summary>
        /// Get the chemical table's contextual ribbon tab.
        /// </summary>
        public ObservableCollection<RibbonTabItem> ContextualTabs
        {
            get { return contextualTab; }
        }

        private readonly IChemicalTable model;

        /// <summary>
        /// Get the underlying <code>IDocumentObject</code> model.
        /// </summary>
        public IDocumentObject Model
        {
            get { return model; }
        }

        /// <summary>
        /// Get the underlying <code>IChemicalTable</code> model.
        /// </summary>
        IChemicalTable IChemicalTableUiController.Model
        {
            get { return model; }
        }
    }
}
