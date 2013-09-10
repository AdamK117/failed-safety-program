using System;
using System.Collections.Generic;
using SafetyProgram.Core.Models;

namespace SafetyProgram.Core.Commands.KernelCommands
{
    public interface ISelectionManager
    {
        /// <summary>
        /// Select the specified application model.
        /// </summary>
        /// <param name="model">The model to select.</param>
        void Select(IApplicationModel model);

        /// <summary>
        /// Deselect the specified application model.
        /// </summary>
        /// <param name="model">The model to deselect.</param>
        void DeSelect(IApplicationModel model);

        /// <summary>
        /// Clear the current selection in the application.
        /// </summary>
        void ClearSelection();

        /// <summary>
        /// Get the current selection in the application.
        /// </summary>
        IEnumerable<IApplicationModel> Selection { get; }

        /// <summary>
        /// Occurs when the applications selection changes.
        /// </summary>
        event EventHandler SelectionChanged;
    }
}
