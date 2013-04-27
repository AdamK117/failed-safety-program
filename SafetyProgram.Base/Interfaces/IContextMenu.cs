using System.Windows.Controls;

namespace SafetyProgram.Base.Interfaces
{
    /// <summary>
    /// Defines an IContextMenu
    ///     Contains a ContextMenu view which can be bound to
    /// </summary>
    public interface IContextMenu : IViewable
    {
        new ContextMenu View { get; }
    }
}
