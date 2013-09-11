using System.Collections.ObjectModel;
using System.Windows.Controls;
using Fluent;
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
        /// Get the view associated with the document object.
        /// </summary>
        Control View { get; }

        /// <summary>
        /// Get the underlying IDocumentObject model.
        /// </summary>
        IDocumentObject Model { get; }
    }
}
