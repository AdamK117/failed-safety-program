using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using Fluent;
using SafetyProgram.Base;
using System.Windows;

namespace SafetyProgram.MainWindow.Ribbons
{
    public sealed partial class CoshhRibbonView : Ribbon
    {
        private readonly ICoshhRibbonViewModel viewModel;

        public CoshhRibbonView(ICoshhRibbonViewModel viewModel)
        {
            Helpers.NullCheck(viewModel);

            this.viewModel = viewModel;
            this.DataContext = viewModel;
            InitializeComponent();

            contextualGroup.Visibility = Visibility.Visible;

            //Add document tabs that may already be there
            foreach (RibbonTabItem ribbonTab in viewModel.DocumentRibbonTabs)
            {
                this.Tabs.Add(ribbonTab);
            }

            //If the document ribbon tabs change.
            viewModel.DocumentRibbonTabsChanged += 
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
                        foreach (RibbonTabItem documentRibbonTab in viewModel.DocumentRibbonTabs)
                        {
                            this.Tabs.Add(documentRibbonTab);
                        }
                    }
                };

            //If the contextual ribbon tabs collection changes
            viewModel.ContextualRibbonTabsChanged += 
                (sender, newContextualTabs) =>
                {
                    if (viewModel.ContextualRibbonTabs != null)
                    {
                        viewModel.ContextualRibbonTabs.CollectionChanged +=
                            ContextualRibbonTabs_CollectionChanged;
                    }
                    else
                    {
                        var removalList = new List<RibbonTabItem>();
                        foreach (RibbonTabItem ribbonTab in this.Tabs)
                        {
                            if (ribbonTab.Group == contextualGroup)
                            {
                                removalList.Add(ribbonTab);
                            }
                        }

                        foreach (RibbonTabItem contextualRibbonTab in removalList)
                        {
                            contextualRibbonTab.Group = null;
                            this.Tabs.Remove(contextualRibbonTab);
                        }
                    }
                };

            //Add a handler to the current contextual tabs.
            viewModel.ContextualRibbonTabs.CollectionChanged +=
                ContextualRibbonTabs_CollectionChanged;         
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
                    var removalList = new List<RibbonTabItem>();

                    foreach (RibbonTabItem ribbonTab in this.Tabs)
                    {
                        if (ribbonTab.Group == contextualGroup)
                        {
                            removalList.Add(ribbonTab);
                        }
                    }

                    removalList.ForEach(contextualTab => 
                        {
                            contextualTab.Group = null;
                            this.Tabs.Remove(contextualTab);
                        });
                    break;
            }
        }
    }
}
