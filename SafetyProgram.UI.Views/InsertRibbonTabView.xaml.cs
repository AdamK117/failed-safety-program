using Fluent;
using SafetyProgram.Base.FSharp;

namespace SafetyProgram.UI.Views
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