using System;

namespace SafetyProgram.Commands
{
    public class NewICommand : CoshhWindowICommand
    {
        /// <summary>
        /// Constructs an instance of the "New Document" command.
        /// </summary>
        /// <param name="window">Window in which the new document will be added when called.</param>
        public NewICommand(CoshhWindow window)
            : base(window)
        {
            //Monitor changes in the CoshhWindow's service (affects CanNew())
            window.ServiceChanged += (service) => RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Can only execute if the current service allows the creation of new documents
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <returns></returns>
        public override bool CanExecute(object parameter)
        {
            return window.Service.CanNew() ? true : false;
        }

        /// <summary>
        /// Attempts to create a new CoshhDocument.
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <exception cref="NotSupportedException">Thrown if Execute is called when CanExecute == false</exception>
        public override void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                if (window.Document != null)
                {
                    try
                    {
                        window.Service.Close(window.Document);
                        window.Document = null;
                    }
                    catch(ArgumentException)
                    {
                        //Close operation was cancelled in some way. Discontinue execution.
                        return;
                    }                    
                }
                window.Document = window.Service.New();
            }
            else throw new NotSupportedException("Call to execute made when it cant execute (CanExecute() == false)");
        }
    }
}