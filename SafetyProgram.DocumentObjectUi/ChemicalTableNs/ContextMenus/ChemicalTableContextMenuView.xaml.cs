using System.Windows.Controls;
using SafetyProgram.Base;

namespace SafetyProgram.DocumentObjectUi.ChemicalTableNs.ContextMenus
{
    /// <summary>
    /// Interaction logic for ChemicalTableContextMenuView.xaml
    /// </summary>
    internal sealed partial class ChemicalTableContextMenuView : ContextMenu
    {
        public ChemicalTableContextMenuView(IChemicalTableContextMenuViewModel viewModel)
        {
            Helpers.NullCheck(viewModel);

            this.DataContext = viewModel;
            
            InitializeComponent();
        }
    }
}
