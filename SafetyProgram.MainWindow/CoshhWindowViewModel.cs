using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using Fluent;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.MainWindow
{
    public sealed class CoshhWindowViewModel : ICoshhWindowViewModel
    {
        public CoshhWindowViewModel(IHolder<Ribbon> ribbonViewHolder,
            IHolder<Control> contentViewHolder,
            List<InputBinding> hotkeys)
        {
            Helpers.NullCheck(ribbonViewHolder, contentViewHolder, hotkeys);

            this.ribbonViewHolder = ribbonViewHolder;
            this.controlViewHolder = contentViewHolder;
            this.hotkeys = hotkeys;

            //Monitor if the ribbon view changes.
            this.ribbonViewHolder.ContentChanged += 
                (sender, newRibbon) => PropertyChanged.Raise(this, "RibbonView");

            //Monitor if the content (document) changes.
            this.controlViewHolder.ContentChanged += 
                (sender, newContent) => PropertyChanged.Raise(this, "ContentView");
        }

        private readonly IHolder<Ribbon> ribbonViewHolder;
        public Ribbon RibbonView
        {
            get { return ribbonViewHolder.Content; }
        }

        private readonly IHolder<Control> controlViewHolder;
        public Control ContentView
        {
            get { return controlViewHolder.Content; }
        }

        private readonly List<InputBinding> hotkeys;
        public List<InputBinding> Hotkeys
        {
            get { return hotkeys; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
