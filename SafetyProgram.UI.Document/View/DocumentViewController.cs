using System.Windows.Controls;
using SafetyProgram.Base;
using SafetyProgram.Core.Commands.SelectionLogic;
using SafetyProgram.Core.Models;
using SafetyProgram.UI.Document.View;
using SafetyProgram.UI.DocumentObject;

namespace SafetyProgram.UI.Document
{
    /// <summary>
    /// Defines a standard implementation of the IDocumentController 
    /// interface.
    /// </summary>
    public sealed class DocumentViewController : 
        IDocumentUiController
    {
        private readonly IDocumentObjectUiControllerFactory documentObjectControllerFactory;
        
        public DocumentViewController(IDocument model, 
            IApplicationConfiguration configuration, 
            ICommandInvoker commandInvoker,
            ISelectionManager selectionManager)
        {
            Helpers.NullCheck(model,
                configuration,
                commandInvoker,
                selectionManager);

            this.model = model;
            
            documentObjectControllerFactory 
                = new DocumentObjectUiControllerFactory(
                    configuration,
                    commandInvoker, 
                    selectionManager);

            var documentObjectControllers 
                = new LinkedReadOnlyObservableCollection
                    <IDocumentObject, IDocumentObjectUiController>(
                        model.Content,
                        documentObjectControllerFactory.GetDocumentObjectUiController);

            // Construct the document view.
            this.View = new DocumentView(
                new DocumentViewModel(
                    model,
                    documentObjectControllers));
        }

        /// <summary>
        /// Get the view this controller oversees (mVc).
        /// </summary>
        public Control View { get; private set; }

        private readonly IDocument model;

        /// <summary>
        /// Get the underlying document model this controller oversees.
        /// </summary>
        IDocument IDocumentUiController.Model
        {
            get { return model; }
        }
    }
}
