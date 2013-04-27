using SafetyProgram.ModelObjects;
using SafetyProgram.Base;

namespace SafetyProgram.DocumentObjects.ChemicalTable.Commands
{
    public sealed class AddNewChemicalICom : ExtendedICommand<ChemicalTable>
    {
        internal AddNewChemicalICom(ChemicalTable table) : base(table) 
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
                data.Chemicals.Add(chemical);
            }            
        }        
    }
}
