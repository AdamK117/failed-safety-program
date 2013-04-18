using SafetyProgram.Document;

namespace SafetyProgram.Document.Services
{
    public interface ICoshhDocumentService
    {
        /// <summary>
        /// Creates a new CoshhDocument using the CoshhDocumentService.
        /// </summary>
        /// <returns>A new CoshhDocument</returns>
        ICoshhDocument New();

        /// <summary>
        /// Indicates if a new CoshhDocument may be made using the CoshhDocument service.
        /// </summary>
        /// <returns></returns>
        bool CanNew();

        /// <summary>
        /// Loads a CoshhDocument using the CoshhDocumentService
        /// </summary>
        /// <returns>Loaded CoshhDocument</returns>
        /// <exception cref="System.IO.FileNotFoundException">Thrown if the CoshhDocument could not be found in a user specified location.</exception>
        /// <exception cref="System.ArgumentException">Thrown if the loading is cancelled in any way.</exception>
        ICoshhDocument Load();

        /// <summary>
        /// Indicates if a CoshhDocument may be loaded from the CoshhDocumentService.
        /// </summary>
        /// <returns></returns>
        bool CanLoad();

        /// <summary>
        /// Saves the CoshhDocument using the CoshhDocumentService
        /// </summary>
        /// <param name="document">CoshhDocument to be saved</param>
        /// <exception cref="System.UnauthroizedAccessException">Thrown if the service attempts to save the CoshhDocument to a restricted location</exception>
        /// <exception cref="System.ArgumentException">Thrown if the operation is cancelled.</exception>
        void Save(ICoshhDocument document);

        /// <summary>
        /// Indicates if the CoshhDocument can be saved.
        /// </summary>
        /// <returns></returns>
        bool CanSave();

        /// <summary>
        /// Saves the CoshhDocument to a user specified location.
        /// </summary>
        /// <param name="document">CoshhDocument to save</param>
        /// <exception cref="System.UnauthorizedAccessException">Thrown if the user selects a location with restricted access.</exception>
        /// <exception cref="System.ArgumentException">Thrown if the operation is cancelled in any way.</exception>
        void SaveAs(ICoshhDocument document);

        /// <summary>
        /// Indicates if the file can be saved to a user specified location (Saved As) using the CoshhDocumentService
        /// </summary>
        /// <returns></returns>
        bool CanSaveAs();

        /// <summary>
        /// Closes the CoshhDocument using the CoshhDocumentService.
        /// </summary>
        /// <param name="document">CoshhDocument to close</param>
        /// <exception cref="System.ArgumentException">Thrown if the closing of the CoshhDocument is cancelled.</exception>
        void Close(ICoshhDocument document);
    }
}