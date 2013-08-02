using SafetyProgram.Core.Models;

namespace SafetyProgram.UI.DocumentObject
{
    /// <summary>
    /// Defines an abstract factory for creating IDocumentObjectUiControllers.
    /// </summary>
    public interface IDocumentObjectUiControllerFactory
    {
        IDocumentObject GetDocumentObject(IDocumentObjectUiController controller);

        IDocumentObjectUiController GetDocumentObjectUiController(IDocumentObject documentObject);
    }
}
