using System;
using System.Collections.Generic;
using System.Windows.Input;
using SafetyProgram.Base;
using SafetyProgram.Core.Models;

namespace SafetyProgram.Core.Commands
{
    /// <summary>
    /// Defines an implementation for a command that deletes IDocumentObjects from
    /// the document contenets.
    /// </summary>
    internal sealed class DeleteIDocumentObjectICommand : ICommand
    {
        private readonly IDocument document;
        private readonly ICommandInvoker commandInvoker;

        /// <summary>
        /// Construct an instance of a command that deletes document contents.
        /// </summary>
        /// <param name="document">The document from which to delete contents.</param>
        /// <param name="commandInvoker">The invoker of the command.</param>
        public DeleteIDocumentObjectICommand(IDocument document,
            ICommandInvoker commandInvoker)
        {
            this.document = document;
            this.commandInvoker = commandInvoker;
        }

        /// <summary>
        /// Returns true if the deletion targets in the paramater are contained within
        /// the document.
        /// </summary>
        /// <param name="parameter">The documentobjects to delete.</param>
        /// <returns>True if the correct deletion targets are chosen.</returns>
        public bool CanExecute(object parameter)
        {
            var selection = (IEnumerable<IDocumentObject>)parameter;

            bool validSelection = true;

            foreach (IDocumentObject documentObject in selection)
            {
                if (!document.Items.Contains(documentObject))
                {
                    validSelection = false;
                    break;
                }
            }

            return validSelection;
        }

        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Delete the specified IDocumentObjects from the underlying IDocument model.
        /// </summary>
        /// <param name="parameter">The IDocumentObjects to delete.</param>
        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                var selection = (IEnumerable<IDocumentObject>)parameter;

                foreach (IDocumentObject documentObject in selection)
                {
                    document.Items.Remove(documentObject);
                }
            }
            else
            {
                throw new ArgumentException("The command was executed when CanExecute was false");
            }
        }
    }
}
