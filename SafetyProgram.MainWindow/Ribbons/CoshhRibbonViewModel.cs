using System;
using System.Collections.Generic;
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
        public CoshhRibbonViewModel(IHolder<IWindowCommands> windowCommandsHolder, 
            IHolder<ICollection<RibbonTabItem>> documentRibbonTabsHolder, 
            IHolder<ObservableCollection<RibbonTabItem>> contextualRibbonTabsHolder)
        {
            Helpers.NullCheck(documentRibbonTabsHolder, windowCommandsHolder, contextualRibbonTabsHolder);

            this.documentRibbonTabsHolder = documentRibbonTabsHolder;
            this.windowCommandsHolder = windowCommandsHolder;
            this.contextualRibbonTabsHolder = contextualRibbonTabsHolder;

            this.documentRibbonTabsHolder.ContentChanged +=
                (sender, newDocumentTabs) =>
                {
                    PropertyChanged.Raise(this, "DocumentRibbonTabs");
                    DocumentRibbonTabsHolderChanged.Raise(this, DocumentRibbonTabs);
                };

            this.windowCommandsHolder.ContentChanged += 
                (sender, newWindowCommands) => PropertyChanged.Raise(this, "Commands");

            this.contextualRibbonTabsHolder.ContentChanged += 
                (sender, newContextualTabs) => 
                {
                    PropertyChanged.Raise(this, "ContextualRibbonTabs");
                    ContextualRibbonTabsHolderChanged.Raise(this, ContextualRibbonTabs);
                };
        }

        private readonly IHolder<ICollection<RibbonTabItem>> documentRibbonTabsHolder;
        public ICollection<RibbonTabItem> DocumentRibbonTabs
        {
            get { return documentRibbonTabsHolder.Content; }
        }
        public event EventHandler<GenericPropertyChangedEventArg<ICollection<RibbonTabItem>>> DocumentRibbonTabsHolderChanged;

        private readonly IHolder<IWindowCommands> windowCommandsHolder;
        public IWindowCommands Commands
        {
            get { return windowCommandsHolder.Content; }
        }

        private readonly IHolder<ObservableCollection<RibbonTabItem>> contextualRibbonTabsHolder;
        public ObservableCollection<RibbonTabItem> ContextualRibbonTabs
        {
            get { return contextualRibbonTabsHolder.Content; }
        }
        public event EventHandler<GenericPropertyChangedEventArg<ObservableCollection<RibbonTabItem>>> ContextualRibbonTabsHolderChanged;

        public event PropertyChangedEventHandler PropertyChanged;


        

        
    }
}
