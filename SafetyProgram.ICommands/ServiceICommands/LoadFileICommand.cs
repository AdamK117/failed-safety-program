namespace SafetyProgram.ICommands
{
    public class LoadFileICommand : ActiveDataICommandsBase
    {
        public LoadFileICommand() : base()
        {
            canExecute = true;
        }

        public override void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                currentlyOpen.Service.LoadFile();
            }            
        }
    }
}
