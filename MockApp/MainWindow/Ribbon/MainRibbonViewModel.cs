using System;
using System.ComponentModel;
using MockApp.Commands;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace MockApp
{
    public sealed class MainRibbonViewModel<T> : IMainRibbonViewModel
        where T : IDocument
    {
        public MainRibbonViewModel(IHolderT<IMainWindowCommands> windowCommands, 
            IHolderT<T> mainContent)
        {
            if (windowCommands != null && mainContent != null)
            {
                this.windowCommands = windowCommands;
                this.windowCommands.ContentChanged += (sender, e) => PropertyChanged.Raise(this, "Commands");
            }
            else throw new ArgumentNullException();
        }

        private IHolderT<IMainWindowCommands> windowCommands;
        public IMainWindowCommands Commands
        {
            get
            {
                return windowCommands.Content;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
