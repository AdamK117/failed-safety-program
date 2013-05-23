using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Fluent;
using MockApp.Commands;
using MockApp.Document;
using MockApp.DocumentObject;
using Ninject;
using SafetyProgram.Base.DocumentFormats;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Document.ContextMenus;

namespace MockApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //using (IKernel kernel = new StandardKernel())
            //{
            //    kernel
            //        .Bind(typeof(IHolderT<>))
            //        .To(typeof(Holder<>));

            //    kernel
            //        .Bind(typeof(IEditableHolderT<>))
            //        .To(typeof(Holder<>));

            //    kernel.Bind<IHolderT<Control>>()
            //        .ToMethod(
            //            ((context) =>
            //                context.Kernel.Get<IEditableHolderT<Control>>()
            //            )
            //        );

            //    kernel.Bind<IEditableHolderT<Control>>()
            //        .To<Holder<Control>>()
            //        .InSingletonScope();

            //    kernel
            //        .Bind<Control>()
            //        .To<DocumentView>();

            //    kernel
            //        .Bind<Control>()
            //        .To<DocumentObjectView>();

            //    kernel
            //        .Bind<IDocumentViewModel>()
            //        .To<DocumentViewModel>();

            //    kernel
            //        .Bind<System.Windows.Controls.ContextMenu>()
            //        .To<DocumentContextMenuView>();

            //    kernel
            //        .Bind<Ribbon>()
            //        .To<MainWindowRibbon>();

            //    kernel
            //        .Bind<IMainWindowCommands>()
            //        .To<MainWindowCommands<Control>>()
            //        .InSingletonScope();

            //    kernel
            //        .Bind<IIOService<MainDocument>>()
            //        .To<MockService>()
            //        .InSingletonScope();

            //    kernel
            //        .Bind<IMainWindowViewModel>()
            //        .To<MainWindowViewModel>();

            //    kernel
            //        .Bind<IMainRibbonViewModel>()
            //        .To<MainRibbonViewModel<IDocument>>();

            //    kernel.Get<MainWindow>().Show();
            //}

            var document = new MainDocument();
            var documentHolder = new Holder<IDocument>(document);

            var someHolder = new Holder<IIOService<IDocument>>(
                new MockService()
            );

            IHolderT<IInputService<IDocument>> anotherHolder = someHolder;

            var commands = new MainWindowCommands<IDocument>(
                                    new MockService(),
                                    documentHolder
                                );
            var commandsHolder = new Holder<MainWindowCommands<IDocument>>(commands);

            //Manually
            var mainWindow = new MainWindowView(
                new MainWindowViewModel(
                    new MainWindowRibbon(
                        new MainRibbonViewModel<IDocument>(
                            commandsHolder,
                            documentHolder
                        )
                    ),
                    new Holder<Control>(
                        new DocumentView(
                            new DocumentViewModel(
                                new DocumentContextMenuView(),
                                new ObservableCollection<Control>(),
                                new A4Format()
                            )
                        )
                    ),
                    commandsHolder
                )
            );

            mainWindow.Show();
        }
    }
}
