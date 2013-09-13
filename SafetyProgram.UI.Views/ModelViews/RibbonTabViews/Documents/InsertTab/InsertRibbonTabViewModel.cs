using SafetyProgram.Base;
using SafetyProgram.Core.Commands.ICommands;
using SafetyProgram.Core.Models;

namespace SafetyProgram.UI.Views.ModelViews.RibbonTabViews.Documents
{
    /// <summary>
    /// Defines a standard implementation of an IInsertRibbonTabViewModel.
    /// </summary>
    public sealed class InsertRibbonTabViewModel : IInsertRibbonTabViewModel
    {
        /// <summary>
        /// Construct an instance of a viewmodel for an insert ribbon.
        /// </summary>
        /// <param name="commands">A set of commands that act on the document that items will be inserted into.</param>
        public InsertRibbonTabViewModel(IDocument model, 
            ICommandInvoker commandInvoker)
        {
            Helpers.NullCheck(model, commandInvoker);

            this.Commands = new DocumentICommands(
                model,
                commandInvoker);
        }

        /// <summary>
        /// Get a group of commands that act on the current document.
        /// </summary>
        public IDocumentICommands Commands { get; private set; }
    }
}
