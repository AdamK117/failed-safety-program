using SafetyProgram.MainWindow.Document.Controls.ChemicalTable;

namespace SafetyProgram.MainWindow.Document.Commands
{
    public class InsertChemicalTableICommand : DocumentCommandsBase
    {
        public InsertChemicalTableICommand(CoshhDocument document) : base(document) 
        { }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            document.Body.Add(new ChemicalTable());
        }
    }
}
