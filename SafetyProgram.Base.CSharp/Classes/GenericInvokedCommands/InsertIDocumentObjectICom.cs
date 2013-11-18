using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace SafetyProgram.Base.CSharp.GenericCommands
{
    public sealed class InsertObjectICom<T> : ICommand
    {
        private readonly ICollection<T> target;
        private readonly ICommandInvoker commandInvoker;
        private readonly Func<T> itemFactory;

        public InsertObjectICom(ICollection<T> target, 
            ICommandInvoker commandInvoker,
            Func<T> itemFactory)
        {
            Helpers.NullCheck(target, commandInvoker, itemFactory);

            this.target = target;
            this.commandInvoker = commandInvoker;
            this.itemFactory = itemFactory;
        }

        /// <summary>
        /// Can always execute.
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Inserts a ChemicalTable into the CoshhDocument
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <exception cref="NotSupportedException">Thrown if Execute is called but CanExecute == false</exception>
        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                var invokedCommand = new AddItemInvokedICom<T>(
                    target,
                    itemFactory()
                );

                commandInvoker.InvokeCommand(invokedCommand);
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
