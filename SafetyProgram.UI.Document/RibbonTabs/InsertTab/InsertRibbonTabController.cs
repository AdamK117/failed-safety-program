using System.Windows.Controls;
using Fluent;
using SafetyProgram.Base;
using SafetyProgram.Core.Commands;
using SafetyProgram.Core.Commands.SelectionLogic;
using SafetyProgram.Core.Models;
using SafetyProgram.Document.Ribbons;

namespace SafetyProgram.UI.Document.RibbonTabs.InsertTab
{
    public sealed class InsertRibbonTabController :
        IRibbonTabController
    {
        public InsertRibbonTabController(IDocument model,
            IApplicationConfiguration appConfiguration,
            ICommandInvoker commandInvoker,
            ISelectionManager selectionManager)
        {
            Helpers.NullCheck(model,
                appConfiguration,
                commandInvoker,
                selectionManager);

            view = new InsertRibbonTabView(
                new InsertRibbonTabViewModel(
                    new DocumentICommands(
                        model, commandInvoker)));
        }

        private readonly RibbonTabItem view;

        /// <summary>
        /// Get the ribbon tab view for this ribbon tab controller.
        /// </summary>
        public RibbonTabItem View
        {
            get { return view; }
        }

        /// <summary>
        /// Get the generic control view for this ribbon tab controller.
        /// </summary>
        Control IUiController.View
        {
            get { return view; }
        }
    }
}
