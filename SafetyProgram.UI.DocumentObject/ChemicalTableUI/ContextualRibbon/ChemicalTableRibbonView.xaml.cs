using Fluent;
using SafetyProgram.Base;

namespace SafetyProgram.UI.DocumentObject.ChemicalTableUI
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
