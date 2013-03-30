using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Forms;
using SafetyProgram.MainWindow.Document;

namespace SafetyProgram.Window.Commands
{
    public class WindowCommands
    {
        CoshhWindow window;
        WindowICommands iCommands;
        WindowHotkeys hotKeys;

        /// <summary>
        /// Constructs a new instance of the commands (iCommands, Hotkeys, generic commands) available to a CoshhWindow.
        /// </summary>
        /// <param name="window">Instance of a CoshhWindow parent</param>
        public WindowCommands(CoshhWindow window)
        {
            this.window = window;
            iCommands = new WindowICommands(window);
            hotKeys = new WindowHotkeys(iCommands);
        }

        /// <summary>
        /// Class used for holding window ICommands.
        /// </summary>
        public class WindowICommands
        {
            private CloseICommand closeCommand;
            private NewICommand newCommand;
            private OpenICommand openCommand;
            private SaveICommand saveCommand;
            private SaveAsICommand saveAsCommand;

            /// <summary>
            /// Constructs an instance of WindowICommands (usually used in binding to the UI).
            /// </summary>
            /// <param name="parent"></param>
            public WindowICommands(CoshhWindow window)
            {
                closeCommand = new CloseICommand(window);
                newCommand = new NewICommand(window);
                openCommand = new OpenICommand(window);
                saveCommand = new SaveICommand(window);
                saveAsCommand = new SaveAsICommand(window);
            }

            public CloseICommand Close { get { return closeCommand; } set { closeCommand = value; } }
            public NewICommand New { get { return newCommand; } set { newCommand = value; } }
            public OpenICommand Open { get { return openCommand; } set { openCommand = value; } }
            public SaveICommand Save { get { return saveCommand; } set { saveCommand = value; } }
            public SaveAsICommand SaveAs { get { return saveAsCommand; } set { saveAsCommand = value; } }
        }

        /// <summary>
        /// Class used for holding window Hotkeys.
        /// </summary>
        public class WindowHotkeys
        {
            List<InputBinding> allHks;

            public WindowHotkeys(WindowICommands iCommands)
            {
                allHks = getHotkeys(iCommands);
            }

            public List<InputBinding> All { get { return allHks; } set { allHks = value; } }

            private List<InputBinding> getHotkeys(WindowICommands iCommands)
            {
                return new List<InputBinding>()
                {
                    new InputBinding(
                        iCommands.Save,
                        new KeyGesture(Key.S, ModifierKeys.Control)
                    ),
                    new InputBinding(
                        iCommands.SaveAs,
                        new KeyGesture(Key.S, ModifierKeys.Control | ModifierKeys.Shift)
                    ),
                    new InputBinding(
                        iCommands.Open,
                        new KeyGesture(Key.O, ModifierKeys.Control)
                    ),
                    new InputBinding(
                        iCommands.Close,
                        new KeyGesture(Key.W, ModifierKeys.Control)
                    ),
                    new InputBinding(
                        iCommands.New,
                        new KeyGesture(Key.N, ModifierKeys.Control)
                    )
                };
            }
        }

        /// <summary>
        /// The ICommands available to the window
        /// </summary>
        public WindowICommands ICommands { get { return iCommands; } set { iCommands = value; } }

        /// <summary>
        /// The hotkeys available to the window
        /// </summary>
        public WindowHotkeys Hotkeys { get { return hotKeys; } set { hotKeys = value; } }

        /// <summary>
        /// Saves the document.
        /// </summary>
        /// <returns>If the document was sucessfully saved.</returns>
        public bool Save()
        {

            if (window.Service.Save(window.Document))
            {
                window.Document.Edited = false;
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
            if (window.Service.SaveAs(window.Document))
            {
                window.Document.Edited = false;
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
                if (window.Service.Load(window.Document))
                {
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
            bool closed = true;
            if (window.Document == null) { return true; }

            // If the file has been edited, prompt the user to save changes. Else, close it.
            if (window.Document.Edited == true)
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to save changes you made to " + window.Document.Title + "?", "Save changes.", MessageBoxButtons.YesNoCancel);

                switch (dialogResult)
                {
                    //Save changes and close the document
                    case System.Windows.Forms.DialogResult.Yes:
                        //If the user sucessfully saves it, close the document; otherwise, keep the document open
                        closed = Save() ? true : false;
                        break;

                    //Don't Save changes and close the document
                    case DialogResult.No:
                        closed = true;
                        break;

                    //Don't close the document
                    case DialogResult.Cancel:
                        closed = false;
                        break;
                }
            }
            else { closed = true; }

            //If it was closed
            if (closed == true)
            {
                window.Document = null;
                window.Service.Close();
                return closed;
            }
            else { return closed; }
        }

        /// <summary>
        /// Creates a new document.
        /// </summary>
        /// <returns>If a new document was sucessfully created.</returns>
        public bool New()
        {
            //If there isn't a document open at the moment or the current one has been closed.
            if (window.Document == null || Close())
            {
                window.Document = new CoshhDocument();
                return true;
            }
            else { return false; }
        }
    }
}
