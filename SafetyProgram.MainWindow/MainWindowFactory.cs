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

        public Window CreateNew()
        {
            var contentHolder = new Holder<TWindowContent>(
                contentService.Content.New()
            );

            var windowCommands = new Holder<IWindowCommands>(
                new WindowICommands<TWindowContent>(
                    commandInvoker,
                    contentHolder,
                    contentService
                )
            );

            var newWindow = new CoshhWindowView(
                new CoshhWindowViewModel(
                    new Holder<Ribbon>(
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
