using System;
using Fluent;

namespace SafetyProgram.MainWindow
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
            System.Windows.Application.Current.Shutdown();
        }
    }
}
