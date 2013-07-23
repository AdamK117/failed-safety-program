using Fluent;

namespace SafetyProgram.DocumentUi.Ribbons
{
    /// <summary>
    /// Interaction logic for CoshhDocumentRibbonTabView.xaml
    /// </summary>
    internal sealed partial class DocumentRibbonTabView : RibbonTabItem
    {
        public DocumentRibbonTabView(IDocumentRibbonTabViewModel viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
