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

            ribbonTabsCollectionChanged();

            viewModel.PropertyChanged += ((x,y) => ribbonTabsCollectionChanged());

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
