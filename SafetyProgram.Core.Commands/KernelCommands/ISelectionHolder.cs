using System.Collections.Generic;
using SafetyProgram.Core.Models;

namespace SafetyProgram.Core.Commands.KernelCommands
{
    /// <summary>
    /// Defines an interface for the applications selection holder.
    /// </summary>
    public interface ISelectionHolder
    {
        /// <summary>
        /// Select an item.
        /// </summary>
        /// <typeparam name="T">The type of item being selected.</typeparam>
        /// <param name="parent">The holder of the item.</param>
        /// <param name="item">The item to select.</param>
        void Select<T>(IHasMany<T> parent, T item)
            where T : IApplicationModel;

        void DeSelect<T>(IHasMany<T> parent, T item)
            where T : IApplicationModel;

        void ClearSelection();

        ICollection<T> GetSelection<T>(IHasMany<T> parent)
            where T : IApplicationModel;
    }
}
