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

namespace SafetyProgram.UserControls.MainWindowControls.ApparatusTable
{
    /// <summary>
    /// Interaction logic for ApparatusTableView.xaml
    /// </summary>
    public partial class ApparatusTableView : UserControl
    {
        private ApparatusTableViewModel vm;
        public ApparatusTableView(ObservableCollection<IDocDataHolder<CoshhApparatusModel>> apparatuses)
        {
            InitializeComponent();
            vm = new ApparatusTableViewModel(apparatuses);
            LayoutRoot.DataContext = vm;
        }
    }
}
