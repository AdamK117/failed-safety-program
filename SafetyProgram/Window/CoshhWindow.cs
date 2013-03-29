using SafetyProgram.MainWindow.Document;
using SafetyProgram.MainWindow.IO;
using SafetyProgram.Window.Ribbon;
using System;
using System.Windows.Forms;
using SafetyProgram.Window.Commands;

namespace SafetyProgram.Window
{
    public partial class CoshhWindow
    {
        private CoshhWindowView view;
        private WindowCommands commands;
        private CoshhDocument document;
        private CoshhRibbon ribbon;        
        private ICoshhDataService service;

        /// <summary>
        /// Constructs a ViewModel for the main window. Acts as the holder for the document, ribbon, save commands, etc.
        /// </summary>
        /// <param name="window"></param>
        public CoshhWindow()
        {
            //Create window commands
            commands = new WindowCommands(this);

            //Create a new (blank) document
            document = new CoshhDocument();

            //Create a new ribbon
            ribbon = new CoshhRibbon(this);

            //Default to saving locally
            service = new CoshhLocalFileService();

            //Create a new window, this is its ViewModel.
            view = new CoshhWindowView(this);

            //Give the window some hotkeys
            view.InputBindings.AddRange(commands.Hotkeys);

            //Show the window
            view.Show();
        }

        public WindowCommands Commands
        {
            get { return commands; }
            set { commands = value; }
        }

        /// <summary>
        /// Holds the current document of the MainWindow
        /// </summary>
        public CoshhDocument Document
        {
            get { return document; }
            set { document = value; }
        }

        /// <summary>
        /// Holds the ribbon of the MainWindow
        /// </summary>
        public CoshhRibbon Ribbon
        {
            get { return ribbon; }
        }

        public ICoshhDataService Service
        {
            get { return service; }
            set { service = value; }
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
                DialogResult dialogResult = System.Windows.Forms.MessageBox.Show("Do you want to save changes you made to " + Document.Title + "?", "Save changes.", MessageBoxButtons.YesNoCancel);

                switch (dialogResult)
                {
                    //Save changes and close the document
                    case System.Windows.Forms.DialogResult.Yes:
                        if (!Save()) { goto case System.Windows.Forms.DialogResult.Cancel; }
                        break;

                    //Don't Save changes and close the document
                    case System.Windows.Forms.DialogResult.No:
                        break;

                    //Don't close the document
                    case System.Windows.Forms.DialogResult.Cancel:
                        return false;
                }
            }

            //Create a new (blank) placeholder document
            Document = new CoshhDocument();
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
                Document = new CoshhDocument();
                Document.IsOpen(true);
                Document.Selected(null);
                Document.Edited(true);
                return true;
            }
            else { return false; }

        }
    }
}
