using Fluent;

namespace MockApp
{
    /// <summary>
    /// Interaction logic for MainWindowRibbon.xaml
    /// </summary>
    public partial class MainWindowRibbon : Ribbon
    {
        public MainWindowRibbon(IMainRibbonViewModel ribbonViewModel)
        {
            this.DataContext = ribbonViewModel;
            InitializeComponent();
        }
    }
}
