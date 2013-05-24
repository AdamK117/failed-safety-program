using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Fluent;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.MainWindow.Commands;

namespace SafetyProgram.MainWindow.Ribbons
{
    public sealed class CoshhRibbonViewModel : ICoshhRibbonViewModel
    {
        public CoshhRibbonViewModel(IHolder<ObservableCollection<RibbonTabItem>> ribbonTabsHolder,
            IHolder<IWindowCommands> windowCommandsHolder)
        {
            if (ribbonTabsHolder == null ||
                windowCommandsHolder == null)
                throw new ArgumentNullException();
            else
            {
                this.ribbonTabsHolder = ribbonTabsHolder;
                this.windowCommandsHolder = windowCommandsHolder;

                this.ribbonTabsHolder.ContentChanged += (sender, newRibbonTabs) => PropertyChanged.Raise(this, "RibbonTabs");
                this.windowCommandsHolder.ContentChanged += (sender, newCommands) => PropertyChanged.Raise(this, "Commands");
            }
        }

        private readonly IHolder<ObservableCollection<RibbonTabItem>> ribbonTabsHolder;
        public ObservableCollection<RibbonTabItem> RibbonTabs
        {
            get { return ribbonTabsHolder.Content; }
        }

        private readonly IHolder<IWindowCommands> windowCommandsHolder;
        public IWindowCommands Commands
        {
            get { return windowCommandsHolder.Content; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
