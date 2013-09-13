using System.Windows;
using SafetyProgram.Base;
using SafetyProgram.Core;
using SafetyProgram.Core.Commands.SelectionLogic;
using SafetyProgram.UI.MainView.Default;

namespace SafetyProgram.UI
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
        /// <param name="applicationKernel">The underlying application model 
        /// the controller oversees.</param>
        public MainUiController(IApplicationKernel applicationKernel)
        {
            Helpers.NullCheck(applicationKernel);

            // Create application singletons for DI here.
            this.applicationKernel = applicationKernel;
            this.commandInvoker = new CommandController();
            this.selectionManager = new SelectionManager();

            this.View = new MainViews(
                new MainViewModel(
                    applicationKernel,
                    applicationKernel.Configuration,
                    commandInvoker,
                    selectionManager));
        }

        /// <summary>
        /// Get the window view this controller oversees.
        /// </summary>
        public Window View { get; private set; }
    }
}
