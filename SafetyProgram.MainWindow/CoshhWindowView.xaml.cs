using System;
using Fluent;

namespace SafetyProgram.MainWindow
{
    /// <summary>
    /// Interaction logic for CoshhWindow.xaml
    /// </summary>
    internal sealed partial class CoshhWindowView : RibbonWindow
    {
        private readonly ICoshhWindow viewModel;

        /// <summary>
        /// Makes an instance of the CoshhWindow object.
        /// </summary>
        public CoshhWindowView(ICoshhWindow viewModel)
        {
            this.viewModel = viewModel;

            this.DataContext = viewModel;
            InitializeComponent();

            this.InputBindings.AddRange(viewModel.Commands.Hotkeys);
        }

        //TODO: Make it so the closed dialog shows up without de-focusing the window
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            //Close the current document (if possible)
            if (viewModel.Content != null)
            {
                viewModel.Commands.Close.Execute(null);
            }
            
            System.Windows.Application.Current.Shutdown();
        }
    }
}
