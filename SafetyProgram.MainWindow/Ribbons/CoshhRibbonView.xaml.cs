using Fluent;

namespace SafetyProgram.MainWindow.Ribbons
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
        public CoshhRibbonView(ICoshhRibbon viewModel)
        {
            //BUG: Datacontext of a Ribbon always goes to its parent RibbonWindow/Window regardless of setting it or not
            InitializeComponent();
        }
    }
}
