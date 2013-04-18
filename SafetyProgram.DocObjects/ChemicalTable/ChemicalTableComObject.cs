using System;
using System.Collections.Generic;
using SafetyProgram.Models.DataModels;

namespace SafetyProgram.DocObjects.ChemicalTable
{
    public class ChemicalTableComHelper : DocObjectComHelper<IEnumerable<CoshhChemicalModel>>
    {
        public ChemicalTableComHelper(string comIdentifier)
            : base(comIdentifier)
        { }

        protected override string getTextFormat(IEnumerable<CoshhChemicalModel> data)
        {
            string output = "";

            foreach (CoshhChemicalModel chemical in data)
            {
                output += chemical.Name + ", ";
                output += chemical.Value.ToString() + " ";
                output += chemical.Unit + ", ";
                output += "Hazards: ";
                foreach (HazardModel hazard in chemical.Hazards)
                {
                    output += hazard.Hazard + ", ";
                }
                //Trim trailing ", "
                output = output.Substring(0, output.Length - 2);
                output += Environment.NewLine;
            }

            return output;
        }

        protected override string getRtfFormat(IEnumerable<CoshhChemicalModel> data)
        {
            string rtfTable = "";

            //Add RTF header
            rtfTable += @"{\rtf1\ansi\deff0";

            //Add a Table with a header row
            rtfTable += @"
\trowd\trautofit1
\intbl
\cellx1
\cellx2
\cellx3
{Chemicals\cell Amounts\cell Hazards\cell }
\row";
            foreach (CoshhChemicalModel chemical in data)
            {
                //Add a table row
                rtfTable += @"\trowd\trautofit1
\intbl
\cellx1
\cellx2
\cellx3";
                //Populate the row with chemical data
                rtfTable += "{";
                rtfTable += chemical.Name + @"\cell ";
                rtfTable += chemical.Value.ToString() + " " + chemical.Unit + @"\cell ";
                foreach (HazardModel hazard in chemical.Hazards)
                {
                    rtfTable += hazard.Hazard + ", ";
                }
                rtfTable += rtfTable.Substring(0, rtfTable.Length - 2);
                rtfTable += @"\cell ";
                rtfTable += "}";

                //Finish the table row
                rtfTable += @"\row";
            }

            //Finish the RTF definition
            rtfTable += "}";

            return rtfTable;
        }

        protected override IEnumerable<CoshhChemicalModel> cloneDataInput(IEnumerable<CoshhChemicalModel> data)
        {
            List<CoshhChemicalModel> reBoxed = new List<CoshhChemicalModel>();
            foreach (CoshhChemicalModel chemical in data)
            {
                CoshhChemicalModel newModel = new CoshhChemicalModel();

                //Clone the CoshhChemicalModel's values.
                newModel.Name = chemical.Name;
                newModel.Unit = chemical.Unit;
                newModel.Value = chemical.Value;
                newModel.Hazards = chemical.Hazards;

                reBoxed.Add(newModel);
            }
            return reBoxed;
        }
    }
}
