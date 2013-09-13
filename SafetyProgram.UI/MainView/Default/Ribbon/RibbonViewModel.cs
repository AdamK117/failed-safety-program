using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Fluent;
using SafetyProgram.Base;
using SafetyProgram.Core;
using SafetyProgram.Core.Commands.ICommands;
using SafetyProgram.Core.Commands.SelectionLogic;
using SafetyProgram.Core.Models;

namespace SafetyProgram.UI.MainView.Default
{
    internal sealed class RibbonViewModel : 
        IRibbonViewModel
    {
        private readonly Func<IDocument, IEnumerable<RibbonTabItem>> documentRibbonTabFactory;

        public RibbonViewModel(IApplicationKernel model,
            IApplicationConfiguration configuration,
            ICommandController commandInvoker,
            ISelectionManager selectionManager,
            Func<IDocument, IEnumerable<RibbonTabItem>> documentRibbonTabFactory)
        {
            Helpers.NullCheck(model,
                configuration,
                commandInvoker, 
                selectionManager,
                documentRibbonTabFactory);

            this.Commands = new CoreCommands(
                model,
                commandInvoker);

            this.RibbonTabs = new ObservableCollection<RibbonTabItem>();

            this.documentRibbonTabFactory =
                documentRibbonTabFactory;

            model.DocumentChanged +=
                (s, e) => documentChanged(e.NewProperty);
            documentChanged(model.Document);
        }

        private void documentChanged(IDocument newDocument)
        {
            RibbonTabs.Clear();

            if (newDocument != null)
            {
                var newRibbonTabs = documentRibbonTabFactory(newDocument);

                foreach(RibbonTabItem ribbonTab in newRibbonTabs)
                {
                    RibbonTabs.Add(ribbonTab);
                }
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
