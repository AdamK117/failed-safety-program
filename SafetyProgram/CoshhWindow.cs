using SafetyProgram.BaseClasses;
using SafetyProgram.Document;
using SafetyProgram.Document.Services;
using SafetyProgram.Commands;
using SafetyProgram.Ribbons;
using System;


namespace SafetyProgram
{
    public class CoshhWindow : BaseINPC
    {
        private readonly CoshhWindowView view;
        private readonly WindowCommandsHolder commands;
        private readonly CoshhRibbon ribbon;

        private ICoshhDocumentService service;
        private ICoshhDocument document;        

        /// <summary>
        /// Constructs a ViewModel for the main window. Acts as the holder for the document, ribbon, save commands, etc.
        /// </summary>
        /// <param name="window"></param>
        public CoshhWindow(ICoshhDocumentService service, ICoshhDocument document)
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
        public ICoshhDocument Document
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
        public event Action<ICoshhDocument> DocumentChanged;

        /// <summary>
        /// Gets the ICoshhDocumentService I/O service used by the CoshhWindow
        /// </summary>
        public ICoshhDocumentService Service
        {
            get { return service; }
        }
        /// <summary>
        /// Changes the CoshhWindow's service to a new ICoshhDocumentService
        /// </summary>
        /// <param name="newService">The new ICoshhDocumentService</param>
        /// <exception cref="System.ArgumentNullException">Thrown if try to change to a null service</exception>
        public void ChangeService(ICoshhDocumentService newService)
        {
            if (newService == null)
            {
                throw new ArgumentNullException("newService", "The CoshhWindow's service cannot be set to null, a valid service must be set");
            }
            service = newService;
            if (ServiceChanged != null)
            {
                ServiceChanged(service);
            }
            RaisePropertyChanged("Service");
        }        
        /// <summary>
        /// Event that triggers if the CoshhWindows service changes.
        /// </summary>
        public event Action<ICoshhDocumentService> ServiceChanged;
    }
}
