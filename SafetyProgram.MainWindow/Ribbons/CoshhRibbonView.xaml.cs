using System;
using System.Collections.Specialized;
using System.Diagnostics;
using Fluent;

namespace SafetyProgram.MainWindow.Ribbons
{
    public sealed partial class CoshhRibbonView : Ribbon
    {
        private readonly ICoshhRibbonViewModel viewModel;

        public CoshhRibbonView(ICoshhRibbonViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException();
            else
            {
                this.viewModel = viewModel;

                this.DataContext = viewModel;
                InitializeComponent();

                viewModel.RibbonTabs.CollectionChanged += RibbonTabs_CollectionChanged;
                viewModel.PropertyChanged += (sender, propertyChanged) =>
                    {
                        if (propertyChanged.PropertyName == "RibbonTabs")
                        {
                            this.Tabs.Clear();
                            viewModel.RibbonTabs.CollectionChanged += RibbonTabs_CollectionChanged;
                        }
                    };
            }
            
        }

        void RibbonTabs_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (RibbonTabItem ribbonTab in e.NewItems)
                    {
                        this.Tabs.Add(ribbonTab);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (RibbonTabItem ribbonTab in e.OldItems)
                    {
                        Debug.Assert(this.Tabs.Contains(ribbonTab), "WARNING: The IWindows ribbon does not contain a tab the IDocument tried to remove.");
                        this.Tabs.Remove(ribbonTab);
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    this.Tabs.Clear();
                    break;
            }
            Debug.Assert(this.Tabs.Count == viewModel.RibbonTabs.Count, "WARNING: There's a difference between the ribbon tabs in the window vs the document");
        }
    }
}
