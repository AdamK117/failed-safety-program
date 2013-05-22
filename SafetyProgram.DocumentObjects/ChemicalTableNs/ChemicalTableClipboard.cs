﻿using System;
using System.Collections.Generic;
using System.Windows;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.ModelObjects;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs
{
    internal static class ChemicalTableClipboard
    {
        public static IDataObject GetDataObject(this IEnumerable<ICoshhChemicalObject> chemicals)
        {
            IDataObject dataObject = chemicals.GetDataObject<ICoshhChemicalObject>(ChemicalTable.COM_IDENTITY);

            dataObject.SetData(DataFormats.Text, getTextFormat(chemicals));
            dataObject.SetData(DataFormats.Rtf, getRtfFormat(chemicals));

            return dataObject;
        }

        public static void TryCopy(this IEnumerable<ICoshhChemicalObject> chemicals)
        {
            Clipboard.SetDataObject(chemicals.GetDataObject());
        }

        public static void TryPaste(this ICollection<ICoshhChemicalObject> target)
        {
            target.TryPasteInto<ICoshhChemicalObject>(ChemicalTable.COM_IDENTITY);
        }

        private static string getTextFormat(IEnumerable<ICoshhChemicalObject> data)
        {
            string output = "";

            foreach (ICoshhChemicalObject chemical in data)
            {
                output += chemical.Chemical.Name + ", ";
                output += chemical.Value.ToString() + " ";
                output += chemical.Unit + ", ";
                output += "Hazards: ";
                foreach (IHazardModelObject hazard in chemical.Chemical.Hazards)
                {
                    output += hazard.Hazard + ", ";
                }
                //Trim trailing ", "
                output = output.Substring(0, output.Length - 2);
                output += Environment.NewLine;
            }

            return output;
        }

        private static string getRtfFormat(IEnumerable<ICoshhChemicalObject> data)
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
            foreach (ICoshhChemicalObject chemical in data)
            {
                //Add a table row
                rtfTable += @"\trowd\trautofit1
\intbl
\cellx1
\cellx2
\cellx3";
                //Populate the row with chemical data
                rtfTable += "{";
                rtfTable += chemical.Chemical.Name + @"\cell ";
                rtfTable += chemical.Value.ToString() + " " + chemical.Unit + @"\cell ";
                foreach (IHazardModelObject hazard in chemical.Chemical.Hazards)
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
    }
}
