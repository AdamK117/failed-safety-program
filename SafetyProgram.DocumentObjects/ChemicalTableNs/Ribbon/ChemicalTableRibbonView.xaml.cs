using System;
using Fluent;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs.Ribbon
{
    /// <summary>
    /// Interaction logic for ChemicalTableRibbonView.xaml
    /// </summary>
    internal sealed partial class ChemicalTableRibbonView : RibbonTabItem
    {
        public ChemicalTableRibbonView(IChemicalTableRibbonTab viewModel)
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
