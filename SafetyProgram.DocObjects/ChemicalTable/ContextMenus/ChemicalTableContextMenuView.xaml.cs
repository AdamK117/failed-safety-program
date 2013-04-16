using System.Windows.Controls;

namespace SafetyProgram.DocObjects.ChemicalTable.ContextMenus
{
    /// <summary>
    /// Interaction logic for ChemicalTableContextMenuView.xaml
    /// </summary>
    public partial class ChemicalTableContextMenuView : ContextMenu
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
