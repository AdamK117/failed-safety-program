using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SafetyProgram.Window.Commands
{
    public class SaveAsICommand : WindowICommandBase
    {
        public SaveAsICommand(CoshhWindow window)
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
            window.Commands.SaveAs();
        }
    }
}
