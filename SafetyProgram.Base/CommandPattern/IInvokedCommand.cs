
namespace SafetyProgram.Base
{
    /// <summary>
    /// Defines an interface for a command that may both execute and
    /// unexecute (undo). Implementations that use this shall only call
    /// execute before calling unexecute.
    /// </summary>
    public interface IInvokedCommand
    {
        /// <summary>
        /// Execute this command.
        /// </summary>
        void Execute();

        /// <summary>
        /// UnExecute the command. This may only be called if execute was
        /// previously called.
        /// </summary>
        void UnExecute();
    }
}
