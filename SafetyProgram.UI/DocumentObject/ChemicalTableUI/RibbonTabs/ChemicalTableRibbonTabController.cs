using System.Windows.Controls;
using Fluent;
using SafetyProgram.Base;
using SafetyProgram.Core.Commands;
using SafetyProgram.Core.Commands.ICommands;
using SafetyProgram.Core.Commands.SelectionLogic;
using SafetyProgram.Core.Models;

namespace SafetyProgram.UI.DocumentObject.ChemicalTableUI.RibbonTabs
{
    /// <summary>
    /// Defines a standard implementation of a UI controller for a 
    /// chemical table ribbon tab (contextual).
    /// </summary>
    internal sealed class ChemicalTableRibbonTabController : 
        IRibbonTabController
    {
        /// <summary>
        /// Construct an instance of a chemical table ribbon tab UI
        /// controller.
        /// </summary>
        /// <param name="model">The chemical table model.</param>
        /// <param name="applicationConfiguration">The (singleton) application
        /// configuration.</param>
        /// <param name="commandInvoker">A command invoker through which any
        /// commands called through the ribbon will be invoked.</param>
        /// <param name="selectionManager">A selection manager monitored by the
        /// ribbon tab controller.</param>
        public ChemicalTableRibbonTabController(IChemicalTable model,
            IApplicationConfiguration applicationConfiguration,
            ICommandInvoker commandInvoker,
            ISelectionManager selectionManager)
        {
            Helpers.NullCheck(model,
                applicationConfiguration,
                commandInvoker,
                selectionManager);

            var chemicalTableCommands =
                new ChemicalTableICommands(
                    model,
                    commandInvoker);

            this.view = new ChemicalTableRibbonView(
                new ChemicalTableRibbonTabViewModel(
                    applicationConfiguration,
                    chemicalTableCommands));
        }

        private readonly RibbonTabItem view;

        /// <summary>
        /// Get the ribbon tab view overseeen by this controller.
        /// </summary>
        public Fluent.RibbonTabItem View
        {
            get { return view; }
        }

        /// <summary>
        /// Get the generic view overseen by this controller.
        /// </summary>
        Control IUiController.View
        {
            get { return view; }
        }
    }
}
