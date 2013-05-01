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

namespace SafetyProgram.UserControls.Tooltips.Fancy
{
    /// <summary>
    /// Interaction logic for FancyToolTipView.xaml
    /// </summary>
    internal sealed partial class FancyToolTipView : ToolTip
    {
        public FancyToolTipView(FancyToolTip viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
