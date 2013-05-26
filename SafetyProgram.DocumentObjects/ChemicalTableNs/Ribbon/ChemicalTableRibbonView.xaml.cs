using Fluent;
using SafetyProgram.Base;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs.Ribbon
{
    /// <summary>
    /// Interaction logic for ChemicalTableRibbonView.xaml
    /// </summary>
    internal sealed partial class ChemicalTableRibbonView : RibbonTabItem
    {
        public ChemicalTableRibbonView(IChemicalTableRibbonViewModel viewModel)
        {
            Helpers.NullCheck(viewModel);

            this.DataContext = viewModel;

            InitializeComponent();  
        }
    }
}
