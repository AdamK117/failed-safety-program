using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using SafetyProgram.Data.IO;
using SafetyProgram.UserControls;

namespace SafetyProgram.Data.DOM
{
    public class CoshhWindow : BaseINPC
    {
        protected ICoshhDataService service;
        protected CoshhDocument document;

        /// <summary>
        /// Makes an instance of the CoshhWindow object.
        /// </summary>
        public CoshhWindow() 
        {
            service = new CoshhLocalFileService();
        }

        /// <summary>
        /// Returns/Sets the document object.
        /// </summary>
        public CoshhDocument Document
        {
            get { return document; }
            set
            {
                document = value;
                RaisePropertyChanged("Document");
            }
        }

        /// <summary>
        /// Saves the document.
        /// </summary>
        /// <returns>If the document was sucessfully saved.</returns>
        public bool Save()
        {
            if (service.Save(Document))
            {
                Document.Edited(false);
                return true;
            }
            else { return false; }
        }

        /// <summary>
        /// Saves the document where specified.
        /// </summary>
        /// <returns>If the operation sucessfully saved the document.</returns>
        public bool SaveAs()
        {
            if (service.SaveAs(Document))
            {
                Document.Edited(false);
                return true;
            }
            else { return false; }
        }

        /// <summary>
        /// Saves the document as a PDF.
        /// </summary>
        public void SaveAsPDF()
        {
            throw new Exception("Placeholder Command, PDF's not yet implemented");
        }

        /// <summary>
        /// Loads a new document.
        /// </summary>
        /// <returns>If the document successfully loaded.</returns>
        public bool Load()
        {
            if (Close())
            {
                if (service.Load(Document))
                {
                    Document.IsOpen(true);
                    Document.Edited(false);
                    Document.Selected(null);
                    return true;
                }
                else { return false; }
            }
            else { return false; }
        }

        /// <summary>
        /// Closes the current document.
        /// </summary>
        /// <returns>If the document sucessfully closed.</returns>
        public bool Close()
        {
            if (Document.Edited())
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to save changes you made to " + Document.Title + "?", "Save changes.", MessageBoxButtons.YesNoCancel);

                switch (dialogResult)
                {
                    //Save changes and close the document
                    case DialogResult.Yes:
                        if (!Save()) { goto case DialogResult.Cancel; }
                        break;

                    //Don't Save changes and close the document
                    case DialogResult.No:
                        break;
                    
                    //Don't close the document
                    case DialogResult.Cancel:
                        return false;
                }
            }
            
            //Create a new (blank) placeholder document
            Document = new CoshhDocument(this);
            Document.IsOpen(false);
            Document.Selected(null);
            Document.Edited(false);

            //Close the file on the fileservice
            service.Close();

            return true;
        }

        /// <summary>
        /// Creates a new document.
        /// </summary>
        /// <returns>If a new document was sucessfully created.</returns>
        public bool New()
        {
            //If the current document is closed
            if (Close())
            {
                Document = new CoshhDocument(this);
                Document.IsOpen(true);
                Document.Selected(null);
                Document.Edited(true);
                return true;
            }
            else { return false; }
        }
    }
}
