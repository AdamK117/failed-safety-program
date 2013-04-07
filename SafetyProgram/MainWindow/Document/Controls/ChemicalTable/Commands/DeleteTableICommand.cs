namespace SafetyProgram.MainWindow.Document.Controls.ChemicalTable.Commands
{
    public class DeleteTableICommand : ChemicalTableCommandsBase
    {
        public DeleteTableICommand(ChemicalTable table)
            : base(table)
        { }

        public override bool CanExecute(object parameter)
        {
            return table.CanRemove();
        }

        public override void Execute(object parameter)
        {
            table.RemoveFlag = true;
        }
    }
}
