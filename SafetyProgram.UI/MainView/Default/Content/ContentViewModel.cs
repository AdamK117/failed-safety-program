using System.ComponentModel;
using System.Windows.Controls;
using SafetyProgram.Base;
using SafetyProgram.Core;
using SafetyProgram.Core.Commands.SelectionLogic;
using SafetyProgram.Core.Models;
using SafetyProgram.UI.ModelViews.Documents.Default;

namespace SafetyProgram.UI.MainView.Default
{
    /// <summary>
    /// Defines a standard implementation of a content view 
    /// viewmodel.
    /// </summary>
    internal sealed class ContentViewModel :
        IContentViewModel
    {
        private readonly IApplicationConfiguration applicationConfiguration;
        private readonly ICommandInvoker commandInvoker;
        private readonly ISelectionManager selectionManager;

        public ContentViewModel(IApplicationKernel model,
            IApplicationConfiguration configuration,
            ICommandInvoker commandInvoker,
            ISelectionManager selectionManager)
        {
            Helpers.NullCheck(model,
                configuration,
                commandInvoker,
                selectionManager);

            this.applicationConfiguration = configuration;
            this.commandInvoker = commandInvoker;
            this.selectionManager = selectionManager;

            model.DocumentChanged +=
                (s, e) => documentChanged(e.NewProperty);

            documentChanged(model.Document);
        }

        private void documentChanged(IDocument newDocument)
        {
            this.Content = null;

            if (newDocument != null)
            {
                Content = new DocumentView(
                    new DocumentViewModel(
                        newDocument,
                        applicationConfiguration,
                        commandInvoker,
                        selectionManager));
            }
        }

        private Control content;

        /// <summary>
        /// Get the content view.
        /// </summary>
        public Control Content
        {
            get { return content; }
            set
            {
                content = value;
                PropertyChanged.Raise(this, "Content");
            }
        }

        /// <summary>
        /// Occurs if a property on the viewmodel changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
