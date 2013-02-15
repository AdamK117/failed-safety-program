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
            Chemicals = new ObservableCollection<CoshhChemicalModel>();
            Apparatuses = new ObservableCollection<CoshhApparatusModel>();
            Processes = new ObservableCollection<CoshhProcessModel>();
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

        private ObservableCollection<CoshhChemicalModel> chemicals;
        public ObservableCollection<CoshhChemicalModel> Chemicals
        {
            get { return chemicals; }
            set
            { 
                chemicals = value;
                RaisePropertyChanged("Chemicals");
            }
        }

        private ObservableCollection<CoshhApparatusModel> apparatuses;
        public ObservableCollection<CoshhApparatusModel> Apparatuses
        {
            get { return apparatuses; }
            set
            {
                apparatuses = value;
                RaisePropertyChanged("Apparatuses");
            }
        }

        private ObservableCollection<CoshhProcessModel> processes;
        public ObservableCollection<CoshhProcessModel> Processes
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
