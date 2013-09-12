using System.Windows.Controls;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Core;
using SafetyProgram.Core.Commands.SelectionLogic;
using SafetyProgram.Core.Models;
using SafetyProgram.UI.Document;

namespace SafetyProgram.UI.MainWindow.ContentViews
{
    internal sealed class ContentViewController :
        IUiController
    {
        private readonly IApplicationKernel model;
        private readonly IApplicationConfiguration
            applicationConfiguration;
        private readonly ICommandInvoker commandInvoker;
        private readonly ISelectionManager
            selectionManager;
        private readonly IEditableHolder<Control> documentViewHolder;

        public ContentViewController(IApplicationKernel model,
            IApplicationConfiguration configuration,
            ICommandInvoker commandInvoker,
            ISelectionManager selectionManager)
        {
            Helpers.NullCheck(model,
                configuration,
                commandInvoker,
                selectionManager);

            this.model = model;
            this.applicationConfiguration = configuration;
            this.commandInvoker = commandInvoker;
            this.selectionManager = selectionManager;

            // We need a bind interface for the document content.
            this.documentViewHolder = new Holder<Control>(null);
            documentChanged();

            this.model.DocumentChanged +=
                (s, e) => documentChanged();

            this.View = new ContentView(
                new ContentViewModel(
                    documentViewHolder));
        }
        
        /// <summary>
        /// Handles when the document changes in the underlying kernel.
        /// </summary>
        private void documentChanged()
        {
            if (model.Document == null)
            {
                documentViewHolder.Content = null;
            }
            else
            {
                var documentViewController = new DocumentViewController(
                            model.Document,
                            applicationConfiguration,
                            commandInvoker,
                            selectionManager);
                documentViewHolder.Content = documentViewController.View;
            }
        }

        /// <summary>
        /// Get the content view.
        /// </summary>
        public Control View { get; private set; }
    }
}
