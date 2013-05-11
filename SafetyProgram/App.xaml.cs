using System.Windows;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Configuration;
using SafetyProgram.Services;
using SafetyProgram.Static;

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
            IService<IConfiguration> configFileService = new GenericLocalFileService<IConfiguration>(new ConfigurationLocalFileFactory(), TestData.CONFIGURATION_FILE);
            IConfiguration configFile = configFileService.Load();

            //Load the document for the app
            IService<IDocument> service = new DocumentLocalFileService(configFile);
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
