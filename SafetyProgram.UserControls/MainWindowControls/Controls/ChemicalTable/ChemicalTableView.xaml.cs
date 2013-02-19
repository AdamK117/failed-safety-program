using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using SafetyProgram.Models.DataModels;

namespace SafetyProgram.UserControls.MainWindowControls.ChemicalTable
{
    /// <summary>
    /// Interaction logic for ChemicalTable.xaml
    /// </summary>
    public partial class ChemicalTableView : UserControl
    {
        private ChemicalTableViewModel vm;

        public ChemicalTableView(ObservableCollection<CoshhDocDataObject<CoshhChemicalModel>> chemicals)
        {
            InitializeComponent();
            vm = new ChemicalTableViewModel(chemicals);
            LayoutRoot.DataContext = vm;
        }
    }
}
