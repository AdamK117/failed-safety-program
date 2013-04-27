using System.Windows.Controls;

namespace SafetyProgram.Base.Interfaces
{
    /// <summary>
    /// Defines an object that is viewable (can be seen in the UI).
    /// </summary>
    public interface IViewable
    {
        /// <summary>
        /// Gets the viewable control for this IViewable
        /// </summary>
        Control View { get; }
    }
}
