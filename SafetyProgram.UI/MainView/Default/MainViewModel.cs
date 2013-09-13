using System.Windows.Controls;
using Fluent;
using SafetyProgram.Base;
using SafetyProgram.Core;
using SafetyProgram.Core.Commands.SelectionLogic;
using SafetyProgram.Core.Models;

namespace SafetyProgram.UI.MainView.Default
{
    /// <summary>
    /// Defines a standard implementation of an <code>IMainViewModel</code>
    /// </summary>
    internal sealed class MainViewModel : 
        IMainViewModel
    {
        public MainViewModel(IApplicationKernel model,
            IApplicationConfiguration configuration,
            ICommandController commandController,
            ISelectionManager selectionManager)
        {
            Helpers.NullCheck(model,
                configuration,
                commandController,
                selectionManager);

            this.RibbonView = new RibbonView(
                new RibbonViewModel(
                    model,
                    configuration,
                    commandController,
                    selectionManager));

            this.ContentView = new ContentView(
                new ContentViewModel(
                    model,
                    configuration,
                    commandController,
                    selectionManager));
        }

        /// <summary>
        /// Get the ribbon view.
        /// </summary>
        public Ribbon RibbonView { get; private set; }

        /// <summary>
        /// Get the content view.
        /// </summary>
        public Control ContentView { get; private set; }
    }
}
