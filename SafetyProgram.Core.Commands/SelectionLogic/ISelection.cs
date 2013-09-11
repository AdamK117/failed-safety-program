using SafetyProgram.Core.Models;

namespace SafetyProgram.Core.Commands.SelectionLogic
{
    /// <summary>
    /// Defines an interface for a selection.
    /// </summary>
    public interface ISelection
    {
        /// <summary>
        /// Get the parent of the selected item.
        /// </summary>
        IHasMany Parent { get; }

        /// <summary>
        /// Get the child of the selected item.
        /// </summary>
        IApplicationModel Selected { get; }
    }
}
