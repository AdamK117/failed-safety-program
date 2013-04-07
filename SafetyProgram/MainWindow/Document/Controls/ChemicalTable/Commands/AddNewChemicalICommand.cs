using SafetyProgram.Models.DataModels;

namespace SafetyProgram.MainWindow.Document.Controls.ChemicalTable.Commands
{
    public class AddNewChemicalICommand : ChemicalTableCommandsBase
    {
        public AddNewChemicalICommand(ChemicalTable table) : base(table) 
        { }

        public override void Execute(object parameter)
        {
            table.Chemicals.Add(
                new CoshhChemicalModel()
                {
                    Name = "MyRandomChemical",
                    Value = 20.5F,
                    Unit = "mgs"
                }
            );
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
