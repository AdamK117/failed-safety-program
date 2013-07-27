namespace SafetyProgram.IO
{
    /// <summary>
    /// Defines an interface for a service that loads from and saves to storage.
    /// </summary>
    /// <typeparam name="T">The type of data handled by the service.</typeparam>
    public interface IIOService<T> :
        IInputService<T>,
        IOutputService<T>
    {
        /// <summary>
        /// Create a new object using the service. The service may create placeholders in the storage, notify other services, etc.
        /// </summary>
        /// <returns>A newly created object from the service.</returns>
        T New();

        /// <summary>
        /// Check if new objects may be created using the service.
        /// </summary>
        /// <returns>If a new object may be created using the service.</returns>
        bool CanNew();

        /// <summary>
        /// Disconnect from the storage this service handles.
        /// </summary>
        new void Disconnect();
    }
}
