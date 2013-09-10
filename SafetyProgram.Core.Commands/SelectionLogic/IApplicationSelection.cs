using System;
using System.Collections.Generic;

namespace SafetyProgram.Core.Commands.KernelCommands
{
    public interface IApplicationSelection
    {
        /// <summary>
        /// Get the current selection as a set of coordinates.
        /// </summary>
        /// <example>[0,1,1], [0,1,2], [0,1,3], [0,2]</example>
        IList<
            IList<int>> Selection { get; }

        /// <summary>
        /// Occurs when the selection changes.
        /// </summary>
        event EventHandler SelectionChanged;
    }
}
