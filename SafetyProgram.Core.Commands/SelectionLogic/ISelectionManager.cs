using System;
using System.Collections.Generic;
using SafetyProgram.Core.Models;

namespace SafetyProgram.Core.Commands.SelectionLogic
{
    public interface ISelectionManager
    {
        /// <summary>
        /// Select the specified application model.
        /// </summary>
        /// <param name="model">The model to select.</param>
        void Select<T>(T model, IHasManyT<T> parent)
            where T : IApplicationModel;

        /// <summary>
        /// Get the current selection in the application.
        /// </summary>
        IEnumerable<ISelection> Selection { get; }

        /// <summary>
        /// Occurs when the applications selection changes.
        /// </summary>
        event EventHandler SelectionChanged;
    }
}
