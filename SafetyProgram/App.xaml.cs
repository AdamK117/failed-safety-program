using System.Windows;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Services;
using SafetyProgram.Configuration;
using SafetyProgram.Static;
using SafetyProgram.RepositoryIO;

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

            //Load the configuration file for the app
            IService<IConfiguration> configFileService = new LocalConfigurationFile(TestData.ConfigFile);
            IConfiguration configFile = configFileService.Load();

            //Load the document for the app
            IService<IDocument> service = new DocumentLocalFileService();
            IDocument document = service.New();

            IWindow window = new CoshhWindow(configFile, service, document);

            window.View.Show();
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
