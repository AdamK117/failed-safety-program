using System;

namespace SafetyProgram.Commands
{
    public class CloseICommand : CoshhWindowICommand
    {
        public CloseICommand(CoshhWindow window)
            : base(window)
        {
            //Monitor changes in the CoshhWindow's CoshhDocument. Closed (null) documents can't be closed.
            window.DocumentChanged += (document) => RaiseCanExecuteChanged();
        }

        /// <summary>
        /// May only close a document if there is one actually open
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <returns></returns>
        public override bool CanExecute(object parameter)
        {            
            return window.Document == null ? false : true;
        }

        /// <summary>
        /// Closes the current document.
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <exception cref="NotSupportedException">Thrown if Execute is called but CanExecute == false</exception>
        public override void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                try
                {
                    window.Service.Close(window.Document);
                    window.Document = null;
                }
                catch (ArgumentException)
                {
                    //Closing the document was cancelled for some reason.
                }   
            }
            else throw new NotSupportedException("Call to execute made when it cant execute (CanExecute() == false)");
        }
    }
}
