using System;
using System.Collections.Generic;
using SafetyProgram.Base;
using SafetyProgram.Core.Models;

namespace SafetyProgram.Core.Commands.SelectionLogic
{
    /// <summary>
    /// Defines a standard implementation of a selection manager that
    /// controls the current selection within a particular unit (such
    /// as a document.
    /// </summary>
    public sealed class SelectionManager :
        ISelectionManager
    {
        private readonly IDocument document;
        private readonly IList<ISelection> currentSelection
            = new List<ISelection>();

        /// <summary>
        /// Select the specified model.
        /// </summary>
        /// <param name="model">The model to select.</param>
        public void Select<T>(T model, IHasManyT<T> parent)
            where T : IApplicationModel
        {
            currentSelection.Add(
                new Selection(parent,model));

            SelectionChanged.Raise(this);
        }

        public IEnumerable<ISelection> Selection
        {
            get { return currentSelection; }
        }

        public event EventHandler SelectionChanged;
    }
}
