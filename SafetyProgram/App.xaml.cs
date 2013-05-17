using System.Windows;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Configuration;
using SafetyProgram.Document;
using SafetyProgram.DocumentObjects;
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

            //CONFIGURATION
            IService<IConfiguration> configFileService = new LocalFileService<IConfiguration>(
                new ConfigurationLocalFileFactory(), 
                TestData.CONFIGURATION_FILE
                );

            IConfiguration appConfiguration = configFileService.Load();

            //COMMAND INVOKER
            ICommandInvoker commandInvoker = new CommandInvoker();

            //DEFAULT DOCUMENT (shown when opening)
            IService<ICoshhDocument> contentService = new InteractiveLocalFileService<ICoshhDocument>(
                new CoshhDocumentLocalFileFactory(
                    new LocalDocumentObjectFactory(appConfiguration, commandInvoker),
                    appConfiguration, 
                    commandInvoker
                    )
                );

            var content = contentService.New();

            //MAIN WINDOW
            IWindow window = CoshhWindowFactory<ICoshhDocument>.StaticCreateNew
                (
                    appConfiguration,
                    contentService,
                    commandInvoker,
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
