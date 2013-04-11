﻿namespace SafetyProgram.MainWindow.Commands
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
            window.DocumentChanged += (document) => RaiseCanExecuteChanged();
        }

        public override bool CanExecute(object parameter)
        {
            return window.Document == null ? false : true;
        }

        public override void Execute(object parameter)
        {
            if (window.Service.Save())
            {
                window.Document.Edited = false;
            }
        }
    }
}