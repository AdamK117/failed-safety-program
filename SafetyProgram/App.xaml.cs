using System.Windows;
using SafetyProgram.IO;
using SafetyProgram.Models;
using SafetyProgram.Models.IO.XML;

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

            // Create an application kernel model.
            var appKernel = new ApplicationKernel(
                null
            );
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
