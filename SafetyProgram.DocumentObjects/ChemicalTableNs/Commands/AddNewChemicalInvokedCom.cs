using System;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.ModelObjects;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs.Commands
{
    internal sealed class AddNewChemicalInvokedCom : IInvokedCommand
    {
        private readonly IChemicalTable table;
        private ICoshhChemicalObject addedChemical;

        public AddNewChemicalInvokedCom(IChemicalTable table)
        {
            if (table != null)
            {
                this.table = table;
            }
            else throw new ArgumentNullException();            
        }

        public void Execute()
        {
            addedChemical = ModelObjectsPrototypes.CoshhChemicalObject();
            addedChemical.Chemical.Name = "MyRandomChemical";
            addedChemical.Value = 20.5M;
            addedChemical.Unit = "mgs";
            addedChemical.Chemical.Hazards.Add
                (
                    new HazardModelObject(
                        "Flammable",
                        "H25",
                        "Flammable"
                        )
                );
            addedChemical.Chemical.Hazards.Add
                (
                    new HazardModelObject(
                        "Flammable",
                        "H25",
                        "Flammable"
                        )
                );

            table.Chemicals.Add(addedChemical);
        }

        public void UnExecute()
        {
            table.Chemicals.Remove(addedChemical);
        }
    }
}
