using System.Windows.Controls;

namespace SafetyProgram.UserControls.Tooltips
{
    /// <summary>
    /// Interaction logic for DefaultToolTipView.xaml
    /// </summary>
    internal sealed partial class DefaultToolTipView : ToolTip
    {
        public DefaultToolTipView(DefaultToolTip viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
