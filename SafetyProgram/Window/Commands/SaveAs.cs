using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SafetyProgram.Window.Commands
{
    public class SaveAs : WindowCommandBase
    {
        public SaveAs(CoshhWindow window)
            : base(window)
        { }

        public override bool CanExecute(object parameter)
        {
            if (window.Document == null) return false;
            else return true;
        }

        public override void Execute(object parameter)
        {
            window.SaveAs();
        }
    }
}
