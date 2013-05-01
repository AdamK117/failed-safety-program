using System;
using System.Windows;
using System.Windows.Controls;

using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Commands;
using SafetyProgram.Ribbons;

namespace SafetyProgram
{
    public sealed class CoshhWindow : BaseINPC, IWindow<IDocument>, IRibbonWindow
    {
        private readonly Window view;
        private readonly IWindowCommands commands;
        private readonly IRibbon ribbon;

        private IService<IDocument> service;
        private IDocument document;

        /// <summary>
        /// Constructs a CoshhWindow IRibbonWindow.
        /// </summary>
        /// <param name="service">The service used by the IRibbonWindow to load IDocuments into it</param>
        /// <param name="document">The IDocument shown by the IRibbonWindow on construction</param>
        public CoshhWindow(IService<IDocument> service, IDocument document)
        {
            if (service == null) throw new ArgumentNullException("Instance of CoshhWindow cannot be created without a service");

            this.service = service;
            this.document = document;

            commands = new WindowICommands(this);
            ribbon = new CoshhRibbon(this);

            view = new CoshhWindowView(this);
            view.InputBindings.AddRange(commands.Hotkeys);          
        }

        /// <summary>
        /// Get the CoshhWindow view.
        /// </summary>
        public Window View
        {
            get { return view; }
        }

        /// <summary>
        /// Gets the CoshhWindow control (IViewable)
        /// </summary>
        Control IViewable.View
        {
            get { return view; }
        }

        /// <summary>
        /// The ICommands (and hotkeys) available to this CoshhWindow
        /// </summary>
        public IWindowCommands Commands
        {
            get { return commands; }
        }

        /// <summary>
        /// Gets the IRibbon viewable for the CoshhWindow
        /// </summary>
        public IRibbon Ribbon
        {
            get { return ribbon; }
        }

        /// <summary>
        /// Gets the IDocument in this CoshhWindow.
        /// </summary>
        /// <remarks>Nullable: IRibbonWindow may contain no IDocument</remarks>
        public IDocument Content
        {
            get { return document; }
            set 
            { 
                document = value;
                if (DocumentChanged != null)
                {
                    DocumentChanged(document);
                }                
                RaisePropertyChanged("Content");
            }
        }
        /// <summary>
        /// Event that triggers when its IDocument changes.
        /// </summary>
        public event Action<IDocument> DocumentChanged;

        /// <summary>
        /// Gets the IDocumentService I/O service used by this CoshhWindow
        /// </summary>
        public IService<IDocument> Service
        {
            get { return service; }
        }
        /// <summary>
        /// Changes the CoshhWindow's I/O service to a new ICoshhDocumentService
        /// </summary>
        /// <param name="newService">The new ICoshhDocumentService</param>
        /// <exception cref="System.ArgumentNullException">Thrown if try to change to a null service</exception>
        public void ChangeService(IService<IDocument> newService)
        {
            if (newService != null)
            {
                service = newService;
                if (ServiceChanged != null)
                {
                    ServiceChanged(service);
                }
                RaisePropertyChanged("Service");
                RaisePropertyChanged("Commands");
            }
            else throw new ArgumentNullException("newService", "The CoshhWindow's service cannot be set to null, a valid service must be set");
        }        
        /// <summary>
        /// Event that triggers if the CoshhWindow's IDocumentService changes.
        /// </summary>
        public event Action<IService<IDocument>> ServiceChanged;
    }
}
