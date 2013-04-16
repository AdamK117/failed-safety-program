using SafetyProgram.BaseClasses;

namespace SafetyProgram.Commands
{
    public abstract class CoshhWindowICommand : BaseICommand
    {
        protected CoshhWindow window;

        /// <summary>
        /// Constructs a base ICommand that uses the CoshhWindow
        /// </summary>
        /// <param name="window">CoshhWindow using the ICommand</param>
        public CoshhWindowICommand(CoshhWindow window)
        {
            this.window = window;
        }
    }
}
