using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.Practices.ServiceLocation;

namespace SafetyProgram.ICommands
{
    public class HotkeysHolder
    {
        private ICommandsHolder iCommandsHolder;
        public List<InputBinding> Bindings;

        public HotkeysHolder()
        {
            iCommandsHolder = ServiceLocator.Current.GetInstance<ICommandsHolder>();

            Bindings = new List<InputBinding>();
            
            Bindings.Add(new InputBinding(
                iCommandsHolder.SaveCommand,
                new KeyGesture(Key.S, ModifierKeys.Control)
            ));

            Bindings.Add(new InputBinding(
                iCommandsHolder.SaveAsCommand,
                new KeyGesture(Key.S, ModifierKeys.Control | ModifierKeys.Shift)
            ));

            Bindings.Add(new InputBinding(
                iCommandsHolder.LoadFileCommand,
                new KeyGesture(Key.O, ModifierKeys.Control)
            ));

            Bindings.Add(new InputBinding(
                iCommandsHolder.CloseCommand,
                new KeyGesture(Key.W, ModifierKeys.Control)
            ));

            Bindings.Add(new InputBinding(
                iCommandsHolder.NewFileCommand,
                new KeyGesture(Key.N, ModifierKeys.Control)
            ));

            Bindings.Add(new InputBinding(
                iCommandsHolder.DeleteSelectedCommand,
                new KeyGesture(Key.Delete)
            ));

            //Copy
            //Paste
            //Undo
            //Redo
            //Select All
            //Print Preview (ctrl+F2)
        }
    }
}
