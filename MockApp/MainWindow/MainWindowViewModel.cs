using System;
using System.ComponentModel;
using System.Windows.Controls;
using Fluent;
using MockApp.Commands;
using SafetyProgram.Base;

namespace MockApp
{
    public sealed class MainWindowViewModel : IMainWindowViewModel
    {
        public MainWindowViewModel(Ribbon ribbon,
            IHolderT<Control> content,
            IHolderT<IMainWindowCommands> commands
            )
        {
            if (ribbon == null ||
                content == null ||
                commands == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                this.ribbon = ribbon;
                this.content = content;
                this.commands = commands;

                content.ContentChanged += (sender, e) => PropertyChanged.Raise(this, "Content");
                commands.ContentChanged += (sender, e) => PropertyChanged.Raise(this, "Commands");
            }
        }

        private readonly Ribbon ribbon;
        public Ribbon Ribbon
        {
            get { return ribbon; }
        }

        private readonly IHolderT<Control> content;
        public Control Content
        {
            get { return content.Content; }
        }

        private readonly IHolderT<IMainWindowCommands> commands;
        public IMainWindowCommands Commands
        {
            get { return commands.Content; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
