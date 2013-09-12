using SafetyProgram.Core.Models;

namespace SafetyProgram.UI.DocumentObject
{
    /// <summary>
    /// Defines an abstract factory for creating IDocumentObjectUiControllers.
    /// </summary>
    public interface IDocumentObjectUiControllerFactory
    {
        /// <summary>
        /// Create an <code>IDocumentObjectUiController</code> from the supplied <code>IDocumentObject</code> model.
        /// </summary>
        /// <param name="documentObject">The model from which to create the Ui controller.</param>
        /// <returns>A new <code>IDocumentObjectUiController</code> created from the model.</returns>
        IDocumentObjectUiController GetDocumentObjectUiController(IDocumentObject documentObject);
    }
}
