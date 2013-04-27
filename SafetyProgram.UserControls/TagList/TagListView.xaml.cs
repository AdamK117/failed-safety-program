using System.Windows.Controls;

namespace SafetyProgram.UserControls.TagList
{
    /// <summary>
    /// Interaction logic for HazardList.xaml
    /// </summary>
    public partial class TagListView : UserControl 
    {
        /// <summary>
        /// For classes deriving TagListView, they implement their own ITagList
        /// </summary>
        protected TagListView() 
        {
            InitializeComponent();
        }

        /// <summary>
        /// For classes composing TagListView, they supply the ITagList dependancy
        /// </summary>
        /// <param name="viewModel"></param>
        internal TagListView(ITagList viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
