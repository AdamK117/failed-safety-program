using Fluent;
using SafetyProgram.Base.FSharp;

namespace SafetyProgram.UI.Views
{
    /// <summary>
    /// Interaction logic for ChemicalTableRibbonView.xaml
    /// </summary>
    public sealed partial class ChemicalTableContextualRibbonTab : RibbonTabItem
    {
        /// <summary>
        /// Construct an instance of a chemical table contextual ribbon tab view.
        /// </summary>
        /// <param name="viewModel">The underlying viewmodel for this contextual ribbon tab.</param>
        public ChemicalTableContextualRibbonTab(IChemicalTableRibbonViewModel viewModel)
        {
            Helpers.NullCheck(viewModel);

            this.DataContext = viewModel;

            InitializeComponent();
        }
    }
}
