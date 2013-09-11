using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace SafetyProgram.Base
{
    public interface IWindowController : IUiController
    {
        new Window View { get; }
    }
}
