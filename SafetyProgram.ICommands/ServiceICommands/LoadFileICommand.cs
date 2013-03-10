namespace SafetyProgram.ICommands
{
    public class LoadFileICommand : DOMBase
    {
        public LoadFileICommand() : base()
        {
            canExecute = true;
        }

        public override void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                coshhWindow.Load();
            }            
        }
    }
}
