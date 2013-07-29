using System;
using System.Collections.Generic;
using SafetyProgram.Core.Commands;
using SafetyProgram.UI.Document;

namespace SafetyProgram.UI
{
    public sealed class RibbonViewModel : IRibbonViewModel
    {
        private readonly IDocumentUiController documentUiController;

        public RibbonViewModel(ICoreCommands coreCommands, IDocumentUiController documentUiController)
        {
            this.coreCommands = coreCommands;
            this.documentUiController = documentUiController;
        }

        public ICollection<Fluent.RibbonTabItem> DocumentRibbonTabs
        {
            get { return documentUiController.DocumentRibbonTabs; }
        }

        public event EventHandler<Base.GenericPropertyChangedEventArg<ICollection<Fluent.RibbonTabItem>>> DocumentRibbonTabsHolderChanged;

        public System.Collections.ObjectModel.ObservableCollection<Fluent.RibbonTabItem> ContextualRibbonTabs
        {
            get { throw new NotImplementedException(); }
        }

        public event EventHandler<Base.GenericPropertyChangedEventArg<System.Collections.ObjectModel.ObservableCollection<Fluent.RibbonTabItem>>> ContextualRibbonTabsHolderChanged;

        private readonly ICoreCommands coreCommands;

        public Core.Commands.ICoreCommands Commands
        {
            get { return coreCommands; }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
    }
}
