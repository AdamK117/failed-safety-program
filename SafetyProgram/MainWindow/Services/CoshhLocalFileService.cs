using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

using SafetyProgram.Document;
using SafetyProgram.DocObjects;
using SafetyProgram.DocObjects.ChemicalTable;
using SafetyProgram.Models.DataModels;

namespace SafetyProgram.MainWindow.Services
{
    public class CoshhLocalFileService : ICoshhDataService
    {
        private readonly CoshhWindow window;

        private string path;

        /// <summary>
        /// Constructs an ICoshhDataService for local (i.e. not database stored) files.
        /// </summary>
        /// <param name="window"></param>
        public CoshhLocalFileService(CoshhWindow window)
        {
            this.window = window;
        }

        /// <summary>
        /// Creates a new document in the window.
        /// </summary>
        /// <returns>If a new document was sucessfully created (e.g. may not be if the last one couldn't be closed).</returns>
        public bool New()
        {
            if (window.Document == null || Close())
            {
                path = "";
                window.Document = new CoshhDocument();
                return true;
            }
            else { return false; }
        }

        /// <summary>
        /// Saves the windows current document. Prompts the user if no save path is known.
        /// </summary>
        /// <returns>If the file sucessfully saved.</returns>
        public bool Save()
        {
            return saveFile(path);
        }

        /// <summary>
        /// Saves the document at the user-specified location.
        /// </summary>
        /// <returns>If the file was sucessfully saved.</returns>
        public bool SaveAs()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Coshh Safety Document|*.xml";
            saveFileDialog.Title = "Save As";

            DialogResult userResponse = saveFileDialog.ShowDialog();

            if (userResponse == DialogResult.OK)
            {
                path = saveFileDialog.FileName;
                Save();
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Loads a document into the window.
        /// </summary>
        /// <returns>If the file has sucessfully loaded.</returns>
        public bool Load()
        {
            bool loaded = false;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Coshh Documents (.xml)|*.xml";
            openFileDialog1.Multiselect = false;

            DialogResult dialogResult = openFileDialog1.ShowDialog();

            switch (dialogResult)
            {
                case DialogResult.OK:
                    if (
                        Close() 
                        && setPath(openFileDialog1.FileName) 
                        && loadFile(Path())
                    )
                    {
                        loaded = true;
                    }
                    break;

                default:
                    loaded = false;
                    break;
            }

            return loaded;
        }

        /// <summary>
        /// Closes the current document.
        /// </summary>
        /// <returns>If the document sucessfully closed.</returns>
        public bool Close()
        {
            //Inbuilt redundancy: Check if it's already closed.
            if (window.Document == null) { return true; }

            bool closed = true;

            // If the file has been edited, prompt the user to save changes. Else, close it.
            if (window.Document.Edited == true)
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to save changes you made to " + window.Document.Title + "?", "Save changes.", MessageBoxButtons.YesNoCancel);

                switch (dialogResult)
                {
                    //User chose to save & close.
                    case System.Windows.Forms.DialogResult.Yes:
                        closed = Save() ? true : false;
                        break;

                    //User chose to close without saving.
                    case DialogResult.No:
                        closed = true;
                        break;

                    //User chose to not close the document.
                    case DialogResult.Cancel:
                        closed = false;
                        break;
                }
            }

            //If the user chose to close (with/without saving)
            if (closed == true)
            {
                //Close the document
                window.Document = null;
                return true;
            }
            else { return false; }
        }        

        //Private members (implementation specific)
        private bool setPath(string path)
        {
            if (File.Exists(path))
            {
                this.path = path;
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Provies the actual (local file) implementation for loading a document.
        /// </summary>
        /// <param name="path">Path to the document.</param>
        /// <returns>If the document sucessfully loaded (no parsing errors etc.).</returns>
        private bool loadFile(string path)
        {
            //Flag that goes false if theres a load error.
            bool noError = true;

            //Create a blank CoshhDocObject
            CoshhDocument loadedDoc = new CoshhDocument();

            loadedDoc.Title = path;

            //Pass all the data into an xDoc for easier parsing
            XDocument xDoc = XDocument.Load(path);
          
            XElement chemTable = xDoc.Element("coshh").Element("chemicals");

            loadedDoc.Body.Add(
                new ChemicalTable(readChemicalTable(chemTable))
            );

            window.Document = loadedDoc;

            return noError;
        }

        /// <summary>
        /// Provies the actual (localfile) implementation for saving a file.
        /// </summary>
        /// <param name="path">Location where the file is to be saved</param>
        /// <returns>If the file sucessfully saved</returns>
        private bool saveFile(string path)
        {
            bool noError = true;

            //Create a new xDoc to be populated with document data
            XDocument xDoc = new XDocument();
            XElement coshh = new XElement("coshh");

            foreach (DocObject docObject in window.Document.Body)
            {
                if (docObject is ChemicalTable)
                {
                    ChemicalTable chemTable = (ChemicalTable)docObject;
                    xDoc.Add(new XElement("coshh", writeChemicalTable(chemTable.Chemicals)));
                }
            }

            xDoc.Save(path);

            return noError;
        }

        /// <summary>
        /// Returns the path of the file.
        /// </summary>
        /// <returns>Local filesystem path.</returns>
        private string Path()
        {
            return path;
        }

        private IEnumerable<CoshhChemicalModel> readChemicalTable(XElement chemicalTable)
        {
            return (
                from XElement chemical in chemicalTable.Elements("chemical")
                    select new CoshhChemicalModel()
                    {
                        Name = chemical.Element("name").Value,
                        Value = float.Parse(chemical.Element("amount").Element("value").Value),
                        Unit = chemical.Element("amount").Element("unit").Value,
                        Hazards = new ObservableCollection<HazardModel>(readHazardNode(chemical.Element("hazards")))
                    }
            );
        }

        private IEnumerable<HazardModel> readHazardNode(XElement hazardNode)
        {
            return (
                from XElement hazard in hazardNode.Elements("hazard")
                select new HazardModel()
                {
                    Hazard = hazard.Value,
                    SignalWord = hazard.Attribute("signalword") == null ? null : hazard.Attribute("signalword").Value,
                    Symbol = hazard.Attribute("symbol") == null ? null : hazard.Attribute("symbol").Value
                }
            );
        }

        private XElement writeChemicalTable(IEnumerable<CoshhChemicalModel> chemicals)
        {
            return (
                new XElement("chemicals",
                    from chemical in chemicals
                    select new XElement("chemical",
                        new XElement("name", chemical.Name),
                        new XElement("amount",
                            new XElement("value", chemical.Value),
                            new XElement("unit", chemical.Unit)
                        ),
                        writeHazards(chemical.Hazards)                        
                    )
                )
            );
        }

        private XElement writeHazards(IEnumerable<HazardModel> hazards)
        {
            return (
                new XElement("hazards",
                    from hazard in hazards
                    select new XElement("hazard", hazard.Hazard,
                        new XAttribute("signalword", hazard.SignalWord ?? ""),
                        new XAttribute("symbol", hazard.Symbol ?? "")
                    )
                )
            );
        }        
    }
}
