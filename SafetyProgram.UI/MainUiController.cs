using System.Windows;
using System.Windows.Controls;
using SafetyProgram.Base;
using SafetyProgram.Core;
using SafetyProgram.Core.Commands.SelectionLogic;
using SafetyProgram.UI.MainWindow;

namespace SafetyProgram.UI
{
    /// <summary>
    /// Defines a standard implementation of IApplicationController.
    /// </summary>
    public sealed class MainUiController : 
        IWindowController
    {
        private readonly IApplicationKernel applicationKernel;
        private readonly ICommandController commandInvoker;
        private readonly ISelectionManager selectionManager;       

        /// <summary>
        /// Construct an instance of an application UI controller.
        /// </summary>
        /// <param name="applicationKernel">The underlying application model 
        /// the controller oversees.</param>
        public MainUiController(IApplicationKernel applicationKernel)
        {
            Helpers.NullCheck(applicationKernel);

            // Create application singletons for DI here.
            this.applicationKernel = applicationKernel;
            this.commandInvoker = new CommandController();
            this.selectionManager = new SelectionManager();

            // Construct a controller for the main window of the application.
            var mainViewController = new MainWindowViewController(
                applicationKernel,
                applicationKernel.Configuration,
                commandInvoker,
                selectionManager);

            this.view = mainViewController.View;
        }

        private readonly Window view;

        /// <summary>
        /// Get the window view this controller oversees.
        /// </summary>
        public Window View
        {
            get { return view; }
        }

        /// <summary>
        /// Get the generic control view this controller oversees.
        /// </summary>
        Control IUiController.View
        {
            get { return view; }
        }
    }
}
