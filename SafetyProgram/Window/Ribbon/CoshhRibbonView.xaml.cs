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
using System.Windows.Shapes;

namespace SafetyProgram.Window.Ribbon
{
    /// <summary>
    /// Interaction logic for RibbonView.xaml
    /// </summary>
    public partial class CoshhRibbonView : UserControl
    {
        private CoshhRibbon viewModel;
        public CoshhRibbonView(CoshhRibbon viewModel)
        {
            this.viewModel = viewModel;
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
