using SafetyProgram.Models.DataModels;
using System.Collections.ObjectModel;

namespace SafetyProgram.DocObjects.ChemicalTable.Commands
{
    public class AddNewChemicalICommand : ChemicalTableCommandsBase
    {
        public AddNewChemicalICommand(ChemicalTable table) : base(table) 
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
        /// Adds a random chemical to the ChemicalTable.
        /// </summary>
        /// <param name="parameter">Unused paramater.</param>
        public override void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                table.Chemicals.Add(
                    new CoshhChemicalModel()
                    {
                        Name = "MyRandomChemical",
                        Value = 20.5F,
                        Unit = "mgs",
                        Hazards = new ObservableCollection<HazardModel>()
                            {
                                new HazardModel()
                                    {
                                        Hazard = "Flammable",
                                        SignalWord = "H25",
                                        Symbol = "Flammable"
                                    }
                            }
                    }
                );
            }            
        }        
    }
}
