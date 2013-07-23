using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Fluent;
using SafetyProgram.MainWindowUi.Commands;

namespace SafetyProgram.MainWindowUi.Ribbons
{
    public sealed class MainRibbonViewModel : IMainRibbonViewModel
    {
        public MainRibbonViewModel(ICollection<RibbonTabItem> documentRibbonTabs,
            ObservableCollection<RibbonTabItem> contextualRibbonTabs,
            IMainWindowCommands windowCommands)
        {
        }

        public ICollection<Fluent.RibbonTabItem> DocumentRibbonTabs
        {
            get { throw new NotImplementedException(); }
        }

        public event EventHandler<Base.GenericPropertyChangedEventArg<ICollection<Fluent.RibbonTabItem>>> DocumentRibbonTabsHolderChanged;

        public System.Collections.ObjectModel.ObservableCollection<Fluent.RibbonTabItem> ContextualRibbonTabs
        {
            get { throw new NotImplementedException(); }
        }

        public event EventHandler<Base.GenericPropertyChangedEventArg<System.Collections.ObjectModel.ObservableCollection<Fluent.RibbonTabItem>>> ContextualRibbonTabsHolderChanged;

        public Commands.IMainWindowCommands Commands
        {
            get { throw new NotImplementedException(); }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
    }
}
