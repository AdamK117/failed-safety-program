using System.Collections.Generic;
using System.Windows.Input;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.MainWindow.Commands
{
    public sealed class WindowICommands<TContent> : IWindowCommands
    {
        /// <summary>
        /// Constructs a new instance of the commands (iCommands, Hotkeys, generic commands) available to a CoshhWindow.
        /// </summary>
        /// <param name="window">Instance of a CoshhWindow parent</param>
        public WindowICommands(ICommandInvoker commandInvoker,
            IEditableHolder<TContent> contentHolder,
            IHolder<IIOService<TContent>> serviceHolder)
        {
            Helpers.NullCheck(commandInvoker, contentHolder, serviceHolder);

            Close = new CloseICom<TContent>(contentHolder, serviceHolder);
            New = new NewICom<TContent>(contentHolder, serviceHolder);
            Open = new OpenICom<TContent>(contentHolder, serviceHolder);
            Save = new SaveICom<TContent>(contentHolder, serviceHolder);
            SaveAs = new SaveAsICom<TContent>(contentHolder, serviceHolder);
            Undo = new UndoICom(commandInvoker);
            Redo = new RedoICom(commandInvoker);

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
