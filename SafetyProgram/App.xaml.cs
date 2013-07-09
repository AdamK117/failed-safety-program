using System.Windows;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Configuration;
using SafetyProgram.Document;
using SafetyProgram.DocumentObjects;
using SafetyProgram.DocumentObjects.ChemicalTableNs;
using SafetyProgram.MainWindow;
using SafetyProgram.ModelObjects;
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

            //Create a service that can load the config file.
            var configService = new LocalFileService<IConfiguration>(
                new ConfigurationLocalFileFactory(
                    new RepositoryInfoLocalFileFactory(),
                    //Give it a way to load chemical models.
                    new ChemicalModelObjectLocalFileFactory()
                ),
                TestData.CONFIGURATION_FILE
            );

            //Load the config file (singleton).
            var config = configService.Load();

            //Assign a command invoker (singleton).
            var commandInvoker = new CommandInvoker();

            //Create a service for CoshhDocuments (default local, may be changed).
            var service = new InteractiveLocalFileService<ICoshhDocument>(
                new CoshhDocumentLocalFileFactory(
                    config, 
                    commandInvoker, 
                    new DocumentObjectLocalFileFactory(
                        config,
                        commandInvoker,
                        new ChemicalTableLocalFileFactory(
                            config,
                            commandInvoker
                        )
                    )
                )
            );

            //Place the service in an ObservableHolder (it may change during runtime).
            var serviceHolder = new Holder<IIOService<ICoshhDocument>>(
                service
            );

            //Create a main window factory (use a facade, a fully exposed API would be a mess to wire up here).
            var windowFactory = new MainWindowFacade<ICoshhDocument>(
                config,
                commandInvoker,
                serviceHolder
            );

            //Use the factory to create a window and show it.
            var window = windowFactory.CreateNew();
            window.Show();
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
