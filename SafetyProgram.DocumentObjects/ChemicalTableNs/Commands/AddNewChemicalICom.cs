using System.Windows.Input;
using SafetyProgram.ModelObjects;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs.Commands
{
    internal sealed class AddNewChemicalICom : ICommand
    {
        private readonly ChemicalTable data;

        public AddNewChemicalICom(ChemicalTable table) 
        {
            this.data = table;
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
        /// Adds a random chemical to the ChemicalTable.
        /// </summary>
        /// <param name="parameter">Unused paramater.</param>
        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                CoshhChemicalObject chemical = new CoshhChemicalObject();
                chemical.Chemical.Name = "MyRandomChemical";
                chemical.Value = 20.5M;
                chemical.Unit = "mgs";
                chemical.Chemical.Hazards.Add
                    (
                        new HazardModelObject()
                        {
                            Hazard = "Flammable",
                            SignalWord = "H25",
                            Symbol = "Flammable"
                        }
                    );
                chemical.Chemical.Hazards.Add
                    (
                        new HazardModelObject()
                        {
                            Hazard = "Harmful",
                            SignalWord = "H25",
                            Symbol = "Harmful"
                        }
                    );
                data.Chemicals.Add(chemical);
            }            
        }

        public event System.EventHandler CanExecuteChanged;
    }
}
