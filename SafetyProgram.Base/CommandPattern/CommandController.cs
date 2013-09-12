using System;
using System.Collections.Generic;

namespace SafetyProgram.Base
{
    /// <summary>
    /// Defines an ICommandInvoker. A class that will execute and buffer IInvokedCommands.
    /// </summary>
    /// <remarks>Book: GoF 'Command Pattern'.</remarks>
    public class CommandController : ICommandController
    {
        //Holder for 'redo' operations.
        protected readonly Stack<IInvokedCommand> futureCommands;

        //Holder for 'undo' operations.
        protected readonly Stack<IInvokedCommand> pastCommands;

        /// <summary>
        /// Construct a new instance of the CommandInvoker class.
        /// </summary>
        public CommandController()
        {
            futureCommands = new Stack<IInvokedCommand>();
            pastCommands = new Stack<IInvokedCommand>();
        }

        /// <summary>
        /// Invoke an IInvokedCommand. Once executed, the command may be reversed with the 'Undo' method.
        /// </summary>
        /// <param name="command">The IInvokedCommand to execute.</param>
        public void InvokeCommand(IInvokedCommand command)
        {
            //Execute a new command:
            //  Clear future commands (this command represents a new 'branch' in the undo-redo chain)
            //  Execute the command
            //  Add the command into the past
            invoker(() =>
            {
                futureCommands.Clear();
                command.Execute();
                pastCommands.Push(command);
            });
        }

        /// <summary>
        /// Perform an undo operation. This will call UnExecute() on the last executed IInvokedCommand.
        /// </summary>
        public void Undo()
        {
            //Undo a command:
            //  Pop a command off the 'past' stack.
            //  Unexecute that command.
            //  Put the unexecuted command onto the 'future' stack.
            invoker(() =>
            {
                var command = pastCommands.Pop();
                command.UnExecute();
                futureCommands.Push(command);
            });
        }

        /// <summary>
        /// Check if the Undo command is available.
        /// </summary>
        /// <returns>The availability of the Undo command.</returns>
        public bool CanUndo()
        {
            return (pastCommands.Count > 0) ? true : false;
        }

        /// <summary>
        /// Get an event that monitors if the CanUndo state of the CommandInvoker has changed.
        /// </summary>
        public event EventHandler<
            GenericPropertyChangedEventArg<
                bool>> CanUndoChanged;

        /// <summary>
        /// Perform a Redo operation. This will call Execute() on the last undone IInvokedCommand.
        /// </summary>
        public void Redo()
        {
            //Redo a command
            //  Pop a command off the 'future' stack.
            //  Execute the command.
            //  Put the executed command into the 'past' stack.
            invoker(() =>
            {
                var command = futureCommands.Pop();
                command.Execute();
                pastCommands.Push(command);
            });
        }

        /// <summary>
        /// Check if the Redo command is available.
        /// </summary>
        /// <returns>The availability of the Redo command.</returns>
        public bool CanRedo()
        {
            return (futureCommands.Count > 0) ? true : false;
        }
     
        /// <summary>
        /// Get an event that monitors if the CanRedo state of the CommandInvoker has changed.
        /// </summary>
        public event EventHandler<
            GenericPropertyChangedEventArg<
                bool>> CanRedoChanged;

        /// <summary>
        /// Invoke an action. Checks if the CanUndo and CanRedo states changed during invokation.
        /// </summary>
        /// <param name="invokedAction">The action that the CommandInvoker will invoke.</param>
        private void invoker(Action invokedAction)
        {
            //Invokes an action, checking if the CanUndo and CanRedo states change after invokation.

            //Store pre-invocation CanUndo/CanRedo states
            bool canUndoBefore = CanUndo();
            bool canRedoBefore = CanRedo();

            //Invoke the action
            invokedAction();

            //Check if the states have changed. If they have, trigger the necessay events.
            {
                bool canUndoAfter = CanUndo();
                if (canUndoAfter != canUndoBefore)
                {
                    CanUndoChanged.Raise(this, canUndoAfter);
                }
            }
            {
                bool canRedoAfter = CanRedo();
                if (canRedoAfter != canRedoBefore)
                {
                    CanRedoChanged.Raise(this, canRedoAfter);
                }
            }
        }
    }
}
