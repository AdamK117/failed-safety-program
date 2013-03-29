using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SafetyProgram.Window.Commands
{
    /// <summary>
    /// ICommand for Saving the current document
    /// </summary>
    public class Save : WindowCommandBase
    {
        /// <summary>
        /// ICommand for saving the current document (of the window)
        /// </summary>
        /// <param name="window">Instance of the window (which contains the document)</param>
        public Save(CoshhWindow window)
            : base(window) { }

        public override bool CanExecute(object parameter)
        {
            if (window.Document == null) return false;
            else return true;
        }

        public override void Execute(object parameter)
        {
            window.Save();
        }
    }
}
