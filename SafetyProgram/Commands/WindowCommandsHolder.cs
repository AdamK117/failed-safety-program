using System.Collections.Generic;
using System.Windows.Input;

namespace SafetyProgram.Commands
{
    public class WindowCommandsHolder
    {
        private readonly CoshhWindow window;

        private readonly CloseICommand closeCommand;
        private readonly NewICommand newCommand;
        private readonly OpenICommand openCommand;
        private readonly SaveICommand saveCommand;
        private readonly SaveAsICommand saveAsCommand;
        private readonly ExitICommand exitCommand;

        private readonly List<InputBinding> hotKeys;

        /// <summary>
        /// Constructs a new instance of the commands (iCommands, Hotkeys, generic commands) available to a CoshhWindow.
        /// </summary>
        /// <param name="window">Instance of a CoshhWindow parent</param>
        public WindowCommandsHolder(CoshhWindow window)
        {
            this.window = window;

            //Instantiate CoshhWindow commands
            closeCommand = new CloseICommand(window);
            newCommand = new NewICommand(window);
            openCommand = new OpenICommand(window);
            saveCommand = new SaveICommand(window);
            saveAsCommand = new SaveAsICommand(window);
            exitCommand = new ExitICommand(window);

            //Get a list of hotkeys for these CoshhWindow commands.
            hotKeys = setHotKeys();
        }

        private List<InputBinding> setHotKeys()
        {
            return new List<InputBinding>()
                {
                    //Save: CTRL+S
                    new InputBinding(
                        Save,
                        new KeyGesture(Key.S, ModifierKeys.Control)
                    ),
                    //SaveAs: CTRL+SHIFT+S
                    new InputBinding(
                        SaveAs,
                        new KeyGesture(Key.S, ModifierKeys.Control | ModifierKeys.Shift)
                    ),
                    //Open: CTRL+O
                    new InputBinding(
                        Open,
                        new KeyGesture(Key.O, ModifierKeys.Control)
                    ),
                    //Close: CTRL+W
                    new InputBinding(
                        Close,
                        new KeyGesture(Key.W, ModifierKeys.Control)
                    ),
                    //New: CTRL+N
                    new InputBinding(
                        New,
                        new KeyGesture(Key.N, ModifierKeys.Control)
                    )
                };
        }

        /// <summary>
        /// Gets an ICommand that closes the currently open CoshhDocument in the CoshhWindow.
        /// </summary>
        public CloseICommand Close { get { return closeCommand; } }

        /// <summary>
        /// Gets an ICommand that tries to create a new CoshhDocument to put into the CoshhWindow.
        /// </summary>
        public NewICommand New { get { return newCommand; } }

        /// <summary>
        /// Gets an ICommand that tries to open a CoshhDocument to put into the CoshhWindow.
        /// </summary>
        public OpenICommand Open { get { return openCommand; } }

        /// <summary>
        /// Gets an ICommand that attempts to save the CoshhDocument in the CoshhWindow.
        /// </summary>
        public SaveICommand Save { get { return saveCommand; } }

        /// <summary>
        /// Gets an ICommand that attempts to save the CoshhDocument in the CoshhWindow to a specific location.
        /// </summary>
        public SaveAsICommand SaveAs { get { return saveAsCommand; } }

        /// <summary>
        /// Gets an ICommand that exits the CoshhWindow.
        /// </summary>
        public ExitICommand Exit { get { return exitCommand; } }

        /// <summary>
        /// Gets a list of the hotkeys associated with the CoshhWindow.
        /// </summary>
        public List<InputBinding> Hotkeys { get { return hotKeys; } }
    }
}
