using System.Windows.Controls;
using SafetyProgram.Base;
using SafetyProgram.Core.Commands;
using SafetyProgram.Core.Commands.ICommands;
using SafetyProgram.Core.Commands.SelectionLogic;
using SafetyProgram.Core.Models;

namespace SafetyProgram.UI.DocumentObject.ChemicalTableUI
{
    /// <summary>
    /// Defines a standard implementation of an IChemcialTableUiController.
    /// </summary>
    internal sealed class ChemicalTableViewController : 
        IChemicalTableController
    {
        /// <summary>
        /// Construct an instance of a ChemicalTableUiController. A controller for a chemical table ui object.
        /// </summary>
        /// <param name="model">The underlying chemical table model.</param>
        /// <param name="configuration">The configuration used by the application.</param>
        /// <param name="commandInvoker">The command invoker used by the application.</param>
        public ChemicalTableViewController(IChemicalTable model, 
            IApplicationConfiguration configuration, 
            ICommandInvoker commandInvoker,
            ISelectionManager selectionManager)
        {
            Helpers.NullCheck(model,
                configuration,
                commandInvoker,
                selectionManager);

            this.model = model;

            var tableCommands = new ChemicalTableICommands(model, commandInvoker);

            this.view = new ChemicalTableView(
                new ChemicalTableViewModel(
                    model));
        }

        private readonly Control view;

        /// <summary>
        /// Get the view for the chemical table.
        /// </summary>
        public Control View
        {
            get { return view; }
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
        IChemicalTable IChemicalTableController.Model
        {
            get { return model; }
        }
    }
}
