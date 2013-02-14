using System.Windows.Controls;

namespace SafetyProgram.RibbonView
{
    /// <summary>
    /// Interaction logic for RibbonView.xaml
    /// </summary>
    public partial class RibbonView : UserControl
    {
        public RibbonView(RibbonViewModel ribbonViewModel)
        {
            InitializeComponent();
            this.DataContext = ribbonViewModel;
        }
    }
}
