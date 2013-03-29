using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace SafetyProgram.Window.Commands
{
    public class WindowCommands
    {
        private Close closeCommand;
        private New newCommand;
        private Open openCommand;
        private Save saveCommand;
        private SaveAs saveAsCommand;
        private List<InputBinding> hotkeys;

        public WindowCommands(CoshhWindow window)
        {
            closeCommand = new Close(window);
            newCommand = new New(window);
            openCommand = new Open(window);
            saveCommand = new Save(window);
            saveAsCommand = new SaveAs(window);

            hotkeys = getHotkeys();
        }

        private List<InputBinding> getHotkeys()
        {
            return new List<InputBinding>()
            {
                new InputBinding(
                    SaveCommand,
                    new KeyGesture(Key.S, ModifierKeys.Control)
                ),
                new InputBinding(
                    SaveAsCommand,
                    new KeyGesture(Key.S, ModifierKeys.Control | ModifierKeys.Shift)
                ),
                new InputBinding(
                    OpenCommand,
                    new KeyGesture(Key.O, ModifierKeys.Control)
                ),
                new InputBinding(
                    CloseCommand,
                    new KeyGesture(Key.W, ModifierKeys.Control)
                ),
                new InputBinding(
                    NewCommand,
                    new KeyGesture(Key.N, ModifierKeys.Control)
                )
            };
        }

        public List<InputBinding> Hotkeys { get { return hotkeys; } set { hotkeys = value; } }
        public Close CloseCommand { get { return closeCommand; } set { closeCommand = value; } }
        public New NewCommand { get { return newCommand; } set { newCommand = value; } }
        public Open OpenCommand { get { return openCommand; } set { openCommand = value; } }
        public Save SaveCommand { get { return saveCommand; } set { saveCommand = value; } }
        public SaveAs SaveAsCommand { get { return saveAsCommand; } set { saveAsCommand = value; } }

        
    }
}
