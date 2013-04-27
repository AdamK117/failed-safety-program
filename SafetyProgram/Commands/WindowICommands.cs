using System.Collections.Generic;
using System.Windows.Input;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Commands
{
    public sealed class WindowICommands : IWindowCommands
    {
        private readonly IWindow<IDocument> window;

        /// <summary>
        /// Constructs a new instance of the commands (iCommands, Hotkeys, generic commands) available to a CoshhWindow.
        /// </summary>
        /// <param name="window">Instance of a CoshhWindow parent</param>
        public WindowICommands(IWindow<IDocument> window)
        {
            this.window = window;

            //Instantiate CoshhWindow commands
            Close = new CloseICom(window);
            New = new NewICom(window);
            Open = new OpenICom(window);
            Save = new SaveICom(window);
            SaveAs = new SaveAsICom(window);
            Exit = new ExitICom(window);

            //Get a list of hotkeys for these CoshhWindow commands.
            Hotkeys = setHotKeys();
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

        public ICommand New
        {
            get; 
            private set;
        }

        public ICommand Open
        {
            get;
            private set;
        }

        public ICommand Save
        {
            get;
            private set;
        }

        public ICommand SaveAs
        {
            get;
            private set;
        }

        public ICommand Close
        {
            get;
            private set;
        }

        public ICommand Exit
        {
            get;
            private set;
        }

        public List<InputBinding> Hotkeys
        {
            get;
            private set;
        }
    }
}
