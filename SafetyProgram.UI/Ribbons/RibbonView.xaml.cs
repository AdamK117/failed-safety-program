using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;
using Fluent;
using SafetyProgram.Base;

namespace SafetyProgram.UI
{
    public sealed partial class RibbonView : Ribbon
    {
        private readonly IRibbonViewModel viewModel;

        public RibbonView(IRibbonViewModel viewModel)
        {
            Helpers.NullCheck(viewModel);

            this.viewModel = viewModel;
            this.DataContext = viewModel;
            InitializeComponent();

            contextualGroup.Visibility = Visibility.Visible;

            //Add document tabs that may already be there
            foreach (RibbonTabItem ribbonTab in viewModel.DocumentRibbonTabs)
            {
                this.SelectedTabItem = ribbonTab;
                this.Tabs.Add(ribbonTab);
            }

            //Add a handler to the current contextual tabs.
            viewModel.ContextualRibbonTabs.CollectionChanged +=
                ContextualRibbonTabs_CollectionChanged;

            //HOLDER CHANGES.
            //If the document ribbon tabs collection changes.
            viewModel.DocumentRibbonTabsHolderChanged += 
                (sender, newDocumentRibbonTabs) =>
                {
                    //De-group and clear the list.
                    foreach (RibbonTabItem ribbonTab in this.Tabs)
                    {
                        ribbonTab.Group = null;
                    }
                    this.Tabs.Clear();

                    if (viewModel.DocumentRibbonTabs != null)
                    {
                        //Add the new tabs in.
                        foreach (RibbonTabItem documentRibbonTab in viewModel.DocumentRibbonTabs)
                        {
                            this.SelectedTabItem = documentRibbonTab;
                            this.Tabs.Add(documentRibbonTab);
                        }
                    }
                };

            //If the contextual ribbon tabs holder changes (normally when a new document is opened).
            viewModel.ContextualRibbonTabsHolderChanged += 
                (sender, newContextualTabs) =>
                {
                    if (viewModel.ContextualRibbonTabs != null)
                    {
                        viewModel.ContextualRibbonTabs.CollectionChanged +=
                            ContextualRibbonTabs_CollectionChanged;
                    }
                };                   
        }

        void ContextualRibbonTabs_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (RibbonTabItem ribbonTab in e.NewItems)
                    {
                        //Add the tab to a default contextual group.
                        ribbonTab.Group = contextualGroup;
                        //Add the tab to the ribbon.
                        this.Tabs.Add(ribbonTab);
                        //Select (focus) the tab.
                        this.SelectedTabItem = ribbonTab;
                    }                  
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (RibbonTabItem ribbonTab in e.OldItems)
                    {
                        this.Tabs.Remove(ribbonTab);
                    }
                    break;

                case NotifyCollectionChangedAction.Reset:
                    var placeHolderTabsHolder = new List<RibbonTabItem>(this.Tabs);

                    foreach (RibbonTabItem ribbonTab in placeHolderTabsHolder)
                    {
                        if (ribbonTab.Group == contextualGroup)
                        {
                            ribbonTab.Group = null;
                            this.Tabs.Remove(ribbonTab);
                        }
                    }
                    break;
            }
        }
    }
}
