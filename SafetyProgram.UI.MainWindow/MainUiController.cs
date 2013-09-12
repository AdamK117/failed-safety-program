using System.Windows;
using SafetyProgram.Base;
using SafetyProgram.Core;
using SafetyProgram.Core.Commands.SelectionLogic;
using SafetyProgram.UI.MainWindow.View;

namespace SafetyProgram.UI.MainWindow
{
    /// <summary>
    /// Defines a standard implementation of IApplicationController.
    /// </summary>
    public sealed class MainUiController
    {
        private readonly IApplicationKernel applicationKernel;
        private readonly ICommandController commandInvoker;
        private readonly ISelectionManager selectionManager;       

        /// <summary>
        /// Construct an instance of an application UI controller.
        /// </summary>
        /// <param name="applicationKernel">The underlying application model the controller oversees.</param>
        public MainUiController(IApplicationKernel applicationKernel)
        {
            Helpers.NullCheck(applicationKernel);

            // Create application singletons for DI here.
            this.applicationKernel = applicationKernel;
            this.commandInvoker = new CommandController();
            this.selectionManager = new SelectionManager();

            // Construct a controller for the main window of the application.
            var mainViewController = new MainWindowController(
                applicationKernel,
                applicationKernel.Configuration,
                commandInvoker,
                selectionManager);

            this.View = mainViewController.View;
        }

        /// <summary>
        /// Get the ui for the application.
        /// </summary>
        public Window View { get; private set; }
    }
}
