namespace SafetyProgram.ICommands
{
    public class NewFileICommand : DOMBase
    {
        public NewFileICommand() : base() 
        {
            canExecute = true;
        }

        public override void Execute(object parameter)
        {
            coshhWindow.New();
        }

    }
}
