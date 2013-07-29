using System.Collections.Generic;
using System.Windows.Controls;
using Fluent;
using SafetyProgram.Core.Commands;
using SafetyProgram.UI.Document;

namespace SafetyProgram.UI
{
    public sealed class MainViewModel : IMainViewModel
    {
        public MainViewModel(ICoreCommands coreCommands, IDocumentUiController documentUiController)
        {
            this.ribbonView = new RibbonView(
                new RibbonViewModel(
                    coreCommands,
                    documentUiController
                )
            );

            this.contentView = documentUiController.View;

            this.coreCommands = coreCommands;
        }

        private readonly Ribbon ribbonView;

        public Fluent.Ribbon RibbonView
        {
            get { return ribbonView; }
        }

        private readonly Control contentView;

        public System.Windows.Controls.Control ContentView
        {
            get { return contentView; }
        }

        private ICoreCommands coreCommands;

        public List<System.Windows.Input.InputBinding> Hotkeys
        {
            get { return coreCommands.Hotkeys; }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
    }
}
