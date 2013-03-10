using System;

using SafetyProgram.FSharp;
using System.Windows.Forms;
using System.IO;
using SafetyProgram.Data.DOM;
using SafetyProgram.Models.DataModels;
using System.Collections.Generic;

namespace SafetyProgram.Data.IO
{
    class CoshhLocalFile : ICoshhDataService
    {
        private string path;

        /// <summary>
        /// Saves the CoshhDocument to the location it was loaded from.
        /// </summary>
        /// <param name="data">The CoshhDocument to be saved.</param>
        /// <returns>If the file sucessfully saved.</returns>
        public bool Save(CoshhDocument data)
        {
            if (String.IsNullOrWhiteSpace(path)) { return false; }

            CoshhXmlWriter.XmlWrite writer = new CoshhXmlWriter.XmlWrite();
            //writer.writeDocument(path, data);

            setPath(path);

            return true;
        }

        /// <summary>
        /// Loads a User specified file into the CoshhDocument supplied.
        /// </summary>
        /// <param name="data">A CoshhDocument to fill with loaded data.</param>
        /// <returns>If the file has sucessfully loaded.</returns>
        public bool Load(CoshhDocument data)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Coshh Documents (.xml)|*.xml";
            openFileDialog1.Multiselect = false;

            DialogResult dialogResult = openFileDialog1.ShowDialog();

            //If the user didn't press OK to leave the dialog.
            if (dialogResult != DialogResult.OK) { return false; }
            //If the selected file is invalid (doesn't exist, inaccessable, etc.)
            else if (!setPath(openFileDialog1.FileName)) { return false; }
            //If the selected file can be loaded
            else if (!loadFile(Path(), data)) { return false; }

            return true;
        }

        private bool loadFile(string path, CoshhDocument doc)
        {
            CoshhXmlReader.XmlParser parser = new CoshhXmlReader.XmlParser();
            IEnumerable<CoshhChemicalModel> models = parser.GetCoshhChemicalModels(path);

            return true;
        }

        /// <summary>
        /// Saves the CoshhDocument to the user specified location.
        /// </summary>
        /// <param name="data">The data to be saved.</param>
        /// <returns>If the file was sucessfully saved.</returns>
        public bool SaveAs(CoshhDocument data)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Coshh Safety Document|*.xml";
            saveFileDialog.Title = "Save As";

            DialogResult userResponse = saveFileDialog.ShowDialog();

            if (userResponse == DialogResult.OK)
            {
                path = saveFileDialog.FileName;
                Save(data);
                return true;
            }
            else { return false; }
        }

        /// <summary>
        /// Closes the current file.
        /// </summary>
        /// <returns>If the file sucessfully closed.</returns>
        public bool Close()
        {
            //Disconnect etc here
            return true;
        }

        /// <summary>
        /// Returns the path of the file.
        /// </summary>
        /// <returns>Local filesystem path.</returns>
        public string Path()
        {
            return path;
        }

        private bool setPath(string path)
        {
            if (File.Exists(path))
            {
                this.path = path;
                return true;
            }
            else { return false; }            
        }
    }
}
