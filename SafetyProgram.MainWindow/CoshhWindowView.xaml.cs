using Fluent;

namespace SafetyProgram.MainWindow
{
    public sealed partial class CoshhWindowView : RibbonWindow
    {
        public CoshhWindowView(ICoshhWindowViewModel viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();

            this.InputBindings.AddRange(viewModel.Hotkeys);
        }

        ////TODO: Make it so the closed dialog shows up without de-focusing the window
        //protected override void OnClosed(EventArgs e)
        //{
        //    base.OnClosed(e);

        //    //Close the current document (if possible)
        //    if (viewModel.Content != null)
        //    {
        //        viewModel.Commands.Close.Execute(null);
        //    }
            
        //    System.Windows.Application.Current.Shutdown();
        //}
    }
}
