using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using Fluent;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Core.Commands;
using SafetyProgram.UI.Document;

namespace SafetyProgram.UI
{
    public sealed class MainViewModel : 
        IMainViewModel
    {
        public MainViewModel(Ribbon ribbonView,
            Control contentView)
        {
            Helpers.NullCheck(ribbonView,
                contentView);

            this.RibbonView = ribbonView;
            this.ContentView = contentView;
        }

        public Ribbon RibbonView { get; private set; }

        public Control ContentView { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
