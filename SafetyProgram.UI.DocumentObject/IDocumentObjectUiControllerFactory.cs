using SafetyProgram.Core.Models;

namespace SafetyProgram.UI.DocumentObject
{
    public interface IDocumentObjectUiControllerFactory
    {
        IDocumentObject GetDocumentObject(IDocumentObjectUiController controller);

        IDocumentObjectUiController GetDocumentObjectUiController(IDocumentObject documentObject);
    }
}
