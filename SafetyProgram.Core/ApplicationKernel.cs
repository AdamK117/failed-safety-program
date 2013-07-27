using System;
using SafetyProgram.Base;
using SafetyProgram.IO;
using SafetyProgram.Models;

namespace SafetyProgram.Core
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
            document = null;
            service = null;
        }

        private IDocument document;

        /// <summary>
        /// Get or set the document open in the application, may be null.
        /// </summary>
        public Models.IDocument Document
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
        public event EventHandler<Base.GenericPropertyChangedEventArg<Models.IDocument>> DocumentChanged;

        private IIOService<IDocument> service;

        /// <summary>
        /// Get or set the service used by the application.
        /// </summary>
        public IO.IIOService<Models.IDocument> Service
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
        public event EventHandler<Base.GenericPropertyChangedEventArg<IO.IIOService<Models.IDocument>>> ServiceChanged;

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
