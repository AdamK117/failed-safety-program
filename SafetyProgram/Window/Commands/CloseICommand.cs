using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SafetyProgram.Window.Commands
{
    public class CloseICommand : WindowICommandBase
    {
        public CloseICommand(CoshhWindow window)
            : base(window)
        {
            window.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(window_PropertyChanged);
        }

        void window_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // Check if the appropriate property (Document) has changed and if it's pertinent to this command. 
            // If it is then change canExecute and raise its INPC so UI can redraw.
            if (e.PropertyName == "Document") { RaiseCanExecuteChanged(); }
        }

        public override bool CanExecute(object parameter)
        {
            return (window.Document == null) ? false : true;
        }

        public override void Execute(object parameter)
        {
            window.Commands.Close();
        }
    }
}
