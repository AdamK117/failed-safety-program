using SafetyProgram.BaseClasses;
namespace SafetyProgram.DocObjects.ChemicalTable.Commands
{
    public abstract class ChemicalTableCommandsBase : BaseICommand
    {        
        protected readonly ChemicalTable table;

        /// <summary>
        /// Constructs a base ICommand that uses the ChemicalTable.
        /// </summary>
        /// <param name="table">ChemicalTable using the ICommand</param>
        public ChemicalTableCommandsBase(ChemicalTable table)
        {
            this.table = table;
        }
    }
}
