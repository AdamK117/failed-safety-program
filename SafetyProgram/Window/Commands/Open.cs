using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SafetyProgram.Window.Commands
{
    public class Open : WindowCommandBase
    {
        public Open(CoshhWindow window)
            : base(window)
        { canExecute = false; RaiseCanExecuteChanged(); }

        /// <summary>
        /// Can execute at all times (will prompt user to save etc.)
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>Always returns true</returns>
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            window.Load();
        }
    }
}
