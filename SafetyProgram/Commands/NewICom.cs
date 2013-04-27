using System;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Base;

namespace SafetyProgram.Commands
{
    public sealed class NewICom : ExtendedICommand<IWindow<IDocument>>
    {
        /// <summary>
        /// Constructs an instance of the "New Document" command.
        /// </summary>
        /// <param name="window">Window in which the new document will be added when called.</param>
        public NewICom(IWindow<IDocument> window)
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
            return data.Service.CanNew() ? true : false;
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
                if (data.Document != null)
                {
                    try
                    {
                        data.Service.Close(data.Document);
                        data.Document = null;
                    }
                    catch(ArgumentException)
                    {
                        throw;
                    }                    
                }
                data.Document = data.Service.New();
            }
            else throw new NotSupportedException("Call to execute made when it cant execute (CanExecute() == false)");
        }
    }
}