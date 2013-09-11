using SafetyProgram.Base;
using SafetyProgram.Core.Models;

namespace SafetyProgram.Core.Commands.SelectionLogic
{
    /// <summary>
    /// Defines a standard implementation of the ISelection
    /// class, a class that holds selection information.
    /// </summary>
    public sealed class Selection : 
        ISelection
    {
        public Selection(IHasMany parent,
            IApplicationModel child)
        {
            Helpers.NullCheck(parent, child);

            this.Parent = parent;
            this.Selected = child;
        }

        /// <summary>
        /// Get the parent of the selection.
        /// </summary>
        public IHasMany Parent { get; private set; }

        /// <summary>
        /// Get the child of the seleciton.
        /// </summary>
        public IApplicationModel Selected { get; private set; }
    }
}
