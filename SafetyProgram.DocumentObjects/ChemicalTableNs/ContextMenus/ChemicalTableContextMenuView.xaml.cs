using System.Windows.Controls;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs.ContextMenus
{
    /// <summary>
    /// Interaction logic for ChemicalTableContextMenuView.xaml
    /// </summary>
    internal sealed partial class ChemicalTableContextMenuView : ContextMenu
    {
        private readonly ChemicalTableContextMenu viewModel;

        public ChemicalTableContextMenuView(ChemicalTableContextMenu viewModel)
        {
            this.viewModel = viewModel;
            this.DataContext = viewModel;

            InitializeComponent();
        }
    }
}
