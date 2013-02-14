namespace SafetyProgram.ICommands
{
    public class NewFileICommand : ActiveDataICommandsBase
    {
        public NewFileICommand() : base() 
        {
            canExecute = true;
        }

        public override void Execute(object parameter)
        {
            currentlyOpen.Service.NewFile();
        }

    }
}
