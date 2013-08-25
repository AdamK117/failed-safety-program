using System.Windows.Controls;
using Fluent;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Core;
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
            IConfiguration configuration, 
            ICommandInvoker commandInvoker)
        {
            IChemicalTableCommands tableCommands = null;

            this.view = new ChemicalTableView(
                new ChemicalTableViewModel(
                    chemicalTable,
                    tableCommands));
        }

        private readonly Control view;

        /// <summary>
        /// Get the view for the chemical table.
        /// </summary>
        public Control View
        {
            get { return view; }
        }

        private readonly RibbonTabItem contextualTab;

        /// <summary>
        /// Get the chemical table's contextual ribbon tab.
        /// </summary>
        public Fluent.RibbonTabItem ContextualTab
        {
            get { return contextualTab; }
        }
    }
}
