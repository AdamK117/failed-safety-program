using System.Collections.Generic;
using System.Windows.Input;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Commands
{
    public sealed class WindowICommands<T> : IWindowCommands
    {
        private readonly IWindow<T> window;

        /// <summary>
        /// Constructs a new instance of the commands (iCommands, Hotkeys, generic commands) available to a CoshhWindow.
        /// </summary>
        /// <param name="window">Instance of a CoshhWindow parent</param>
        public WindowICommands(IWindow<T> window, ICommandInvoker commandInvoker)
        {
            this.window = window;

            //Instantiate CoshhWindow commands
            Close = new CloseICom<T>(window);
            New = new NewICom<T>(window);
            Open = new OpenICom<T>(window);
            Save = new SaveICom<T>(window);
            SaveAs = new SaveAsICom<T>(window);
            Exit = new ExitICom(window);
            Undo = new UndoICom(commandInvoker);
            Redo = new RedoICom(commandInvoker);

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
                    ),
                    //Undo: CTRL+Z
                    new InputBinding(
                        Undo,
                        new KeyGesture(Key.Z, ModifierKeys.Control)
                    ),
                    //Redo: CTRL+Y
                    new InputBinding(
                        Redo,
                        new KeyGesture(Key.Y, ModifierKeys.Control)
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

        public ICommand Undo
        {
            get;
            private set;
        }

        public ICommand Redo
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
