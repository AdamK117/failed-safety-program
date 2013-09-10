using SafetyProgram.Core.Models;

namespace SafetyProgram.UI
{
    /// <summary>
    /// Defines an interface for a viewmodel, a UI proxy
    /// for a model.
    /// </summary>
    public interface IViewModel
    {
        /// <summary>
        /// Get the model this viewmodel represents.
        /// </summary>
        IApplicationModel Model { get; }
    }
}
