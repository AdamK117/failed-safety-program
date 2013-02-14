using System;
using System.Collections.Generic;

using SafetyProgram.Models.DataModels;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SafetyProgram.Data
{
    public class CoshhData : BaseINPC
    {
        #region Data Held

        private string title = "Untitled Safety Document";
        public string Title
        {
            get { return title; }
            set 
            { 
                title = value;
                RaisePropertyChanged("Title");
            }
        }

        private ObservableCollection<CoshhChemicalModel> chemicals = new ObservableCollection<CoshhChemicalModel>();
        public ObservableCollection<CoshhChemicalModel> Chemicals
        {
            get { return chemicals; }
            set
            { 
                chemicals = value;
                RaisePropertyChanged("Chemicals");
            }
        }

        private ObservableCollection<CoshhApparatusModel> apparatuses = new ObservableCollection<CoshhApparatusModel>();
        public ObservableCollection<CoshhApparatusModel> Apparatuses
        {
            get { return apparatuses; }
            set
            {
                apparatuses = value;
                RaisePropertyChanged("Apparatuses");
            }
        }

        private ObservableCollection<CoshhProcessModel> processes = new ObservableCollection<CoshhProcessModel>();
        public ObservableCollection<CoshhProcessModel> Processes
        {
            get { return processes; }
            set 
            { 
                processes = value;
                RaisePropertyChanged("Processes");
            }
        }

        private string additionalComments = "No additional comments.";
        public string AdditionalComments
        {
            get { return additionalComments; }
            set 
            { 
                additionalComments = value;
                RaisePropertyChanged("AdditionalComments");
            }
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
