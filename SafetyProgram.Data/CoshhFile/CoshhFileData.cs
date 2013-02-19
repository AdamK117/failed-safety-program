using SafetyProgram.Models.DataModels;
using System.Collections.ObjectModel;
using SafetyProgram.UserControls;
using SafetyProgram.UserControls.MainWindowControls.ChemicalTable;

namespace SafetyProgram.Data.CoshhFile
{
    public class CoshhFileData : BaseINPC, ICoshhData
    {
        public CoshhFileData()
        {
            Chemicals = new ObservableCollection<CoshhDocDataObject<CoshhChemicalModel>>();
            Apparatuses = new ObservableCollection<CoshhDocDataObject<CoshhApparatusModel>>();
            Processes = new ObservableCollection<CoshhDocDataObject<CoshhProcessModel>>();
            DocObject = new ObservableCollection<IDocUserControl>();
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
        private ObservableCollection<IDocUserControl> docObject;
        public ObservableCollection<IDocUserControl> DocObject
        {
            get { return docObject; }
            set
            {
                docObject = value;
                RaisePropertyChanged("DocObject");
            }
        }

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

        private ObservableCollection<CoshhDocDataObject<CoshhChemicalModel>> chemicals;
        public ObservableCollection<CoshhDocDataObject<CoshhChemicalModel>> Chemicals
        {
            get { return chemicals; }
            set
            { 
                chemicals = value;
                RaisePropertyChanged("Chemicals");
            }
        }

        private ObservableCollection<CoshhDocDataObject<CoshhApparatusModel>> apparatuses;
        public ObservableCollection<CoshhDocDataObject<CoshhApparatusModel>> Apparatuses
        {
            get { return apparatuses; }
            set
            {
                apparatuses = value;
                RaisePropertyChanged("Apparatuses");
            }
        }

        private ObservableCollection<CoshhDocDataObject<CoshhProcessModel>> processes;
        public ObservableCollection<CoshhDocDataObject<CoshhProcessModel>> Processes
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
