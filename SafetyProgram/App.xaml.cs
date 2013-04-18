using System.Windows;

using SafetyProgram.Document;
using SafetyProgram.Document.Services;
using SafetyProgram;

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
            ICoshhDocumentService service = new CoshhDocumentLocalFileService();
            ICoshhDocument document = service.New();

            CoshhWindow window = new CoshhWindow(service, document);

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
