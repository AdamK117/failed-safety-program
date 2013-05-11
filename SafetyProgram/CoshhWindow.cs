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
        /// <summary>
        /// Constructs a CoshhWindow IRibbonWindow.
        /// </summary>
        /// <param name="documentService">The service used by the IRibbonWindow to load IDocuments into it</param>
        /// <param name="document">The IDocument shown by the IRibbonWindow on construction</param>
        public CoshhWindow
            (IConfiguration appConfiguration, IService<IDocument> documentService, IDocument document)
        {
            if (appConfiguration == null) throw new ArgumentNullException("Instance of CoshhWindow cannot be created without an app configuration");
            else this.appConfiguration = appConfiguration;

            if (documentService == null) throw new ArgumentNullException("Instance of CoshhWindow cannot be created without a service");
            else this.service = documentService;

            if (document == null) throw new ArgumentNullException("Instance of CoshhWindow cannot be created without a document");
            else this.content = document;

            //TODO: Change to Func<IWindow<obj>> Dependancy injections?
            commands = new WindowICommands(this);
            ribbon = new CoshhRibbon(this);
            view = new CoshhWindowView(this);
            view.InputBindings.AddRange(commands.Hotkeys);
        }

        private readonly IConfiguration appConfiguration;

        private readonly Window view;
        /// <summary>
        /// Get the CoshhWindow view.
        /// </summary>
        public Window View
        {
            get 
            { 
                return view; 
            }
        }
        /// <summary>
        /// Gets the CoshhWindow control (IViewable)
        /// </summary>
        Control IViewable.View
        {
            get 
            { 
                return view; 
            }
        }

        private readonly IWindowCommands commands;
        /// <summary>
        /// The ICommands (and hotkeys) available to this CoshhWindow
        /// </summary>
        public IWindowCommands Commands
        {
            get 
            { 
                return commands; 
            }
        }

        private readonly IRibbon ribbon;
        /// <summary>
        /// Gets the IRibbon viewable for the CoshhWindow
        /// </summary>
        public IRibbon Ribbon
        {
            get 
            { 
                return ribbon; 
            }
        }

        private IDocument content;
        /// <summary>
        /// Gets the IDocument in this CoshhWindow.
        /// </summary>
        /// <remarks>Nullable: IRibbonWindow may contain no IDocument</remarks>
        public IDocument Content
        {
            get 
            { 
                return content; 
            }
            set 
            { 
                content = value;
                if (ContentChanged != null)
                {
                    ContentChanged(content);
                }                
                RaisePropertyChanged("Content");
            }
        }
        /// <summary>
        /// Event that triggers when its IDocument changes.
        /// </summary>
        public event Action<IDocument> ContentChanged;

        private IService<IDocument> service;
        /// <summary>
        /// Gets the IDocumentService I/O service used by this CoshhWindow
        /// </summary>
        public IService<IDocument> Service
        {
            get 
            { 
                return service; 
            }
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
