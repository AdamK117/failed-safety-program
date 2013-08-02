using SafetyProgram.Base;
using SafetyProgram.Core.Commands;

namespace SafetyProgram.UI.Document
{
    /// <summary>
    /// Defines an implementation of a IDocumentContextViewModel.
    /// </summary>
    public sealed class DocumentContextMenuViewModel : IDocumentContextMenuViewModel
    {
        /// <summary>
        /// Construct an instance of a viewmodel for a document context menu view.
        /// </summary>
        /// <param name="commands"></param>
        public DocumentContextMenuViewModel(IDocumentICommands commands)
        {
            Helpers.NullCheck(commands);

            this.commands = commands;
        }

        private readonly IDocumentICommands commands;

        /// <summary>
        /// Get commands that act on the document.
        /// </summary>
        public IDocumentICommands Commands
        {
            get { return commands; }
        }
    }
}
