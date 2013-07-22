namespace SafetyProgram.IO
{
    /// <summary>
    /// Defines a service for loading data from storage.
    /// </summary>
    /// <typeparam name="T">The type of data to be loaded from storage.</typeparam>
    public interface IOutputService<out T>
    {
        /// <summary>
        /// Load data from storage using the service.
        /// </summary>
        /// <returns>The loaded data</returns>
        T Load();

        /// <summary>
        /// Check if data may be loaded using the service.
        /// </summary>
        /// <returns>If data may be loaded.</returns>
        bool CanLoad();

        /// <summary>
        /// Disconnect from storage.
        /// </summary>
        void Disconnect();
    }
}
