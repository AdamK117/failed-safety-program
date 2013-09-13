using System.Collections.ObjectModel;
using Fluent;
using SafetyProgram.Base;
using SafetyProgram.Core;
using SafetyProgram.Core.Commands.ICommands;
using SafetyProgram.Core.Commands.SelectionLogic;
using SafetyProgram.Core.Models;
using SafetyProgram.UI.ModelViews.RibbonTabViews.Documents;

namespace SafetyProgram.UI.MainView.Default
{
    internal sealed class RibbonViewModel : 
        IRibbonViewModel
    {
        private readonly IApplicationConfiguration applicationConfiguration;
        private readonly ICommandInvoker commandInvoker;
        private readonly ISelectionManager selectionManager;

        public RibbonViewModel(IApplicationKernel model,
            IApplicationConfiguration configuration,
            ICommandController commandInvoker,
            ISelectionManager selectionManager)
        {
            Helpers.NullCheck(model,
                configuration,
                commandInvoker, 
                selectionManager);

            this.applicationConfiguration = configuration;
            this.commandInvoker = commandInvoker;
            this.selectionManager = selectionManager;

            this.Commands = new CoreCommands(
                model,
                commandInvoker);

            this.RibbonTabs = new ObservableCollection<RibbonTabItem>();

            model.DocumentChanged +=
                (s, e) => documentChanged(e.NewProperty);
            documentChanged(model.Document);
        }

        private void documentChanged(IDocument newDocument)
        {
            RibbonTabs.Clear();

            if (newDocument != null)
            {
                RibbonTabs.Add(
                    new InsertRibbonTabView(
                        new InsertRibbonTabViewModel(
                            newDocument,
                            commandInvoker)));
            }
        }

        /// <summary>
        /// Get the ribbon tabs for the ribbon.
        /// </summary>
        public ObservableCollection<
            RibbonTabItem> RibbonTabs { get; private set; }

        /// <summary>
        /// Get the commands available to the ribbon.
        /// </summary>
        public ICoreCommands Commands { get; private set; }
    }
}
