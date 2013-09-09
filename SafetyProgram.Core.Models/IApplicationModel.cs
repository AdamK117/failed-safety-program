namespace SafetyProgram.Core.Models
{
    public interface IApplicationModel
    {
        /// <summary>
        /// Get the unique identifier for the model.
        /// </summary>
        string Identifier { get; }
    }
}
