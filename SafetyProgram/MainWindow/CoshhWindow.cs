using SafetyProgram.MainWindow.Commands;
using SafetyProgram.MainWindow.Document;
using SafetyProgram.MainWindow.Services;
using SafetyProgram.MainWindow.Ribbons;

namespace SafetyProgram.MainWindow
{
    public class CoshhWindow : BaseINPC
    {
        //Can't be changed once a window is instantiated
        private readonly CoshhWindowView view;
        private readonly WindowCommandsHolder commands;
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
            commands = new WindowCommandsHolder(this);
            ribbon = new CoshhRibbon(this);
            Service = new CoshhLocalFileService(this);
            Document = new CoshhDocument();

            ///Create the view
            view = new CoshhWindowView(this);
            view.InputBindings.AddRange(commands.Hotkeys);
        }

        /// <summary>
        /// Gets the CoshhWindow view.
        /// </summary>
        public CoshhWindowView View
        {
            get { return view; }
        }

        /// <summary>
        /// The commands available to the window.
        /// </summary>
        public WindowCommandsHolder Commands
        {
            get { return commands; }
        }

        /// <summary>
        /// Holds the ribbon of the MainWindow
        /// </summary>
        public CoshhRibbon Ribbon
        {
            get { return ribbon; }
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
                if (DocumentChanged != null)
                {
                    DocumentChanged(document);
                }                
                RaisePropertyChanged("Document");
            }
        }
        public event documentChangedDelegate DocumentChanged;
        public delegate void documentChangedDelegate(CoshhDocument document);

        /// <summary>
        /// The current I/O service used by the window.
        /// </summary>
        public ICoshhDataService Service
        {
            get { return service; }
            set 
            { 
                service = value;
                if (ServiceChanged != null)
                {
                    ServiceChanged(service);
                }
                RaisePropertyChanged("Service");                
            }
        }
        public event serviceChangedDelegate ServiceChanged;
        public delegate void serviceChangedDelegate(ICoshhDataService service);
    }
}
