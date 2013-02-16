using System;
using System.Collections.Generic;

using SafetyProgram.Models.DataModels;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SafetyProgram.Data
{
    public class CoshhData : BaseINPC, ICoshhData
    {
        public CoshhData()
        {
            Chemicals = new ObservableCollection<CoshhObject<CoshhChemicalModel>>();
            Apparatuses = new ObservableCollection<CoshhObject<CoshhApparatusModel>>();
            Processes = new ObservableCollection<CoshhObject<CoshhProcessModel>>();
            Clear();
        }

        public bool Clear()
        {
            Title = "Untitled Safety Document";
            Chemicals.Clear();
            Apparatuses.Clear();
            Processes.Clear();
            AdditionalComments = "No additional comments.";

            return true;
        }

        #region Data Structure

        private string title;
        public string Title
        {
            get { return title; }
            set 
            { 
                title = value;
                RaisePropertyChanged("Title");
            }
        }

        private ObservableCollection<CoshhObject<CoshhChemicalModel>> chemicals;
        public ObservableCollection<CoshhObject<CoshhChemicalModel>> Chemicals
        {
            get { return chemicals; }
            set
            { 
                chemicals = value;
                RaisePropertyChanged("Chemicals");
            }
        }

        private ObservableCollection<CoshhObject<CoshhApparatusModel>> apparatuses;
        public ObservableCollection<CoshhObject<CoshhApparatusModel>> Apparatuses
        {
            get { return apparatuses; }
            set
            {
                apparatuses = value;
                RaisePropertyChanged("Apparatuses");
            }
        }

        private ObservableCollection<CoshhObject<CoshhProcessModel>> processes;
        public ObservableCollection<CoshhObject<CoshhProcessModel>> Processes
        {
            get { return processes; }
            set 
            { 
                processes = value;
                RaisePropertyChanged("Processes");
            }
        }

        private string additionalComments;
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
    }
}
