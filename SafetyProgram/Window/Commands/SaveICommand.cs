using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SafetyProgram.Window.Commands
{
    /// <summary>
    /// ICommand for Saving the current document
    /// </summary>
    public class SaveICommand : WindowICommandBase
    {
        /// <summary>
        /// ICommand for saving the current document (of the window)
        /// </summary>
        /// <param name="window">Instance of the window (which contains the document)</param>
        public SaveICommand(CoshhWindow window)
            : base(window) 
        {
            window.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(window_PropertyChanged);
        }

        void window_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RaiseCanExecuteChanged();
        }

        public override bool CanExecute(object parameter)
        {
            return window.Document == null ? false : true;
        }

        public override void Execute(object parameter)
        {
            window.Commands.Save();
        }
    }
}
