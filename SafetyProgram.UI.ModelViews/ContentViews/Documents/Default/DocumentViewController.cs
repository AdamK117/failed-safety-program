using System.Windows.Controls;
using SafetyProgram.Base;
using SafetyProgram.Core.Commands.SelectionLogic;
using SafetyProgram.Core.Models;
using SafetyProgram.UI.Document;
using SafetyProgram.UI.DocumentObject;

namespace SafetyProgram.UI.Document
{
    /// <summary>
    /// Defines a standard implementation of the IDocumentController 
    /// interface.
    /// </summary>
    internal sealed class DocumentViewController : 
        IUiController
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
            
            documentObjectControllerFactory 
                = new DocumentObjectUiControllerFactory(
                    configuration,
                    commandInvoker, 
                    selectionManager);

            var documentObjectControllers 
                = new LinkedReadOnlyObservableCollection
                    <IDocumentObject, IUiController>(
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
    }
}
