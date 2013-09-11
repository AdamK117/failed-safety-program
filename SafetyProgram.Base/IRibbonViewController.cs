using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fluent;

namespace SafetyProgram.Base
{
    public interface IRibbonViewController : IUiController
    {
        new Ribbon View { get; }
    }
}
