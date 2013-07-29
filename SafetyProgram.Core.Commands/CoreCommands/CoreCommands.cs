using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Core.Commands.CoreCommands
{
    public sealed class CoreCommands : ICoreCommands
    {
        public CoreCommands(IApplicationKernel applicationKernel, ICommandInvoker commandInvoker)
        {
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
    }
}
