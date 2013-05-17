﻿using System;
using System.Collections.Generic;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Base
{
    /// <summary>
    /// Defines an ICommandInvoker. A class that will execute and buffer IInvokedCommands.
    /// </summary>
    public sealed class CommandInvoker : ICommandInvoker
    {
        private Stack<IInvokedCommand> futureCommands;
        private Stack<IInvokedCommand> pastCommands;

        public CommandInvoker()
        {
            futureCommands = new Stack<IInvokedCommand>();
            pastCommands = new Stack<IInvokedCommand>();
        }

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

        public bool CanUndo()
        {
            return (pastCommands.Count > 0) ? true : false;
        }

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

        public bool CanRedo()
        {
            return (futureCommands.Count > 0) ? true : false;
        }

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

        public event EventHandler<GenericPropertyChangedEventArg<bool>> CanUndoChanged;

        public event EventHandler<GenericPropertyChangedEventArg<bool>> CanRedoChanged;

        /// <summary>
        /// Invokes actions. Checks if the CanUndo and CanRedo states changed during invokation
        /// </summary>
        /// <param name="invokedAction"></param>
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
                if (canUndoBefore != canUndoAfter)
                {
                    CanUndoChanged.Raise(this, new GenericPropertyChangedEventArg<bool>(canUndoAfter));
                }
            }
            {
                bool canRedoAfter = CanRedo();
                if (canRedoBefore != canRedoAfter)
                {
                    CanRedoChanged.Raise(this, new GenericPropertyChangedEventArg<bool>(canRedoAfter));
                }
            }
        }
    }
}
