using Fluent;

namespace SafetyProgram.Ribbons
{
    /// <summary>
    /// Interaction logic for RibbonView.xaml
    /// </summary>
    internal sealed partial class CoshhRibbonView : Ribbon
    {
        /// <summary>
        /// Construct a CoshhRibbon view (this is the primary CoshhWindow ribbon).
        /// </summary>
        /// <param name="viewModel"></param>
        public CoshhRibbonView(CoshhRibbon viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
