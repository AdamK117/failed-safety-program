namespace SafetyProgram.Core.IO
{
    /// <summary>
    /// Defines an interface for a service that saves data to storage.
    /// </summary>
    /// <typeparam name="T">The type of the data being stored.</typeparam>
    public interface IInputService<in T>
    {
        /// <summary>
        /// Save the data using the service.
        /// </summary>
        /// <param name="data">The data to be saved.</param>
        void Save(T data);

        /// <summary>
        /// Check if the data can be saved.
        /// </summary>
        /// <param name="data">If the data can be saved.</param>
        /// <returns></returns>
        bool CanSave(T data);

        /// <summary>
        /// Save the data using the service. The service will provide a means of the user specifying the save location.
        /// </summary>
        /// <param name="data"></param>
        void SaveAs(T data);

        /// <summary>
        /// Check if the data can be saved to a specific location in storage (or if the service provides this facility).
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool CanSaveAs(T data);

        /// <summary>
        /// Disconnect this service from the storage.
        /// </summary>
        void Disconnect();
    }
}
