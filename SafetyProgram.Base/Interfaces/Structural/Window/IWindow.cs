using System.Windows;

namespace SafetyProgram.Base.Interfaces
{
    /// <summary>
    /// Defines a viewable (IViewable) window
    /// </summary>
    public interface IWindow : IViewable
    {
        new Window View { get; }
    }
}
