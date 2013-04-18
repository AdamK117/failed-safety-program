using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

using SafetyProgram.DocObjects;
using SafetyProgram.DocObjects.ChemicalTable;
using SafetyProgram.BaseClasses.DocumentFormats;
using SafetyProgram.BaseClasses;

namespace SafetyProgram.Document.Services
{
    public class CoshhDocumentLocalFileService : ICoshhDocumentService
    {
        private bool canNew = true, canLoad = true, canSave = true, canSaveAs = true;

        private string path;

        /// <summary>
        /// Creates a new CoshhDocument
        /// </summary>
        /// <returns>A new CoshhDocument</returns>
        public ICoshhDocument New()
        {
            path = "";
            return new CoshhDocument(new A4DocFormat());
        }

        /// <summary>
        /// Indicates if a new CoshhDocument may be made as a LocalFile.
        /// </summary>
        /// <returns></returns>
        public bool CanNew()
        {
            return canNew;
        }

        /// <summary>
        /// Load a CoshhDocument using the local file system.
        /// </summary>
        /// <returns>The loaded CoshhDocument.</returns>
        /// <exception cref="FileNotFoundException">Thrown if the user specified file could not be found</exception>
        /// <exception cref="ArgumentException">Thrown if the user cancels out of loading a file</exception>
        public ICoshhDocument Load()
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
                    if(File.Exists(openFileDialog1.FileName))
                    {
                        return loadFile(openFileDialog1.FileName);
                    }
                    else throw new FileNotFoundException("The file selected does not exist", openFileDialog1.FileName);

                default:
                    throw new ArgumentException("User cancelled out of selecting a file to load");
            }
        }

        /// <summary>
        /// Indicates if a CoshhDocument may be loaded as a LocalFile
        /// </summary>
        /// <returns></returns>
        public bool CanLoad()
        {
            return canLoad;
        }

        /// <summary>
        /// Saves the CoshhDocument using the local file system.
        /// </summary>
        /// <param name="document">Document to be saved</param>
        /// <exception cref="System.UnauthorizedAccessException">Thrown if the specified path has access restrictions</exception>
        /// <exception cref="System.ArgumentException">Thrown if the user cancels out of saving</exception>
        public void Save(ICoshhDocument document)
        {
            if (String.IsNullOrWhiteSpace(path))
            {
                SaveAs(document);
            }
            else
            {
                //Create a new xDoc to be populated with document data
                XDocument xDoc = new XDocument();

                foreach (DocObject docObject in document.Body)
                {
                    if (docObject is ChemicalTable)
                    {
                        ChemicalTable chemTable = (ChemicalTable)docObject;
                        xDoc.Add(new XElement("coshh", chemTable.Save()));
                    }
                }
                xDoc.Save(path);
            }            
        }

        /// <summary>
        /// Indicates if the file can be saved as a local file
        /// </summary>
        /// <returns></returns>
        public bool CanSave()
        {
            return canSave;
        }

        /// <summary>
        /// Saves a CoshhDocument on the local file system (as Xml).
        /// </summary>
        /// <param name="document">CoshhDocument to save.</param>
        /// <exception cref="System.UnauthorizedAccessException">Thrown when user selects a directory with no write permissions.</exception>
        /// <exception cref="System.ArgumentException">Thrown if the user cancels out of a SaveAs dialog.</exception>
        public void SaveAs(ICoshhDocument document)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Coshh Safety Document|*.xml";
            saveFileDialog.Title = "Save As";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Directory.GetAccessControl(path);
                path = saveFileDialog.FileName;
                Save(document);
            }
            else throw new ArgumentException("User cancelled out of SaveAs dialog");
        }

        /// <summary>
        /// Indicates if a CoshhDocument can be saved to a specific location (Save As) on the local file system.
        /// </summary>
        /// <returns></returns>
        public bool CanSaveAs()
        {
            return canSaveAs;
        }

        /// <summary>
        /// Closes the CoshhDocument. Performs checks on edited etc. to save locally if necessay.
        /// </summary>
        /// <exception cref="System.ArgumentException">Thrown if the user cancels out of closing the CoshhDocument.</exception>
        public void Close(ICoshhDocument document)
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

        /// <summary>
        /// Provies the actual (local file) implementation for loading a document.
        /// </summary>
        /// <param name="path">Path to the document.</param>
        /// <returns>Loaded CoshhDocument</returns>
        /// <exception cref="System.InvalidDataException">Thrown if invalid data is found</exception>
        private ICoshhDocument loadFile(string path)
        {
            CoshhDocument loadedDoc = new CoshhDocument(new A4DocFormat());

            loadedDoc.Title = path;

            //Pass all the data into an xDoc for easier parsing
            XDocument xDoc = XDocument.Load(path);

            if (xDoc.Element("coshh") != null)
            {
                if (xDoc.Element("coshh").Attribute("title").Value != null)
                {
                    loadedDoc.Title = xDoc.Element("coshh").Attribute("title").Value;
                }

                if (xDoc.Element("coshh").Element("chemicals") != null)
                {
                    XElement chemTable = xDoc.Element("coshh").Element("chemicals");

                    DocObject docObj = new ChemicalTable();
                    docObj.Load(chemTable);
                    loadedDoc.Body.Add(docObj);
                }

                return loadedDoc;
            }

            else throw new InvalidDataException("Invalid data input, cannot find Coshh XML in file");
        }
    }
}
