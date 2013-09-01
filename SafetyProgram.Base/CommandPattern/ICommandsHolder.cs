using System.Collections.Generic;
using System.Windows.Input;

namespace SafetyProgram.Base
{
    /// <summary>
    /// Defines an interface for a class that holds a set of hotkeys 
    /// for its commands.
    /// </summary>
    public interface ICommandsHolder
    {
        /// <summary>
        /// Get the hotkeys for the commands held.
        /// </summary>
        List<InputBinding> Hotkeys { get; }
    }
}
