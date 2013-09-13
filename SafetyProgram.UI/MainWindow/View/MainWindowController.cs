using System.Windows;
using System.Windows.Controls;
using SafetyProgram.Base;
using SafetyProgram.Core;
using SafetyProgram.Core.Commands.SelectionLogic;
using SafetyProgram.Core.Models;

namespace SafetyProgram.UI.MainWindow
{
    /// <summary>
    /// Defines a standard implementation of an <code>IWindowController</code> 
    /// for the main application window UI.
    /// </summary>
    internal sealed class MainWindowViewController : IWindowController
    {
        public MainWindowViewController(IApplicationKernel model,
            IApplicationConfiguration configuration,
            ICommandController commandController,
            ISelectionManager selectionManager)
        {
            Helpers.NullCheck(model,
                configuration,
                commandController,
                selectionManager);

            // Construct the controller for the ribbon view.
            var ribbonController = new RibbonViewController(
                model,
                configuration,
                commandController,
                selectionManager);

            // Construct the controller for the content view.
            var contentController = new ContentViewController(
                model,
                configuration,
                commandController,
                selectionManager);

            /* Construct the main window view. It is composed of a 
             * content view and a ribbon view. */
            this.view = new MainView(
                new MainViewModel(
                    ribbonController.View,
                    contentController.View));
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
