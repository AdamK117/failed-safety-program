using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SafetyProgram.Window.Commands
{
    public class Close : WindowCommandBase
    {
        public Close(CoshhWindow window)
            : base(window)
        { }

        public override bool CanExecute(object parameter)
        {
            return window.Document == null ? false : true;
        }

        public override void Execute(object parameter)
        {
            window.Close();
        }
    }
}
