using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;

using SafetyProgram.Data.DOM;
using SafetyProgram.Models.DataModels;

namespace SafetyProgram.Data.IO
{
    class CoshhLocalFileService : ICoshhDataService
    {
        private string path;

        /// <summary>
        /// Saves the CoshhDocument to the location it was loaded from.
        /// </summary>
        /// <param name="document">The CoshhDocument to be saved.</param>
        /// <returns>If the file sucessfully saved.</returns>
        public bool Save(CoshhDocument document)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Loads a User specified file into the CoshhDocument supplied.
        /// </summary>
        /// <param name="document">A CoshhDocument to fill with loaded data.</param>
        /// <returns>If the file has sucessfully loaded.</returns>
        public bool Load(CoshhDocument document)
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
            else if (!loadFile(Path(), document)) { return false; }

            return true;
        }

        private bool loadFile(string path, CoshhDocument doc)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves the CoshhDocument to the user specified location.
        /// </summary>
        /// <param name="document">The data to be saved.</param>
        /// <returns>If the file was sucessfully saved.</returns>
        public bool SaveAs(CoshhDocument document)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Coshh Safety Document|*.xml";
            saveFileDialog.Title = "Save As";

            DialogResult userResponse = saveFileDialog.ShowDialog();

            if (userResponse == DialogResult.OK)
            {
                path = saveFileDialog.FileName;
                Save(document);
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
