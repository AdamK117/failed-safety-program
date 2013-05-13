using System.Windows;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Commands;
using SafetyProgram.Configuration;
using SafetyProgram.Document;
using SafetyProgram.Ribbons;
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
            IService<IConfiguration> configFileService = new LocalFileService<IConfiguration>(
                new ConfigurationLocalFileFactory(), 
                TestData.CONFIGURATION_FILE
                );

            IConfiguration configFile = configFileService.Load();

            //Load the document for the app
            IService<CoshhDocument> contentService = new InteractiveLocalFileService<CoshhDocument>(
                configFile, 
                new CoshhDocumentLocalFileFactory(configFile)
                );

            var content = contentService.New();

            IWindow window = CoshhWindowFactory<CoshhDocument>.StaticCreateNew
                (
                    configFile,
                    contentService,
                    content
                );

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
