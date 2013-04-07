using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace SafetyProgram.MainWindow.Document.Controls.ChemicalTable
{
    /// <summary>
    /// Interaction logic for ChemicalTableView.xaml
    /// </summary>
    public partial class ChemicalTableView : UserControl
    {
        private readonly ChemicalTable viewModel;

        public ChemicalTableView(ChemicalTable viewModel)
        {
            this.viewModel = viewModel;
            DataContext = viewModel;

            InitializeComponent();
        }
    }
}
