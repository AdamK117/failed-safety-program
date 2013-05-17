using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Commands
{
    internal class OpenICom<TContent> : ICommand
    {
        private IWindow<TContent> window;

        /// <summary>
        /// Constructs a command that opens a CoshhDocument into the CoshhWindow using the CoshhWindow's service
        /// </summary>
        /// <param name="window">The CoshhWindow that will load the CoshhDocument</param>
        public OpenICom(IWindow<TContent> window)
        {
            this.window = window;
            this.window.ServiceChanged += (sender, newProperty) => CanExecuteChanged.Raise(this);
        }

        /// <summary>
        /// Can only execute if the CoshhWindow's service allows loading.
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return window.Service.CanLoad() ? true : false;
        }

        /// <summary>
        /// Attempts to open a new CoshhDocument using the CoshhWindow's service.
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <exception cref="NotSupportedException">Thrown if Execute is called when CanExecute is false.</exception>
        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                //If theres a document open, close it.
                if (window.Content != null)
                {
                    try
                    {
                        window.Service.Disconnect();
                        window.Content = default(TContent);
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
                    window.Content = window.Service.Load();
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("File specified was not found");
                    throw;
                }               
            }
            else throw new NotSupportedException("Call to execute made when it cant execute (CanExecute() == false)");          
        }

        public event EventHandler CanExecuteChanged;
    }
}
