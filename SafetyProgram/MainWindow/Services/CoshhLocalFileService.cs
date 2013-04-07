using System;
using System.Windows.Forms;
using System.IO;

using SafetyProgram.MainWindow.Document;

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
            throw new NotImplementedException();
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

            if (Close())
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "Coshh Documents (.xml)|*.xml";
                openFileDialog1.Multiselect = false;

                DialogResult dialogResult = openFileDialog1.ShowDialog();

                switch (dialogResult)
                {
                    case DialogResult.OK:
                        if (setPath(openFileDialog1.FileName) && loadFile(Path())) loaded = true;
                        break;

                    default:
                        loaded = false;
                        break;
                }
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the path of the file.
        /// </summary>
        /// <returns>Local filesystem path.</returns>
        public string Path()
        {
            return path;
        }
    }
}
