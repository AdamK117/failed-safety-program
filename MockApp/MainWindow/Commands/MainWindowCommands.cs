using System;
using System.Collections.Generic;
using System.Windows.Input;
using SafetyProgram.Base.Interfaces;

namespace MockApp.Commands
{
    internal sealed class MainWindowCommands<T> : IMainWindowCommands
    {
        public MainWindowCommands(IHolderT<IIOService<T>> service,
            IEditableHolderT<T> contentHolder)
        {
            if (service == null ||
                contentHolder == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                newCommand = new NewICom<T>(service, contentHolder);
            }
        }

        private readonly ICommand newCommand;
        public ICommand New
        {
            get { return newCommand; }
        }

        public List<InputBinding> Hotkeys
        {
            get { throw new NotImplementedException(); }
        }
    }
}
