namespace SafetyProgram.Core.Models
{
    /// <summary>
    /// Defines an implementation for the description of a repository.
    /// </summary>
    public interface IRepositoryInfo
    {
        /// <summary>
        /// Get the data source for the repository.
        /// </summary>
        /// <example>'database', 'local'</example>
        string Source { get; }

        /// <summary>
        /// Get the type of content in the repository.
        /// </summary>
        /// <example>'ChemicalModel', 'Document'</example>
        string ContentType { get; }

        /// <summary>
        /// Get the absolute path to the repository.
        /// </summary>
        /// <example>'C:\Some\Path\To\The\Repository.xml'</example>
        string Path { get; }

        /// <summary>
        /// Get the login to the repository (if applicable).
        /// </summary>
        string Login { get; }

        /// <summary>
        /// Get the password to the repository (if applicable).
        /// </summary>
        string Password { get; }
    }
}
