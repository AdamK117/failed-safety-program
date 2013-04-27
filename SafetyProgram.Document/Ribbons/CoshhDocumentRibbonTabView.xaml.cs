using Fluent;

namespace SafetyProgram.Document.Ribbons
{
    /// <summary>
    /// Interaction logic for CoshhDocumentRibbonTabView.xaml
    /// </summary>
    internal sealed partial class CoshhDocumentRibbonTabView : RibbonTabItem
    {
        private readonly CoshhDocumentRibbonTab viewModel;

        public CoshhDocumentRibbonTabView(CoshhDocumentRibbonTab viewModel)
        {
            this.viewModel = viewModel;
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
