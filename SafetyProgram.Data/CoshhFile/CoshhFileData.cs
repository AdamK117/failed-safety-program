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
            DocObject = new ObservableCollection<IDocUserControl>();
            Clear();
        }

        public bool Clear()
        {
            Title = "Untitled Safety Document";
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
