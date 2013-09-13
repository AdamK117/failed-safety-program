using Fluent;
using SafetyProgram.Base;

namespace SafetyProgram.UI.Document
{
    /// <summary>
    /// Interaction logic for CoshhDocumentRibbonTabView.xaml
    /// </summary>
    internal sealed partial class InsertRibbonTabView : RibbonTabItem
    {
        public InsertRibbonTabView(IInsertRibbonTabViewModel viewModel)
        {
            Helpers.NullCheck(viewModel);

            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
