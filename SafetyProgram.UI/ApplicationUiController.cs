using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SafetyProgram.Core;

namespace SafetyProgram.UI
{
    /// <summary>
    /// Defines a standard implementation of IApplicationUiController.
    /// </summary>
    public sealed class ApplicationUiController : IApplicationUiController
    {
        public ApplicationUiController(IApplicationKernel applicationKernel)
        {
            
        }

        public System.Windows.Window View
        {
            get { throw new NotImplementedException(); }
        }
    }
}
