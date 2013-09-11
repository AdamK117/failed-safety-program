using System.Windows;
using System.Windows.Controls;
using SafetyProgram.Base;
using SafetyProgram.Core;
using SafetyProgram.Core.Commands.SelectionLogic;
using SafetyProgram.Core.Models;
using SafetyProgram.UI.ContentViews;
using SafetyProgram.UI.Ribbons;

namespace SafetyProgram.UI.View
{
    public sealed class MainWindowController : IWindowController
    {
        public MainWindowController(IApplicationKernel model,
            IApplicationConfiguration configuration,
            ICommandController commandController,
            ISelectionManager selectionManager)
        {
            Helpers.NullCheck(model,
                configuration,
                commandController,
                selectionManager);

            var ribbonController = new RibbonViewController(
                model,
                configuration,
                commandController,
                selectionManager);

            var contentController = new ContentViewController(
                model,
                configuration,
                commandController,
                selectionManager);

            this.view = new MainView(
                new MainViewModel(
                    ribbonController.View,
                    contentController.View));
        }

        private readonly Window view;

        public Window View
        {
            get { return view; }
        }

        Control IUiController.View
        {
            get { return view; }
        }
    }
}
