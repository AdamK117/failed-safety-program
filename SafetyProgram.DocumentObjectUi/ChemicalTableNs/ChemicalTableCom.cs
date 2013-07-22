using System;
using System.Collections.Generic;
using System.Windows;
using SafetyProgram.Models.DataModels;

namespace SafetyProgram.DocObjects.ChemicalTable
{
    public class ChemicalTableCom : IDataObject
    {
        private readonly IEnumerable<CoshhChemicalModel> chemicals;

        public ChemicalTableCom(IEnumerable<CoshhChemicalModel> chemicals)
        {
            this.chemicals = chemicals;
        }

        public object GetData(string format, bool autoConvert)
        {
            return GetData(format);
        }

        public object GetData(Type format)
        {
            return GetData(format.ToString());
        }

        public object GetData(string format)
        {
            #region Model object format
            if (format == "CoshhChemicalModels")
            {
                return chemicals;
            }
            #endregion

            #region Text
            else if (format == DataFormats.Text)
            {
                string data = "";

                foreach (CoshhChemicalModel chemical in chemicals)
                {
                    data += chemical.Name + ", ";
                    data += chemical.Value.ToString() + " ";
                    data += chemical.Unit + ", ";
                    data += "Hazards: ";
                    foreach (HazardModel hazard in chemical.Hazards)
                    {
                        data += hazard.Hazard + ", ";
                    }
                    //Trim trailing ", "
                    data = data.Substring(0, data.Length - 2);
                    data += Environment.NewLine;
                }

                return data;
            }
            #endregion

            #region RTF
            else if (format == DataFormats.Rtf)
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
                foreach (CoshhChemicalModel chemical in chemicals)
                {
                    //Add a table row
                    rtfTable += @"\trowd\trautofit1
\intbl
\cellx1
\cellx2
\cellx3";
                    //Populate its data
                    rtfTable += "{";
                    rtfTable += chemical.Name + @"\cell ";
                    rtfTable += chemical.Value.ToString() + " " + chemical.Unit +  @"\cell ";
                    foreach(HazardModel hazard in chemical.Hazards)
                    {
                        rtfTable += hazard.Hazard + ", ";
                    }
                    rtfTable += @"\cell ";
                    rtfTable += "}";
                    
                    //Finish the table row
                    rtfTable += @"\row";
                }

                //Finish the RTF definition
                rtfTable += "}";

                return rtfTable;
            }
            #endregion

            throw new ArgumentException("GetData(string format) should not be called directly. Was called with an unknown format.");
        }

        public bool GetDataPresent(string format, bool autoConvert)
        {
            //Check for raw, unconverted, format
            if (format == "CoshhChemicalModels")
            {
                return true;
            }
            //Check for autoconverted formats
            else if (autoConvert == true && 
                (
                    (format == DataFormats.Text) ||
                    (format == DataFormats.Rtf)
                )
            )
            {
                return true;
            }
            //Format not found
            else
            {
                return false;
            }
        }

        public bool GetDataPresent(Type format)
        {
            return GetDataPresent(format.ToString(), true);
        }

        public bool GetDataPresent(string format)
        {
            return GetDataPresent(format, true);
        }

        public string[] GetFormats(bool autoConvert)
        {
            //Define "raw" (not converted) formats.
            string[] rawFormats = new string[]
            {
                "CoshhChemicalModels"
            };

            //Define converted formats.
            string[] convertedFormats = new string[]
            {
                DataFormats.Text,
                DataFormats.Rtf,
                DataFormats.FileDrop
            };

            //If autoconversion is allowed, can use both rawFormats and convertedFormats in Copy/Paste op.
            if (autoConvert == true)
            {
                //Create a new array to hold both rawFormats (that don't need autoconverting) and convertedFormats (that do).
                string[] allFormats = new string[rawFormats.Length + convertedFormats.Length];

                //Copy in rawFormats and convertedFormats
                rawFormats.CopyTo(allFormats, 0);
                convertedFormats.CopyTo(allFormats, rawFormats.Length);

                return allFormats;
            }
            else
            {
                return rawFormats;
            }
        }

        public string[] GetFormats()
        {
            return GetFormats(true);
        }

        //Set methods not implemented. The constructor effectively "sets" the data.

        public void SetData(string format, object data, bool autoConvert)
        {
            throw new NotImplementedException();
        }

        public void SetData(Type format, object data)
        {
            throw new NotImplementedException();
        }

        public void SetData(string format, object data)
        {
            throw new NotImplementedException();
        }

        public void SetData(object data)
        {
            throw new NotImplementedException();
        }
    }
}
