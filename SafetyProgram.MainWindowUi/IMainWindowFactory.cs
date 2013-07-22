using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SafetyProgram.IO;
using SafetyProgram.Models;

namespace SafetyProgram.MainWindowUi
{
    /// <summary>
    /// Defines an interface for a factory that uses a configuration file to generate the main window.
    /// </summary>
    public interface IMainWindowFactory : IStorageConverter<IMainWindowController, IConfiguration>
    {
    }
}
