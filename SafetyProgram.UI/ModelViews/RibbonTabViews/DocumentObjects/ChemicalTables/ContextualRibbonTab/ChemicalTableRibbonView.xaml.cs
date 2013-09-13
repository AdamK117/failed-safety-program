using Fluent;
using SafetyProgram.Base;

namespace SafetyProgram.UI.ModelViews.RibbonTabViews.DocumentObjects.ChemicalTables
{
    /// <summary>
    /// Interaction logic for ChemicalTableRibbonView.xaml
    /// </summary>
    internal sealed partial class ChemicalTableRibbonView : RibbonTabItem
    {
        /// <summary>
        /// Construct an instance of a chemical table contextual ribbon tab view.
        /// </summary>
        /// <param name="viewModel">The underlying viewmodel for this contextual ribbon tab.</param>
        public ChemicalTableRibbonView(IChemicalTableRibbonViewModel viewModel)
        {
            Helpers.NullCheck(viewModel);

            this.DataContext = viewModel;

            InitializeComponent();
        }
    }
}
