using SafetyProgram.MainWindow.Document;
using SafetyProgram.MainWindow.IO;
using SafetyProgram.Window.Commands;
using SafetyProgram.Window.Ribbon;

namespace SafetyProgram.Window
{
    public class CoshhWindow : BaseINPC
    {
        //Can't be changed once a window is instantiated
        private readonly CoshhWindowView view;
        private readonly WindowCommands commands;        
        private readonly CoshhRibbon ribbon;

        //Can be changed throughout the course of runtime
        private CoshhDocument document;
        private ICoshhDataService service;

        /// <summary>
        /// Constructs a ViewModel for the main window. Acts as the holder for the document, ribbon, save commands, etc.
        /// </summary>
        /// <param name="window"></param>
        public CoshhWindow()
        {
            //Create window commands
            commands = new WindowCommands(this);

            //Create a new (blank) document
            Document = new CoshhDocument();

            //Create a new ribbon
            ribbon = new CoshhRibbon(this);

            //Default to saving locally
            service = new CoshhLocalFileService(Document);

            //Create a new window, this is its ViewModel.
            view = new CoshhWindowView(this);

            //Give the window some hotkeys
            view.InputBindings.AddRange(commands.Hotkeys.All);

            //Show the window
            view.Show();
        }

        /// <summary>
        /// The commands available to the window.
        /// </summary>
        public WindowCommands Commands
        {
            get { return commands; }
        }

        /// <summary>
        /// Holds the current document of the MainWindow
        /// </summary>
        public CoshhDocument Document
        {
            get { return document; }
            set 
            { 
                document = value;
                RaisePropertyChanged("Document");
            }
        }

        /// <summary>
        /// Holds the ribbon of the MainWindow
        /// </summary>
        public CoshhRibbon Ribbon
        {
            get { return ribbon; }
        }

        /// <summary>
        /// The current I/O service used by the window.
        /// </summary>
        public ICoshhDataService Service
        {
            get { return service; }
            set { service = value; }
        }
    }
}
