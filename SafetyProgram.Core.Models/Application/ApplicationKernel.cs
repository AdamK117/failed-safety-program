using System;
using SafetyProgram.Base;
using SafetyProgram.Core.IO;

namespace SafetyProgram.Core.Models
{
    /// <summary>
    /// Defines a standard implementation of IApplicationKernel
    /// </summary>
    public sealed class ApplicationKernel : IApplicationKernel
    {
        /// <summary>
        /// Construct an instance of an application kernel.
        /// </summary>
        public ApplicationKernel(IConfiguration configuration)
        {
            this.Configuration = configuration;

            document = new Document(
                new System.Collections.ObjectModel.ObservableCollection<IDocumentObject>(),
                new A4Format()
            );
            service = null;
        }

        private IDocument document;

        /// <summary>
        /// Get or set the document open in the application, may be null.
        /// </summary>
        public IDocument Document
        {
            get
            {
                return document;
            }
            set
            {
                document = value;
                DocumentChanged.Raise(this, document);
            }
        }

        /// <summary>
        /// Occurs when the document in the appplication changes.
        /// </summary>
        public event EventHandler<GenericPropertyChangedEventArg<IDocument>> DocumentChanged;

        private IIOService<IDocument> service;

        /// <summary>
        /// Get or set the service used by the application.
        /// </summary>
        public IIOService<IDocument> Service
        {
            get
            {
                return service;
            }
            set
            {
                Helpers.NullCheck(service);
                service = value;
                ServiceChanged.Raise(this, service);
            }
        }

        /// <summary>
        /// Occurs when the IO service used by the application changes.
        /// </summary>
        public event EventHandler<GenericPropertyChangedEventArg<IIOService<IDocument>>> ServiceChanged;

        /// <summary>
        /// Get the configuration used by the application.
        /// </summary>
        public IConfiguration Configuration
        {
            get;
            private set;
        }
    }
}
