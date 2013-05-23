using System;
using System.Windows;
using Fluent;
using Ninject;
using Ninject.Modules;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Document;
using SafetyProgram.MainWindow.Commands;
using SafetyProgram.MainWindow.Ribbons;

namespace SafetyProgram.MainWindow
{
    public sealed class MainWindowModule : NinjectModule
    {
        public override void Load()
        {
            ////CONTENT SERVICE
            //Bind<IIOService<ICoshhDocument>>()
            //    .To<InteractiveLocalFileService<ICoshhDocument>>()
            //    .InSingletonScope();

            ////CONTENT
            //Bind<ICoshhDocument>()
            //    .ToMethod(
            //        ((context) =>
            //            context.Kernel
            //            .Get<IIOService<ICoshhDocument>>()
            //            .Load()
            //        )
            //    );

            ////VIEW: MainWindowViewConstructor
            //Bind<Func<ICoshhWindowT<ICoshhDocument>, Window>>()
            //    .ToMethod(
            //        context =>
            //            coshhWindow => new CoshhWindowView(coshhWindow)
            //    );

            ////RIBBON: MainWindowRibbonView Constructor
            //Bind<Func<ICoshhRibbon, Ribbon>>()
            //    .ToMethod(
            //        ((context) =>
            //            (coshhRibbon) => new CoshhRibbonView(coshhRibbon)
            //        )
            //    );

            ////RIBBON: MainWindowRibbon Constructor
            //Bind<Func<ICoshhWindowT<ICoshhDocument>, IRibbon>>()
            //    .ToMethod(
            //        ((context) =>
            //            ((coshhWindow) => 
            //                new CoshhRibbon<ICoshhDocument>(
            //                    coshhWindow,
            //                    context.Kernel.Get<Func<ICoshhRibbon, Ribbon>>()
            //                )
            //            )
            //        )
            //    );

            ////MainWindowCommands
            //Bind<IWindowCommands>()
            //    .To<WindowICommands<ICoshhDocument>>();

            ////MainWindowCommands Constructor
            //Bind<Func<ICoshhWindowT<ICoshhDocument>, IWindowCommands>>()
            //    .ToMethod(
            //        ((context) =>
            //            ((coshhWindow) => 
            //                new WindowICommands<ICoshhDocument>(
            //                    coshhWindow,
            //                    context.Kernel.Get<ICommandInvoker>()
            //                )
            //            )
            //        )
            //    );

            ////Complete the window binding
            //Bind<IWindow>()
            //    .To<CoshhWindow<ICoshhDocument>>();
        }
    }
}
