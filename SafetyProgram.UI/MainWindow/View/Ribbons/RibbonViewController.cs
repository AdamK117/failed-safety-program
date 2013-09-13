using System.Collections.ObjectModel;
using System.Windows.Controls;
using Fluent;
using SafetyProgram.Base;
using SafetyProgram.Core;
using SafetyProgram.Core.Commands;
using SafetyProgram.Core.Commands.ICommands;
using SafetyProgram.Core.Commands.SelectionLogic;
using SafetyProgram.Core.Models;
using SafetyProgram.UI.Document;

namespace SafetyProgram.UI.MainWindow
{
    /// <summary>
    /// Defines a standard implementation of a controller for the ribbon view.
    /// </summary>
    internal sealed class RibbonViewController :
        IRibbonViewController
    {
        private readonly IApplicationKernel model;
        private readonly IApplicationConfiguration
            applicationConfiguration;
        private readonly ICommandInvoker commandInvoker;
        private readonly ISelectionManager
            selectionManager;

        private readonly ObservableCollection<RibbonTabItem>
            documentRibbonTabs = new ObservableCollection<RibbonTabItem>();

        public RibbonViewController(IApplicationKernel model,
            IApplicationConfiguration configuration,
            ICommandController commandInvoker,
            ISelectionManager selectionManager)
        {
            Helpers.NullCheck(
                model,
                configuration,
                commandInvoker,
                selectionManager);

            this.model = model;
            this.applicationConfiguration 
                = configuration;
            this.commandInvoker = commandInvoker;
            this.selectionManager = selectionManager;

            this.model.DocumentChanged +=
                (s, e) => documentChanged();
            documentChanged();

            this.view = new RibbonView(
                new RibbonViewModel(
                    new CoreCommands(
                        model,
                        commandInvoker),
                        documentRibbonTabs));
        }

        /// <summary>
        /// Handles the DocumentChanged event in the application kernel.
        /// </summary>
        private void documentChanged()
        {
            if (model.Document == null)
            {
                documentRibbonTabs.Clear();
            }
            else
            {
                var firstTab = new InsertRibbonTabController(
                    model.Document,
                    applicationConfiguration,
                    commandInvoker,
                    selectionManager);
                documentRibbonTabs.Add(firstTab.View);
            }
        }

        private readonly Ribbon view;
        
        /// <summary>
        /// Get the ribbon view the controller oversees.
        /// </summary>
        public Ribbon View
        {
            get { return view; }
        }

        /// <summary>
        /// Get the generic control view the controller oversees.
        /// </summary>
        Control IUiController.View
        {
            get { return view; }
        }
    }
}
