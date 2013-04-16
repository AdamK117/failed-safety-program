using System;
using System.IO;
using System.Windows;

namespace SafetyProgram.Commands
{
    public class OpenICommand : CoshhWindowICommand
    {
        /// <summary>
        /// Constructs a command that opens a CoshhDocument into the CoshhWindow using the CoshhWindow's service
        /// </summary>
        /// <param name="window">The CoshhWindow that will load the CoshhDocument</param>
        public OpenICommand(CoshhWindow window)
            : base(window)
        {
            //Monitor changes in the CoshhWindow's service (affects CanLoad()).
            window.ServiceChanged += (service) => RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Can only execute if the CoshhWindow's service allows loading.
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <returns></returns>
        public override bool CanExecute(object parameter)
        {
            return window.Service.CanLoad() ? true : false;
        }

        /// <summary>
        /// Attempts to open a new CoshhDocument using the CoshhWindow's service.
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <exception cref="NotSupportedException">Thrown if Execute is called when CanExecute is false.</exception>
        public override void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                //If theres a document open, close it.
                if (window.Document != null)
                {
                    try
                    {
                        window.Service.Close(window.Document);
                        window.Document = null;
                    }
                    catch (ArgumentException)
                    {
                        //Closing the document cancelled out for some reason. Discontinue execution.
                        return;
                    }                    
                }

                //Try to load a CoshhDocument using the service.
                try
                {
                    window.Document = window.Service.Load();
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("File specified was not found");
                }
                catch (ArgumentException)
                {
                    //Loading a CoshhDocument was cancelled for some reason.
                }                
            }
            else throw new NotSupportedException("Call to execute made when it cant execute (CanExecute() == false)");          
        }
    }
}
