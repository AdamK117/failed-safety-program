using System.Windows.Input;
using SafetyProgram.Base;

namespace SafetyProgram.Core.Commands.ICommands
{
    public sealed class CoreCommands : 
        ICoreCommands
    {
        public CoreCommands(IApplicationKernel applicationKernel, 
            ICommandController commandController)
        {
            Helpers.NullCheck(applicationKernel, commandController);

            New = new NewICommand(applicationKernel);
            Open = new OpenICommand(applicationKernel);
            Save = new SaveICommand(applicationKernel);
            SaveAs = new SaveAsICommand(applicationKernel);
            Close = new CloseICommand(applicationKernel);
            Undo = new UndoICommand(commandController);
            Redo = new RedoICommand(commandController);
        }

        public ICommand New { get; private set; }

        public ICommand Open { get; private set; }

        public ICommand Save { get; private set; }

        public ICommand SaveAs { get; private set; }

        public ICommand Close { get; private set; }

        public ICommand Undo { get; private set; }

        public ICommand Redo { get; private set; }
    }
}
