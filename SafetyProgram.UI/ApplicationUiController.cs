using System.Windows;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Core;
using SafetyProgram.Core.Commands;
using SafetyProgram.Core.Models;
using SafetyProgram.UI.Document;

namespace SafetyProgram.UI
{
    /// <summary>
    /// Defines a standard implementation of IApplicationUiController.
    /// </summary>
    public sealed class ApplicationUiController : IApplicationUiController
    {
        private readonly ICommandInvoker commandInvoker;
        private readonly IApplicationKernel applicationKernel;

        /// <summary>
        /// Construct an instance of an application UI controller.
        /// </summary>
        /// <param name="applicationKernel">The underlying application model the controller oversees.</param>
        public ApplicationUiController(IApplicationKernel applicationKernel)
        {
            this.applicationKernel = applicationKernel;
            this.commandInvoker = new CommandInvoker();

            var coreCommands = new CoreCommands(applicationKernel, commandInvoker);

            /* Create a mutable holder for the idocumentuicontroller. A new controller will
             * be generated each time that the document changes in the underlying kernel.
             * The holder will allow subscribers (such as views) to rebind to the new document controller. */
            this.document = new Holder<IDocumentUiController>(
                new DocumentUiController(
                    applicationKernel.Document, 
                    applicationKernel.Configuration, 
                    commandInvoker));
            
            /* Attach to the documentchanged event in the kernel, this will allow the controller to change 
             * the mutable documentuicontroller when the model changes */
            applicationKernel.DocumentChanged += 
                (sender, newDocument) => documentChanged(newDocument.NewProperty);

            // Construct the view this controller oversees.
            this.view = new MainView(
                new MainViewModel(
                    coreCommands,
                    this.document,
                    new RibbonView(
                        new RibbonViewModel(
                            coreCommands,
                            document))));            
        }

        /// <summary>
        /// Handles the document changed event. Updates the necessary mutable holder with a new document controller
        /// for subscribers to attach to.
        /// </summary>
        /// <param name="newDocument"></param>
        private void documentChanged(IDocument newDocument)
        {
            document.Content = new DocumentUiController(
                newDocument, 
                applicationKernel.Configuration, 
                commandInvoker);
        }

        private readonly Window view;

        /// <summary>
        /// Get the ui for the application.
        /// </summary>
        public Window View
        {
            get { return view; }
        }

        private readonly IEditableHolder<IDocumentUiController> document;

        /// <summary>
        /// Get the controller for the document inside the window.
        /// </summary>
        public IHolder<IDocumentUiController> Document
        {
            get { return document; }
        }
    }
}
