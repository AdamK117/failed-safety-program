using System;

namespace SafetyProgram.Base
{
    /// <summary>
    /// Defines an interface for a command controller. The command controller
    /// is a specialized commandinvoker that may invoke commands and perfom undo 
    /// and redo operations.
    /// </summary>
    public interface ICommandController : ICommandInvoker
    {
        /// <summary>
        /// Undo the last invoked command.
        /// </summary>
        void Undo();

        /// <summary>
        /// Returns true if the undo method is in a state
        /// where it may be called.
        /// </summary>
        /// <returns></returns>
        bool CanUndo();

        /// <summary>
        /// Occurs when the CanUndo state has changed.
        /// </summary>
        event EventHandler<
            GenericPropertyChangedEventArg<
                bool>> CanUndoChanged;

        /// <summary>
        /// Redo the last invoked command.
        /// </summary>
        void Redo();

        /// <summary>
        /// Returns true if the redo method is in a state 
        /// where it may be called.
        /// </summary>
        /// <returns></returns>
        bool CanRedo();

        /// <summary>
        /// Occurs when the CanRedo state has changed.
        /// </summary>
        event EventHandler<
            GenericPropertyChangedEventArg<
                bool>> CanRedoChanged;
    }
}
