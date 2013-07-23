using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Fluent;
using SafetyProgram.Base;
using SafetyProgram.DocumentUi;
using SafetyProgram.DocumentUi.View;
using SafetyProgram.IO;
using SafetyProgram.MainWindowUi.Commands;
using SafetyProgram.MainWindowUi.Ribbons;
using SafetyProgram.Models;

namespace SafetyProgram.MainWindowUi.View
{
    public sealed class MainWindowViewModel : IMainWindowViewModel
    {
        public MainWindowViewModel(IWindowKernel window, IConfiguration configuration)
        {
            var commandInvoker = new CommandInvoker();

            IIOService<IDocument> docService = null;
            IDocument document = docService.Load();

            IMainWindowCommands windowCommands = new MainWindowICommands(
                window,
                docService,
                commandInvoker
            );

            Ribbon ribbon = new MainRibbonView(
                new MainRibbonViewModel(
                    null,
                    null,
                    windowCommands
                )
            );

            Control contentView = new DocumentView(
                new DocumentViewModel(
                    document,
                    configuration,
                    commandInvoker
                )
            );
        }

        public Fluent.Ribbon RibbonView
        {
            get { throw new NotImplementedException(); }
        }

        public System.Windows.Controls.Control ContentView
        {
            get { throw new NotImplementedException(); }
        }

        private readonly IMainWindowCommands windowCommands;

        public List<System.Windows.Input.InputBinding> Hotkeys
        {
            get { return windowCommands.Hotkeys; }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
    }
}
