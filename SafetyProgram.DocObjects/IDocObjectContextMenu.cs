using System.Windows.Controls;

namespace SafetyProgram.DocObjects
{
    public interface IDocObjectContextMenu
    {
        /// <summary>
        /// Gets the ContextMenu view for the DocObjectContextMenu.
        /// </summary>
        ContextMenu View { get; }
    }
}
