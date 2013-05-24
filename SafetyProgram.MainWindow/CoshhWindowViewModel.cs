using System;
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
            if (ribbonViewHolder == null ||
                contentViewHolder == null ||
                hotkeys == null)
                throw new ArgumentNullException();
            else
            {
                this.ribbonViewHolder = ribbonViewHolder;
                this.controlViewHolder = contentViewHolder;
                this.hotkeys = hotkeys;

                this.ribbonViewHolder.ContentChanged += (sender, newRibbon) => PropertyChanged.Raise(this, "RibbonView");
                this.controlViewHolder.ContentChanged += (sender, newContent) => PropertyChanged.Raise(this, "ContentView");
            }
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
