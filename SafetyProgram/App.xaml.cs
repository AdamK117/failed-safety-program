using System;
using System.Windows;
using Fluent;
using Ninject;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Configuration;
using SafetyProgram.Document;
using SafetyProgram.DocumentObjects;
using SafetyProgram.DocumentObjects.ChemicalTableNs;
using SafetyProgram.MainWindow;
using SafetyProgram.MainWindow.Commands;
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

            using (IKernel kernel = new StandardKernel())
            {
                //FACTORY BINDINGS

                ////LocalFactories: Configuration/Repository
                //kernel
                //    .Bind<ILocalFileFactory<IConfiguration>>()
                //    .To<ConfigurationLocalFileFactory>()
                //    .InSingletonScope();

                //kernel
                //    .Bind<ILocalFileFactory<IRepositoryInfo>>()
                //    .To<RepositoryInfoLocalFileFactory>();

                ////LocalFactories: Core components
                //kernel
                //    .Bind<ILocalFileFactory<ICoshhDocument>>()
                //    .To<CoshhDocumentLocalFileFactory>();

                //kernel
                //    .Bind<ILocalFileFactory<IDocumentObject>>()
                //    .To<DocumentObjectLocalFileFactory>();

                //kernel
                //    .Bind<ILocalFileFactory<IChemicalTable>>()
                //    .To<ChemicalTableLocalFileFactory>();

                ////LocalFactories: Models
                //kernel
                //    .Bind<ILocalFileFactory<ICoshhChemicalObject>>()
                //    .To<CoshhChemicalObjectLocalFileFactory>();

                //kernel
                //    .Bind<ILocalFileFactory<IChemicalModelObject>>()
                //    .To<ChemicalModelObjectLocalFileFactory>();

                //kernel
                //    .Bind<ILocalFileFactory<IHazardModelObject>>()
                //    .To<HazardModelObjectLocalFileFactory>();

                ////COMMAND INVOKER (command pattern, singleton pattern)
                //kernel
                //    .Bind<ICommandInvoker>()
                //    .To<CommandInvoker>()
                //    .InSingletonScope();

                ////COMMANDS
                ////Commands: Window commands
                //kernel
                //    .Bind<IWindowCommands>()
                //    .To<WindowICommands<ICoshhDocument>>();

                ////SERVICES
                ////LocalServices: Configuration
                //kernel
                //    .Bind<IIOService<IConfiguration>>()
                //    .To<LocalFileService<IConfiguration>>()
                //    .InSingletonScope()
                //    .WithConstructorArgument("path", TestData.CONFIGURATION_FILE);

                ////LocalServices: Core
                //kernel
                //    .Bind<IIOService<ICoshhDocument>>()
                //    .To<InteractiveLocalFileService<ICoshhDocument>>()
                //    .InSingletonScope();

                ////VIEWMODELS              
                ////MainWindowRibbon
                //kernel
                //    .Bind<IRibbon>()
                //    .To<CoshhRibbon<ICoshhDocument>>()
                //    .Named("MainRibbon");

                ////VIEW CONSTRUCTORS
                ////MainWindowRibbonView
                //kernel
                //    .Bind<Func<ICoshhRibbon, Ribbon>>()
                //    .ToMethod(
                //        ((context) =>
                //            (coshhRibbon) => new CoshhRibbonView(coshhRibbon)
                //        )
                //    );

                ////MainWindowView
                //kernel
                //    .Bind<Func<ICoshhWindowT<ICoshhDocument>, Window>>()
                //    .ToMethod(
                //        context =>
                //            coshhWindow => new CoshhWindowView(coshhWindow)
                //    );

                ////VIEWMODEL CONSTRUCTORS
                ////MainWindowRibbon
                //kernel
                //    .Bind<Func<ICoshhWindowT<ICoshhDocument>, IRibbon>>()
                //    .ToMethod(
                //        ((context) =>
                //            coshhWindow => kernel.Get<IRibbon>()
                //        )
                //    );

                ////MainWindowCommands
                //kernel
                //    .Bind<Func<ICoshhWindowT<ICoshhDocument>, IWindowCommands>>()
                //    .ToMethod(
                //        ((context) =>
                //            windowCommands => kernel.Get<IWindowCommands>()
                //        )
                //    );
                
                ////RESOLVING BINDINGS

                ////Resolve: IConfiguration (singleton pattern)
                //kernel
                //    .Bind<IConfiguration>()
                //    .ToMethod(
                //        context =>
                //            {
                //                return context.Kernel
                //                    .Get<IIOService<IConfiguration>>()
                //                    .Load();
                //            }
                //    )
                //    .InSingletonScope();                          

                ////Resolve: Default ICoshhDocument
                //kernel
                //    .Bind<ICoshhDocument>()
                //    .ToMethod(
                //        context =>
                //            {
                //                return context.Kernel.Get<IIOService<ICoshhDocument>>().New();
                //            }
                //    );

                ////kernel
                ////    .Bind<IWindow<ICoshhDocument>>()
                ////    .To<CoshhWindow<ICoshhDocument>>();

                ////Resolve main window
                //kernel
                //    .Bind<IWindow<ICoshhDocument>>()
                //    .ToMethod(
                //        context =>
                //        {
                //            return CoshhWindowFactory<ICoshhDocument>.StaticCreateNew
                //                (
                //                    kernel.Get<IConfiguration>(),
                //                    kernel.Get<IIOService<ICoshhDocument>>(),
                //                    kernel.Get<ICommandInvoker>(),
                //                    kernel.Get<ICoshhDocument>()
                //                );
                //        }
                //    );

                //var a = kernel.Get<IWindow<ICoshhDocument>>();
                //a.View.Show();
            }

            //CONFIGURATION
            //IService<IConfiguration> configFileService = new LocalFileService<IConfiguration>(
            //    new ConfigurationLocalFileFactory(new RepositoryInfoLocalFileFactory(),
            //        new ChemicalModelObjectLocalFileFactory()), 
            //    TestData.CONFIGURATION_FILE
            //    );

            //IConfiguration appConfiguration = configFileService.Load();

            ////COMMAND INVOKER
            //ICommandInvoker commandInvoker = new CommandInvoker();

            ////DEFAULT DOCUMENT (shown when opening)
            //IService<ICoshhDocument> contentService = new InteractiveLocalFileService<ICoshhDocument>(
            //    new CoshhDocumentLocalFileFactory(
            //        new DocumentObjectLocalFileFactory(appConfiguration, commandInvoker),
            //        appConfiguration, 
            //        commandInvoker
            //        )
            //    );

            //content = contentService.New();

            ////MAIN WINDOW
            //IWindow window = CoshhWindowFactory<ICoshhDocument>.StaticCreateNew
            //    (
            //        appConfiguration,
            //        contentService,
            //        commandInvoker,
            //        content
            //    );

            //window.View.Show();
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
