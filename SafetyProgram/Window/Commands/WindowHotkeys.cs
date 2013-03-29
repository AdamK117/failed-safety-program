using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace SafetyProgram.Window.Commands
{
    public class WindowHotkeys
    {
        private WindowCommands commands;
        public List<InputBinding> Hotkeys;

        public WindowHotkeys(WindowCommands commands)
        {
            //Get the current commands (from the window)
            this.commands = commands;

            Hotkeys.Add(new InputBinding(
                commands.SaveCommand,
                new KeyGesture(Key.S, ModifierKeys.Control)
            ));

            Hotkeys.Add(new InputBinding(
                commands.SaveAsCommand,
                new KeyGesture(Key.S, ModifierKeys.Control | ModifierKeys.Shift)
            ));

            Hotkeys.Add(new InputBinding(
                commands.OpenCommand,
                new KeyGesture(Key.O, ModifierKeys.Control)
            ));

            Hotkeys.Add(new InputBinding(
                commands.CloseCommand,
                new KeyGesture(Key.W, ModifierKeys.Control)
            ));

            Hotkeys.Add(new InputBinding(
                commands.NewCommand,
                new KeyGesture(Key.N, ModifierKeys.Control)
            ));
        }
    }
}
