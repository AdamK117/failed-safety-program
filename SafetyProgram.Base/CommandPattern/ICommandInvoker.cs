namespace SafetyProgram.Base
{
    /// <summary>
    /// Defines an interface for a class that invokes IInvokedCommand's.
    /// </summary>
    public interface ICommandInvoker
    {
        /// <summary>
        /// Invoke a command.
        /// </summary>
        /// <param name="command">The command to invoke.</param>
        void InvokeCommand(IInvokedCommand command);     
    }
}
