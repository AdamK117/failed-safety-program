using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SafetyProgram.Window.Commands
{
    public class New : WindowCommandBase
    {
        public New(CoshhWindow window)
            : base(window)
        { }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            window.New();
        }
    }
}
