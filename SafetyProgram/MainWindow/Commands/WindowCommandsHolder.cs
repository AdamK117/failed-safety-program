using System.Collections.Generic;
using System.Windows.Input;

namespace SafetyProgram.MainWindow.Commands
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

            //Instantiate window commands
            closeCommand = new CloseICommand(window);
            newCommand = new NewICommand(window);
            openCommand = new OpenICommand(window);
            saveCommand = new SaveICommand(window);
            saveAsCommand = new SaveAsICommand(window);
            exitCommand = new ExitICommand(window);

            //Get the hotkeys (needs instantiated window commands).
            hotKeys = setHotKeys();
        }

        private List<InputBinding> setHotKeys()
        {
            return new List<InputBinding>()
                {
                    new InputBinding(
                        Save,
                        new KeyGesture(Key.S, ModifierKeys.Control)
                    ),
                    new InputBinding(
                        SaveAs,
                        new KeyGesture(Key.S, ModifierKeys.Control | ModifierKeys.Shift)
                    ),
                    new InputBinding(
                        Open,
                        new KeyGesture(Key.O, ModifierKeys.Control)
                    ),
                    new InputBinding(
                        Close,
                        new KeyGesture(Key.W, ModifierKeys.Control)
                    ),
                    new InputBinding(
                        New,
                        new KeyGesture(Key.N, ModifierKeys.Control)
                    )
                };
        }

        public CloseICommand Close { get { return closeCommand; } }
        public NewICommand New { get { return newCommand; } }
        public OpenICommand Open { get { return openCommand; } }
        public SaveICommand Save { get { return saveCommand; } }
        public SaveAsICommand SaveAs { get { return saveAsCommand; } }
        public ExitICommand Exit { get { return exitCommand; } }

        /// <summary>
        /// The hotkeys available to the window
        /// </summary>
        public List<InputBinding> Hotkeys { get { return hotKeys; } }
    }
}
