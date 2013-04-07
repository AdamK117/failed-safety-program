namespace SafetyProgram.MainWindow.Document.Controls.ChemicalTable.Commands
{
    public abstract class ChemicalTableCommandsBase : BaseICommand
    {        
        protected readonly ChemicalTable table;

        public ChemicalTableCommandsBase(ChemicalTable table)
        {
            this.table = table;
        }
    }
}
