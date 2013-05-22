using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.MainWindow.Commands;

namespace SafetyProgram.MainWindow
{
    /// <summary>
    /// Defines a CoshhWindow, a window composed of:
    ///     -A document, which is displayed in the window
    ///     -A service, which saves/loads/creates new documents
    ///     -A view, which is displayed to the user
    ///     -Commands which are digested by the view
    ///     -A ribbon which appears at the top of the view
    /// </summary>
    internal sealed class CoshhWindow<TContent> : ICoshhWindowT<TContent>
        where TContent : IViewable
    {

        /// <summary>
        /// Constructs a CoshhWindow IRibbonWindow.
        /// </summary>
        /// <param name="contentService">The service used by the IRibbonWindow to load IDocuments into it</param>
        /// <param name="content">The IDocument shown by the IRibbonWindow on construction</param>
        public CoshhWindow(
            IIOService<TContent> contentService, 
            TContent content,
            Func<ICoshhWindowT<TContent>, Window> viewConstructor,
            Func<ICoshhWindowT<TContent>, IWindowCommands> commandsConstructor,
            Func<ICoshhWindowT<TContent>, IRibbon> ribbonConstructor
            )
        {
            if (
                contentService == null ||
                content == null ||
                commandsConstructor == null ||
                ribbonConstructor == null ||
                viewConstructor == null
                )
            {
                throw new ArgumentNullException();
            }
            else
            {
                this.service = contentService;
                this.content = content;
                this.commands = commandsConstructor(this);
                this.ribbon = ribbonConstructor(this);
                this.view = viewConstructor(this);
            }
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

        private TContent content;
        /// <summary>
        /// Gets the IDocument in this CoshhWindow.
        /// </summary>
        /// <remarks>Nullable: IRibbonWindow may contain no IDocument</remarks>
        public TContent Content
        {
            get 
            { 
                return content; 
            }
            set 
            { 
                content = value;
                ContentChanged.Raise(this, content);
                PropertyChanged.Raise(this, "Content");
            }
        }
        IViewable IContentWindow.Content
        {
            get 
            {
                return content;
            }
        }
        /// <summary>
        /// Event that triggers when its IDocument changes.
        /// </summary>
        public event EventHandler<GenericPropertyChangedEventArg<TContent>> ContentChanged;

        private IIOService<TContent> service;
        /// <summary>
        /// Gets the IDocumentService I/O service used by this CoshhWindow
        /// </summary>
        public IIOService<TContent> Service
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
        public void ChangeService(IIOService<TContent> newService)
        {
            if (newService != null)
            {
                service = newService;
                ServiceChanged.Raise(this, service);
                PropertyChanged.Raise(this, "Service");
            }
            else throw new ArgumentNullException("newService", "The CoshhWindow's service cannot be set to null, a valid service must be set");
        }        
        /// <summary>
        /// Event that triggers if the CoshhWindow's IDocumentService changes.
        /// </summary>
        public event EventHandler<GenericPropertyChangedEventArg<IIOService<TContent>>> ServiceChanged;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
