using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Commands;
using SafetyProgram.Document;

namespace SafetyProgram
{
    /// <summary>
    /// Defines a CoshhWindow, a window composed of:
    ///     -A document, which is displayed in the window
    ///     -A service, which saves/loads/creates new documents
    ///     -A view, which is displayed to the user
    ///     -Commands which are digested by the view
    ///     -A ribbon which appears at the top of the view
    /// </summary>
    internal sealed class CoshhWindow : ICoshhWindow
    {
        private readonly IConfiguration appConfiguration;

        /// <summary>
        /// Constructs a CoshhWindow IRibbonWindow.
        /// </summary>
        /// <param name="documentService">The service used by the IRibbonWindow to load IDocuments into it</param>
        /// <param name="document">The IDocument shown by the IRibbonWindow on construction</param>
        public CoshhWindow(
            IConfiguration appConfiguration, 
            IService<CoshhDocument> documentService, 
            CoshhDocument document,
            Func<ICoshhWindow, Window> viewConstructor,
            Func<ICoshhWindow, IWindowCommands> commandsConstructor,
            Func<ICoshhWindow, IRibbon> ribbonConstructor
            )
        {
            if (
                appConfiguration != null &&
                documentService != null &&
                document != null &&
                commandsConstructor != null &&
                ribbonConstructor != null &&
                viewConstructor != null
                )
            {
                this.appConfiguration = appConfiguration;
                this.service = documentService;
                this.content = document;
                this.commands = commandsConstructor(this);
                this.ribbon = ribbonConstructor(this);
                this.view = viewConstructor(this);
            }
            else throw new ArgumentNullException();
        }

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

        private CoshhDocument content;
        /// <summary>
        /// Gets the IDocument in this CoshhWindow.
        /// </summary>
        /// <remarks>Nullable: IRibbonWindow may contain no IDocument</remarks>
        public CoshhDocument Content
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
                PropertyChanged.Raise(this, "Content");
            }
        }
        IViewable IContentWindow.Content
        {
            get { return content; }
        }
        /// <summary>
        /// Event that triggers when its IDocument changes.
        /// </summary>
        public event Action<CoshhDocument> ContentChanged;

        private IService<CoshhDocument> service;
        /// <summary>
        /// Gets the IDocumentService I/O service used by this CoshhWindow
        /// </summary>
        public IService<CoshhDocument> Service
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
        public void ChangeService(IService<CoshhDocument> newService)
        {
            if (newService != null)
            {
                service = newService;
                if (ServiceChanged != null)
                {
                    ServiceChanged(service);
                }
                PropertyChanged.Raise(this, "Service");
                PropertyChanged.Raise(this, "Commands");
            }
            else throw new ArgumentNullException("newService", "The CoshhWindow's service cannot be set to null, a valid service must be set");
        }        
        /// <summary>
        /// Event that triggers if the CoshhWindow's IDocumentService changes.
        /// </summary>
        public event Action<IService<CoshhDocument>> ServiceChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        
    }
}
