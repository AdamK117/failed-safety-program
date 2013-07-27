using System.Windows;

namespace SafetyProgram.UI
{
    public interface IApplicationUiController
    {
        /// <summary>
        /// Get the UI view for the application
        /// </summary>
        Window View { get; }
    }
}
