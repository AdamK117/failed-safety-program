using Fluent;

namespace SafetyProgram.Ribbons
{
    /// <summary>
    /// Interaction logic for RibbonView.xaml
    /// </summary>
    internal sealed partial class CoshhRibbonView : Ribbon
    {
        private readonly CoshhRibbon viewModel;

        /// <summary>
        /// Construct a CoshhRibbon view (this is the primary CoshhWindow ribbon).
        /// </summary>
        /// <param name="viewModel"></param>
        public CoshhRibbonView(CoshhRibbon viewModel)
        {
            this.viewModel = viewModel;

            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
