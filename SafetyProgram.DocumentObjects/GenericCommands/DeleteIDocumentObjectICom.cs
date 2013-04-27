using System.Windows.Forms;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.DocumentObjects.GenericCommands
{
    public sealed class DeleteIDocumentObjectICom : ExtendedICommand<IDocumentObject>
    {
        internal DeleteIDocumentObjectICom(IDocumentObject docObject)
            : base(docObject)
        { }

        /// <summary>
        /// Can always execute
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <returns></returns>
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Flags the IDocObject for deletion.
        /// </summary>
        /// <param name="parameter">Unused paramater.</param>
        public override void Execute(object parameter)
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
    }
}
