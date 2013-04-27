using System;
using System.Windows;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Base;

namespace SafetyProgram.Commands
{
    /// <summary>
    /// ICommand for Saving the current document
    /// </summary>
    public class SaveICom : ExtendedICommand<IWindow<IDocument>>
    {
        /// <summary>
        /// Construct a Save command which will save the CoshhWindow's document
        /// </summary>
        /// <param name="window">CoshhWindow which houses the CoshhDocument to be saved</param>
        public SaveICom(IWindow<IDocument> window)
            : base(window) 
        {
            //Monitor if the CoshhWindow's service has changed (Service.CanSave())
            window.ServiceChanged += (service) => RaiseCanExecuteChanged();

            //Monitor if the CoshhWindow's document has changed (Can't save a closed (null) document)
            window.DocumentChanged += (document) => RaiseCanExecuteChanged();
        }

        /// <summary>
        /// CanExecute if there is a CoshhDocument open in the CoshhWindow and the CoshhWindow's service allows saving of the document
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <returns></returns>
        public override bool CanExecute(object parameter)
        {
            return (data.Document != null && data.Service.CanSave(data.Document)) ? false : true;
        }

        /// <summary>
        /// Attempts to save the CoshhWindow's CoshhDocument.
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        public override void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                try
                {
                    data.Service.Save(data.Document);
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("Error: You do not have sufficient permissions to save to this location.");
                }
                catch (ArgumentException)
                {
                    //Saving was cancelled for some reason, discontinue ICommand execution.
                    return;
                }                
            }
            else throw new NotSupportedException("Call to execute made when it cant execute (CanExecute() == false)"); 
        }
    }
}