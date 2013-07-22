using System;
using SafetyProgram.Base;

namespace SafetyProgram.Models
{
    /// <summary>
    /// Defines an implementation of an IApplicationKernel. 
    /// </summary>
    public sealed class ApplicationKernel : IApplicationKernel
    {
        /// <summary>
        /// Construct an instance of an application kernel
        /// </summary>
        /// <param name="window">The window kernel to be used by the application</param>
        public ApplicationKernel(IWindowKernel window)
        {
            this.window = window;
        }

        private IWindowKernel window;

        /// <summary>
        /// Get or set the current window kernel (may be null).
        /// </summary>
        public IWindowKernel Window
        {
            get { return window; }
            set
            {
                this.window = value;
                WindowChanged.Raise(this, this.window);
            }
        }

        public event EventHandler<Base.GenericPropertyChangedEventArg<IWindowKernel>> WindowChanged;
    }
}
