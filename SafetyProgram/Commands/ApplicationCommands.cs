using System;
using System.Windows;
using System.Windows.Input;
using SafetyProgram.Base;
using SafetyProgram.Core;

namespace SafetyProgram.Commands
{
    /// <summary>
    /// Defines an implementation of IApplicationCommands, the commands available to the application.
    /// </summary>
    public sealed class ApplicationCommands : IApplicationCommands
    {
        /// <summary>
        /// Construct an instance of the application commands holder.
        /// </summary>
        /// <param name="mainWindow"></param>
        public ApplicationCommands(IApplicationKernel applicationKernel)
        {
            //Constuct commands to act on the application kernel
        }

        /// <summary>
        /// Get a command that exits the application.
        /// </summary>
        public ICommand Exit
        {
            get { throw new NotImplementedException(); }
        }
    }
}
