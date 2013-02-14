using System.Windows;

namespace PrismAggregatorDesignTest
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
            new Bootstrapper().Run();
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            Args = e.Args;
        }
    }
}
