using System.Windows;
using SafetyProgram.Base;
using SafetyProgram.Core;
using SafetyProgram.Core.Commands;
using SafetyProgram.UI.Document;

namespace SafetyProgram.UI
{
    /// <summary>
    /// Defines a standard implementation of IApplicationUiController.
    /// </summary>
    public sealed class ApplicationUiController : IApplicationUiController
    {
        /// <summary>
        /// Construct an instance of an application UI controller.
        /// </summary>
        /// <param name="applicationKernel"></param>
        public ApplicationUiController(IApplicationKernel applicationKernel)
        {
            var commandInvoker = new CommandInvoker();
            var coreCommands = new CoreCommands(applicationKernel, commandInvoker);

            this.view = new MainView(
                new MainViewModel(
                    coreCommands,
                    new DocumentUiController(null, applicationKernel.Configuration, commandInvoker)
                )
            );
        }

        private readonly Window view;

        /// <summary>
        /// Get the ui for the application.
        /// </summary>
        public System.Windows.Window View
        {
            get { return view; }
        }

        private IDocumentUiController document;

        public Document.IDocumentUiController Document
        {
            get { return document; }
        }
    }
}
