using SafetyProgram.Base;
using SafetyProgram.Core.Models;

namespace SafetyProgram.UI.DocumentObject
{
    /// <summary>
    /// Defines an interface for a UiController of an IDocumentObject.
    /// </summary>
    public interface IDocumentObjectUiController : 
        IUiController
    {
        /// <summary>
        /// Get the underlying IDocumentObject model.
        /// </summary>
        new IDocumentObject Model { get; }
    }
}
