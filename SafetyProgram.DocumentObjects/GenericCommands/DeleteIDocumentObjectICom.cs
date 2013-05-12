using System.Windows.Forms;
using System.Windows.Input;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.DocumentObjects.GenericCommands
{
    internal sealed class DeleteIDocumentObjectICom : ICommand
    {
        private readonly IDocumentObject data;

        public DeleteIDocumentObjectICom(IDocumentObject docObject)
        {
            this.data = docObject;
        }

        /// <summary>
        /// Can always execute
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Flags the IDocObject for deletion.
        /// </summary>
        /// <param name="parameter">Unused paramater.</param>
        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                switch (MessageBox.Show("Are you sure you want to delete the selection?", "Confirm Deletion.", MessageBoxButtons.YesNo))
                {
                    case DialogResult.Yes:
                        //Flag the IDocObject for removal
                        data.FlagForRemoval();
                        break;

                    default:
                        //Don't do anything
                        break;
                }
            }                      
        }

        public event System.EventHandler CanExecuteChanged;
    }
}
