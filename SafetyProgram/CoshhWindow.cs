using SafetyProgram.BaseClasses;
using SafetyProgram.Document;
using SafetyProgram.Document.Services;
using SafetyProgram.Commands;
using SafetyProgram.Ribbons;


namespace SafetyProgram
{
    public class CoshhWindow : BaseINPC
    {
        private readonly CoshhWindowView view;
        private readonly WindowCommandsHolder commands;
        private readonly CoshhRibbon ribbon;

        private ICoshhDocumentService service;
        private CoshhDocument document;        

        /// <summary>
        /// Constructs a ViewModel for the main window. Acts as the holder for the document, ribbon, save commands, etc.
        /// </summary>
        /// <param name="window"></param>
        public CoshhWindow(ICoshhDocumentService service, CoshhDocument document)
        {
            this.service = service;
            this.document = document;

            commands = new WindowCommandsHolder(this);
            ribbon = new CoshhRibbon(this);
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
        /// <summary>
        /// Event that triggers if he CoshhDocument in the CoshhWindow changes.
        /// </summary>
        public event documentChangedDelegate DocumentChanged;
        /// <summary>
        /// Delegate for DocumentChanged event.
        /// </summary>
        /// <param name="document">The new CoshhDocument.</param>
        public delegate void documentChangedDelegate(CoshhDocument document);

        /// <summary>
        /// The current I/O service used by the window.
        /// </summary>
        public ICoshhDocumentService Service
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
        /// <summary>
        /// Event that triggers if the CoshhWindows service changes.
        /// </summary>
        public event serviceChangedDelegate ServiceChanged;
        /// <summary>
        /// Delegate for ServiceChanged event.
        /// </summary>
        /// <param name="service">The new ICoshhDocument service for the CoshhWindow</param>
        public delegate void serviceChangedDelegate(ICoshhDocumentService service);
    }
}
