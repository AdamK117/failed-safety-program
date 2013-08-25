using System.Windows;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.UI.Document;

namespace SafetyProgram.UI
{
    public interface IApplicationUiController
    {
        /// <summary>
        /// Get the UI view for the application
        /// </summary>
        Window View { get; }

        /// <summary>
        /// Get the controller of the document Ui.
        /// </summary>
        IHolder<IDocumentUiController> Document { get; }
    }
}
