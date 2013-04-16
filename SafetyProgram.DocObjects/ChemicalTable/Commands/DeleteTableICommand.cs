using System.Windows.Forms;

namespace SafetyProgram.DocObjects.ChemicalTable.Commands
{
    public class DeleteTableICommand : ChemicalTableCommandsBase
    {
        public DeleteTableICommand(ChemicalTable table)
            : base(table)
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
        /// Flags the ChemicalTable for deletion.
        /// </summary>
        /// <param name="parameter">Unused paramater.</param>
        public override void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                switch (MessageBox.Show("Are you sure you want to delete this table?", "Confirm Deletion.", MessageBoxButtons.YesNo))
                {
                    case DialogResult.Yes:
                        //Flag the ChemicalTable for removal
                        table.FlagForRemoval();
                        break;

                    default:
                        //Don't do anything
                        break;
                }
            }                      
        }
    }
}
