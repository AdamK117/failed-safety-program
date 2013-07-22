using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SafetyProgram.Base;

namespace SafetyProgram.Models
{
    /// <summary>
    /// Defines an implementation of IWindowKernel. The model of the window in the application.
    /// </summary>
    public sealed class WindowKernel : IWindowKernel
    {
        /// <summary>
        /// Construct an instance of a window kernel, the model of the window in the application.
        /// </summary>
        /// <param name="document"></param>
        public WindowKernel(IDocument document)
        {
            this.document = document;
        }

        IDocument document;

        /// <summary>
        /// Get or set the current document in the window (may be null)
        /// </summary>
        public IDocument Document
        {
            get { return document; }
            set
            {
                this.document = value;
                DocumentChanged.Raise(this, this.document);
            }
        }

        /// <summary>
        /// Occurs when the document in the window changes.
        /// </summary>
        public event EventHandler<Base.GenericPropertyChangedEventArg<IDocument>> DocumentChanged;
    }
}
