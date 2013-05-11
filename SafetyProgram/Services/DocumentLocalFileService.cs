using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

using SafetyProgram.Base.Interfaces;
using SafetyProgram.Document;

namespace SafetyProgram.Services
{
    /// <summary>
    /// Defines a service that uses CoshhDocument file factories to generate IDocuments.
    /// </summary>
    internal sealed class DocumentLocalFileService : IService<IDocument>
    {
        private readonly bool canNew = true, canLoad = true, canSave = true, canSaveAs = true;

        private string path;

        private readonly IConfiguration appConfiguration;
        private readonly CoshhDocumentLocalFileFactory factory;

        public DocumentLocalFileService(IConfiguration appConfiguration)
        {
            this.appConfiguration = appConfiguration;
            factory = new CoshhDocumentLocalFileFactory(appConfiguration);
        }

        /// <summary>
        /// Creates a new IDocument
        /// </summary>
        /// <returns>A newly constructed IDocument</returns>
        public IDocument New()
        {
            if (CanNew())
            {
                path = "";
                return factory.CreateNew();
            }
            else throw new InvalidOperationException("Cannot create a new document");
        }

        /// <summary>
        /// Indicates if a new IDocument may be made as a LocalFile.
        /// </summary>
        /// <returns></returns>
        public bool CanNew()
        {
            return canNew;
        }

        /// <summary>
        /// Load an IDocument using the local file system.
        /// </summary>
        /// <returns>Loaded IDocument</returns>
        /// <exception cref="FileNotFoundException">Thrown if the user specified file could not be found</exception>
        /// <exception cref="System.IO.InvalidDataException">Thrown if the loaded data is invalid</exception>
        /// <exception cref="ArgumentException">Thrown if the user cancels out of loading a file</exception>
        public IDocument Load()
        {
            if (CanLoad())
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog()
                {
                    Filter = "Coshh Documents (.xml)|*.xml",
                    Multiselect = false,
                    CheckFileExists = true
                };

                switch (openFileDialog1.ShowDialog())
                {
                    case DialogResult.OK:
                        if (File.Exists(openFileDialog1.FileName))
                        {
                            path = openFileDialog1.FileName;

                            XElement xDoc = XElement.Load(path);

                            return factory.Load(xDoc);
                        }
                        else throw new FileNotFoundException("The file selected does not exist", openFileDialog1.FileName);

                    default:
                        throw new ArgumentException("User cancelled out of selecting a file to load");
                }
            }
            else throw new InvalidOperationException();            
        }

        /// <summary>
        /// Indicates if an IDocument may be loaded as a LocalFile
        /// </summary>
        /// <returns></returns>
        public bool CanLoad()
        {
            return canLoad;
        }

        /// <summary>
        /// Saves the IDocument using the local file system.
        /// </summary>
        /// <param name="document">IDocument to be saved</param>
        /// <exception cref="System.UnauthorizedAccessException">Thrown if the specified path has access restrictions</exception>
        /// <exception cref="System.ArgumentException">Thrown if the user cancels out of saving</exception>
        /// <exception cref="System.IO.InvalidDataException">Thrown if something in the IDocument is invalid for saving</exception>
        public void Save(IDocument document)
        {
            if (CanSave(document))
            {
                if (String.IsNullOrWhiteSpace(path))
                {
                    SaveAs(document);
                }
                else
                {
                    XDocument xDoc = new XDocument();

                    xDoc.Add(factory.Store(document as CoshhDocument));

                    xDoc.Save(path);
                }
            }
            else throw new InvalidOperationException();
                        
        }

        /// <summary>
        /// Indicates if the IDocument can be saved as a local file
        /// </summary>
        /// <returns></returns>
        public bool CanSave(IDocument document)
        {
            return canSave;
        }

        /// <summary>
        /// Saves an IDocument on the local file system (as Xml).
        /// </summary>
        /// <param name="document">IDocument to save.</param>
        /// <exception cref="System.UnauthorizedAccessException">Thrown when user selects a directory with no write permissions.</exception>
        /// <exception cref="System.ArgumentException">Thrown if the user cancels out of a SaveAs dialog.</exception>
        /// <exception cref="System.IO.InvalidDataException">Thrown if invalid data is present in the IDocument</exception>
        public void SaveAs(IDocument document)
        {
            if (CanSaveAs(document))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Coshh Safety Document|*.xml";
                saveFileDialog.Title = "Save As";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //TODO: Figure out why this is blocking saving a file
                    //Directory.GetAccessControl(path);
                    path = saveFileDialog.FileName;
                    Save(document);
                }
                else throw new ArgumentException("User cancelled out of SaveAs dialog");
            }
            else throw new InvalidOperationException();            
        }

        /// <summary>
        /// Indicates if an IDocument can be saved to a specific location (Save As) on the local file system.
        /// </summary>
        /// <returns></returns>
        public bool CanSaveAs(IDocument document)
        {
            return canSaveAs;
        }

        /// <summary>
        /// Closes the IDocument. Attempts to save it if it has been edited etc.
        /// </summary>
        /// <exception cref="System.UnauthorizedAccessException">Thrown if access to a restricted folder is attempted (usually when trying to save the IDocument).</exception>
        /// <exception cref="System.ArgumentException">Thrown if the user cancels out of closing the IDocument.</exception>
        /// <exception cref="System.IO.InvalidDataException">Thrown if invalid data is present in the IDocument.</exception>
        public void Close(IDocument document)
        {
            //Ask to save changes (if applicable)
            if (document.EditedFlag == true)
            {
                switch (MessageBox.Show("Do you want to save changes to " + document.Title + "?", "", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:
                        Save(document);
                        break;

                    case DialogResult.No:
                        break;

                    default:
                        throw new ArgumentException("User cancelled out of closing the document");
                }
            }

            //TODO: Close document implementation
        }
    }
}
