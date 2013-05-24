using System;
using System.Collections.ObjectModel;
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
using SafetyProgram.MainWindow.Ribbons;
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

            //CoshhWindowView window = new CoshhWindowView(
            //    new CoshhWindowViewModel(
            //        new Holder<Ribbon>(
            //            new CoshhRibbonView(
            //                new CoshhRibbonViewModel(
            //                    new Holder<ObservableCollection<RibbonTabItem>>(
            //                        new ObservableCollection<RibbonTabItem>()
            //                    ),
            //                    new Holder<WindowICommands<>(
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
