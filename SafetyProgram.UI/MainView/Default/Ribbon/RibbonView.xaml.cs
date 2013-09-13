using Fluent;
using SafetyProgram.Base;

namespace SafetyProgram.UI.MainView.Default
{
    internal sealed partial class RibbonView : Ribbon
    {
        private readonly IRibbonViewModel viewModel;

        public RibbonView(IRibbonViewModel viewModel)
        {
            Helpers.NullCheck(viewModel);

            this.viewModel = viewModel;
            this.DataContext = viewModel;

            this.viewModel
                .RibbonTabs
                .CollectionChanged +=
                    (s, e) => ribbonTabsCollectionChanged();

            ribbonTabsCollectionChanged();

            InitializeComponent();
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
