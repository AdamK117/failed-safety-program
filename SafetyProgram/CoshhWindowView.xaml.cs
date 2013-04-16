using System;
using Fluent;

namespace SafetyProgram
{
    /// <summary>
    /// Interaction logic for CoshhWindow.xaml
    /// </summary>
    public partial class CoshhWindowView : RibbonWindow
    {
        private readonly CoshhWindow viewModel;

        /// <summary>
        /// Makes an instance of the CoshhWindow object.
        /// </summary>
        public CoshhWindowView(CoshhWindow viewModel)
        {
            this.viewModel = viewModel;

            DataContext = viewModel;
            InitializeComponent();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            //Close the current document (if possible)
            if (viewModel.Document != null)
            {
                viewModel.Service.Close(viewModel.Document);
            }
            
            System.Windows.Application.Current.Shutdown();
        }
    }
}
