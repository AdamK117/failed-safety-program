using System;
using System.Windows.Controls;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs.ContextMenus
{
    /// <summary>
    /// Interaction logic for ChemicalTableContextMenuView.xaml
    /// </summary>
    internal sealed partial class ChemicalTableContextMenuView : ContextMenu
    {
        public ChemicalTableContextMenuView(IChemicalTableContextMenu viewModel)
        {
            if (viewModel != null)
            {
                this.DataContext = viewModel;
            }
            else throw new ArgumentNullException();
            
            InitializeComponent();
        }
    }
}
