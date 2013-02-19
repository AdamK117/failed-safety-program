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

namespace SafetyProgram.MainWindow.UserControls.ApparatusTable
{
    /// <summary>
    /// Interaction logic for ApparatusTableView.xaml
    /// </summary>
    public partial class ApparatusTableView : UserControl
    {
        private ApparatusTableViewModel vm;
        public ApparatusTableView()
        {
            InitializeComponent();
            vm = new ApparatusTableViewModel();
            LayoutRoot.DataContext = vm;
        }
    }
}
