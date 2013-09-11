using Fluent;
using SafetyProgram.Base;

namespace SafetyProgram.Document.Ribbons
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
