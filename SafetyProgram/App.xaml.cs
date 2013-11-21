using System.Windows;
using SafetyProgram.UI;
using SafetyProgram.UI.Models;

namespace SafetyProgram
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string[] Args;


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            GuiKernelData x = UI.Defaults.defaultKernelData;
            var y = new UiController(x);
            y.View.Show();
        }

        /// <summary>
        /// Gets startup event arguments. Useful if the application accepts filepaths etc for startup arguments.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void App_Startup(object sender, StartupEventArgs e)
        {
            Args = e.Args;
        }
    }
}
