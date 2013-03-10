using System.Windows.Controls;

using Microsoft.Practices.ServiceLocation;

using SafetyProgram.UserControls;
using SafetyProgram.Data.DOM;

namespace SafetyProgram.MainWindow
{
    /// <summary>
    /// Interaction logic for MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : UserControl
    {
        private CoshhDocument currentlyOpen;
        public MainWindowView(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
            currentlyOpen = ServiceLocator.Current.GetInstance<CoshhDocument>();
            currentlyOpen.Body.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(DocObject_CollectionChanged);

            foreach (IDocObject docObject in currentlyOpen.Body)
            {
                LayoutRoot.Children.Clear();
                LayoutRoot.Children.Add(docObject.Display());
            }
        }

        void DocObject_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            foreach (IDocObject docObject in currentlyOpen.Body)
            {
                LayoutRoot.Children.Add(docObject.Display());
            }
        }
    }
}
