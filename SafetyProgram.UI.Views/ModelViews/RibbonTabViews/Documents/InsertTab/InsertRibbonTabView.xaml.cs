using Fluent;
using SafetyProgram.Base;

namespace SafetyProgram.UI.Views.ModelViews.RibbonTabViews.Documents
{
    /// <summary>
    /// Interaction logic for CoshhDocumentRibbonTabView.xaml
    /// </summary>
    public sealed partial class InsertRibbonTabView : RibbonTabItem
    {
        public InsertRibbonTabView(IInsertRibbonTabViewModel viewModel)
        {
            Helpers.NullCheck(viewModel);

            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
