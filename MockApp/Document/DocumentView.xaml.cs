using System.Windows.Controls;
using MockApp.Document;

namespace MockApp
{
    /// <summary>
    /// Interaction logic for MainContent.xaml
    /// </summary>
    public partial class DocumentView : UserControl
    {
        public DocumentView(IDocumentViewModel viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
