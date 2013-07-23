using System;
using System.Collections.Generic;
using System.Windows.Input;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.IO;
using SafetyProgram.Models;

namespace SafetyProgram.MainWindowUi.Commands
{
    /// <summary>
    /// Defines an implementation for IWindowCommands. The commands available to the window.
    /// </summary>
    public sealed class MainWindowICommands : IMainWindowCommands
    {
        public MainWindowICommands(IWindowKernel windowKernel, 
            IIOService<IDocument> service, 
            ICommandInvoker commandInvoker)
        {
            //TODO: Implement commands
        }

        public ICommand New
        {
            get { throw new NotImplementedException(); }
        }

        public ICommand Open
        {
            get { throw new NotImplementedException(); }
        }

        public ICommand Save
        {
            get { throw new NotImplementedException(); }
        }

        public ICommand SaveAs
        {
            get { throw new NotImplementedException(); }
        }

        public ICommand Close
        {
            get { throw new NotImplementedException(); }
        }

        public ICommand Undo
        {
            get { throw new NotImplementedException(); }
        }

        public ICommand Redo
        {
            get { throw new NotImplementedException(); }
        }

        public List<InputBinding> Hotkeys
        {
            get { throw new NotImplementedException(); }
        }
    }
}
