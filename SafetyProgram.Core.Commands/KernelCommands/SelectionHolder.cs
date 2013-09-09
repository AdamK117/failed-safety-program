using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SafetyProgram.Core.Models;

namespace SafetyProgram.Core.Commands.KernelCommands
{
    /// <summary>
    /// Defines a standard implmenetation of an ISelectionHolder for an IDocument.
    /// </summary>
    public sealed class SelectionHolder : ISelectionHolder
    {
        public SelectionHolder(IDocument documentModel)
        {

        }

        public void Select<T>(Models.IHasMany<T> parent, T item) where T : Models.IApplicationModel
        {
            throw new NotImplementedException();
        }

        public void DeSelect<T>(Models.IHasMany<T> parent, T item) where T : Models.IApplicationModel
        {
            throw new NotImplementedException();
        }

        public void ClearSelection()
        {
            throw new NotImplementedException();
        }

        public ICollection<T> GetSelection<T>(Models.IHasMany<T> parent) where T : Models.IApplicationModel
        {
            throw new NotImplementedException();
        }
    }
}
