using System;
using System.Collections.Generic;

using SafetyProgram.Models.DataModels;

namespace SafetyProgram.Data
{
    public class CoshhData : ICoshhData
    {
        #region Data Held

        private string title = "Untitled Safety Document";
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private List<CoshhChemicalModel> chemicals = new List<CoshhChemicalModel>();
        public List<CoshhChemicalModel> Chemicals
        {
            get { return chemicals; }
            set { chemicals = value; }
        }

        private List<CoshhApparatusModel> apparatuses = new List<CoshhApparatusModel>();
        public List<CoshhApparatusModel> Apparatuses
        {
            get { return apparatuses; }
            set { apparatuses = value; }
        }

        private List<CoshhProcessModel> processes = new List<CoshhProcessModel>();
        public List<CoshhProcessModel> Processes
        {
            get { return processes; }
            set { processes = value; }
        }

        private string additionalComments = "No additional comments.";
        public string AdditionalComments
        {
            get { return additionalComments; }
            set { additionalComments = value; }
        }

        #endregion

        #region Methods

        public virtual bool Save() { throw new Exception("Save without type"); }

        public virtual bool Load() { throw new Exception("Loading without type"); }

        public virtual bool SaveAs() { throw new Exception("SaveAs without type"); }

        public void SaveAsPDF() { throw new Exception("Can't save as PDF"); }

        public virtual bool Close() { return true; }

        #endregion
    }
}
