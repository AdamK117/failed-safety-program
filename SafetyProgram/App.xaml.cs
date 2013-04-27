using System.Windows;

using SafetyProgram.Base.Interfaces;
using SafetyProgram.Services;
using SafetyProgram.UserControls;

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

            //DI
            IService<IDocument> service = new DocumentLocalFileService();
            IDocument document = service.New();

            IWindow window = new CoshhWindow(service, document);

            window.View.Show();

            //TestWindow tw = new TestWindow();
            //tw.Show();
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
