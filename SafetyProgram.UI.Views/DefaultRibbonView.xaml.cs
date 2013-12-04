using Fluent;
using SafetyProgram.Base;
using SafetyProgram.UI.ViewModels;

namespace SafetyProgram.UI.Views
{
    public sealed partial class DefaultRibbonView : Ribbon
    {
        private readonly IRibbonViewModel viewModel;

        public DefaultRibbonView(IRibbonViewModel viewModel)
        {
            Helpers.NullCheck(viewModel);

            this.viewModel = viewModel;
            this.DataContext = viewModel;
            viewModel.RibbonTabs.CollectionChanged += RibbonTabs_CollectionChanged;
            viewModel.PropertyChanged += viewModel_PropertyChanged;

            ribbonTabsCollectionChanged();

            InitializeComponent();
        }

        void viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ribbonTabsCollectionChanged();
        }

        void RibbonTabs_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ribbonTabsCollectionChanged();
        }

        private void ribbonTabsCollectionChanged()
        {
            this.Tabs.Clear();
            foreach (RibbonTabItem ribbonTab in this.viewModel.RibbonTabs)
            {
                this.SelectedTabItem = ribbonTab;
                this.Tabs.Add(ribbonTab);
            }
        }
    }
}
