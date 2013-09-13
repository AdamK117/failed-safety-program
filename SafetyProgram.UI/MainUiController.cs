using System.Windows;
using SafetyProgram.Base;
using SafetyProgram.Core;
using SafetyProgram.Core.Commands.SelectionLogic;
using SafetyProgram.UI.Views.MainView.Default;

namespace SafetyProgram.UI
{
    /// <summary>
    /// Defines a standard implementation of IApplicationController.
    /// </summary>
    public sealed class MainUiController
    {
        /// <summary>
        /// Construct an instance of an application UI controller.
        /// </summary>
        /// <param name="applicationKernel">The underlying application model 
        /// the controller oversees.</param>
        public MainUiController(IApplicationKernel applicationKernel)
        {
            Helpers.NullCheck(applicationKernel);

            var commandInvoker = new CommandController();
            var selectionManager = new SelectionManager();

            var documentViewFactory = ContentViewFactories.DocumentViewFactory(
                applicationKernel.Configuration,
                commandInvoker,
                selectionManager);

            this.View = new MainViews(
                new MainViewModel(
                    applicationKernel,
                    new RibbonView(
                        new RibbonViewModel(
                            applicationKernel,
                            applicationKernel.Configuration,
                            commandInvoker,
                            selectionManager,
                            RibbonFactories.DocumentRibbonTabFactory(commandInvoker))),
                    documentViewFactory));
        }

        /// <summary>
        /// Get the window view this controller oversees.
        /// </summary>
        public Window View { get; private set; }
    }
}
