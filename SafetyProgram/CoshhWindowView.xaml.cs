using System;
using Fluent;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram
{
    /// <summary>
    /// Interaction logic for CoshhWindow.xaml
    /// </summary>
    internal sealed partial class CoshhWindowView : RibbonWindow
    {
        private readonly IWindow<IDocument> viewModel;

        /// <summary>
        /// Makes an instance of the CoshhWindow object.
        /// </summary>
        public CoshhWindowView(IWindow<IDocument> viewModel)
        {
            this.viewModel = viewModel;

            DataContext = viewModel;
            InitializeComponent();
        }

        //TODO: Make it so the closed dialog shows up without de-focusing the window
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            //Close the current document (if possible)
            if (viewModel.Content != null)
            {
                viewModel.Service.Close(viewModel.Content);
            }
            
            System.Windows.Application.Current.Shutdown();
        }
    }
}
