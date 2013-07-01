using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Fluent;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Document;
using SafetyProgram.MainWindow.Commands;
using SafetyProgram.MainWindow.Ribbons;

namespace SafetyProgram.MainWindow
{
    /// <summary>
    /// Defines a facade factory for producing MainWindow, siplifying its instantiation.
    /// </summary>
    /// <typeparam name="TWindowContent"></typeparam>
    public sealed class MainWindowFacade<TWindowContent> : IFactory<Window>
        where TWindowContent : class, IWindowContent
    {
        private readonly IConfiguration appConfig;
        private readonly ICommandInvoker commandInvoker;
        private readonly IEditableHolder<IIOService<TWindowContent>> contentService;

        public MainWindowFacade(IConfiguration appConfig,
            ICommandInvoker commandInvoker,
            IEditableHolder<IIOService<TWindowContent>> contentService)
        {
            Helpers.NullCheck(appConfig, commandInvoker, contentService);

            this.appConfig = appConfig;
            this.commandInvoker = commandInvoker;
            this.contentService = contentService;
        }

        /// <summary>
        /// Creates a new SafetyProgram window.
        /// </summary>
        /// <returns></returns>
        public Window CreateNew()
        {
            //Create a holder for the main windows content.
            var contentHolder = new Holder<TWindowContent>(
                //Occupy the holder with a blank document (by default).
                contentService.Content.New()
            );

            //Create a holder for the windows commands.
            var windowCommands = new Holder<IWindowCommands>(
                //Occupy the holder with default window commands.
                new WindowICommands<TWindowContent>(
                    commandInvoker,
                    contentHolder,
                    contentService
                )
            );

            //Create the main windows view.
            var newWindow = new CoshhWindowView(
                //Supply the view with a viewmodel. Its method of getting model data.
                new CoshhWindowViewModel(
                    //Create a holder for the mainwindows ribbon
                    new Holder<Ribbon>(
                        //Occupy the holder with a ribbon view
                        new CoshhRibbonView(
                            new CoshhRibbonViewModel(
                                windowCommands,
                                new ChildHolder<IWindowContent, ICollection<RibbonTabItem>>(
                                    contentHolder,
                                    (newContent) => newContent.DocumentRibbonTabs
                                ),
                                new ChildHolder<IWindowContent, ObservableCollection<RibbonTabItem>>(
                                    contentHolder,
                                    (newContent) => newContent.ContextualRibbonTabs
                                )
                            )
                        )
                    ),
                    new ChildHolder<IWindowContent, Control>(
                        contentHolder,
                        (newContent) => newContent.View
                    ),
                    windowCommands.Content.Hotkeys
                )
            );

            return newWindow;                
        }
    }
}
