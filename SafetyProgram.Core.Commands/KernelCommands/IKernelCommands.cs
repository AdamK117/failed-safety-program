using SafetyProgram.Core.Models;

namespace SafetyProgram.Core.Commands.KernelCommands
{
    public interface IKernelCommands
    {
        /// <summary>
        /// Returns true if the Undo method may be called.
        /// </summary>
        /// <returns>True if the undo method may be called.</returns>
        bool CanUndo();

        /// <summary>
        /// Undo the last invoked command.
        /// </summary>
        void Undo();

        /// <summary>
        /// Returns true if the Redo method may be called.
        /// </summary>
        /// <returns>True if the redo method may be called.</returns>
        bool CanRedo();

        /// <summary>
        /// Redo the last undone invoked command.
        /// </summary>
        void Redo();

        /// <summary>
        /// Returns true if the specified item may be deleted from its parent collection.
        /// </summary>
        /// <typeparam name="T">The type of item being deleted.</typeparam>
        /// <param name="parent">The parent holder of the item.</param>
        /// <param name="item">The item to delete.</param>
        /// <returns>True if the item may be deleted from the specified parent.</returns>
        bool CanDelete<T>(IHasManyT<T> parent, T item)
            where T : IApplicationModel;

        /// <summary>
        /// Delete the item from its specified parent.
        /// </summary>
        /// <typeparam name="T">The type of item being deleted.</typeparam>
        /// <param name="parent">The parent holder of the item.</param>
        /// <param name="item">The item to delete.</param>
        void Delete<T>(IHasManyT<T> parent, T item)
            where T : IApplicationModel;

        /// <summary>
        /// Returns true if the specified item may be inserted into a parent collection.
        /// </summary>
        /// <typeparam name="T">The type of item to insert.</typeparam>
        /// <param name="parent">The new parent holder of the item.</param>
        /// <param name="item">The item to insert.</param>
        /// <returns>True if the item may be inserted into the specified target.</returns>
        bool CanInsert<T>(IHasManyT<T> parent, T item)
            where T : IApplicationModel;

        /// <summary>
        /// Insert an item into the specified target.
        /// </summary>
        /// <typeparam name="T">The type of item to insert.</typeparam>
        /// <param name="parent">The target for insertion.</param>
        /// <param name="item">The item to insert.</param>
        void Insert<T>(IHasManyT<T> parent, T item)
            where T : IApplicationModel;

        /// <summary>
        /// Returns true if the item may be inserted into the specified target 
        /// at the specified index location.
        /// </summary>
        /// <typeparam name="T">The type of item to insert.</typeparam>
        /// <param name="parent">The target for the insertion.</param>
        /// <param name="item">The item to insert.</param>
        /// <param name="index">The index at which to insert the item.</param>
        /// <returns>True if the item may be inserted at the specified location in the target.</returns>
        bool CanInsert<T>(IHasManyT<T> parent, T item, int index)
            where T : IApplicationModel;

        /// <summary>
        /// Insert an item at the specified index in the target.
        /// </summary>
        /// <typeparam name="T">The type of item to insert.</typeparam>
        /// <param name="parent">The target for insertion.</param>
        /// <param name="item">The item to insert.</param>
        /// <param name="index">The location of the insertion.</param>
        void Insert<T>(IHasManyT<T> parent, T item, int index)
            where T : IApplicationModel;
    }
}
