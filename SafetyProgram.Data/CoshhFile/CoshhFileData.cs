using SafetyProgram.Models.DataModels;
using System.Collections.ObjectModel;
using SafetyProgram.UserControls;
using SafetyProgram.UserControls.MainWindowControls.ChemicalTable;

namespace SafetyProgram.Data.CoshhFile
{
    public class CoshhFileData : BaseINPC
    {
        public bool Clear()
        {
            DocObject.Clear();
            return true;
        }

        private ObservableCollection<IDocUserControl> docObject = new ObservableCollection<IDocUserControl>();
        public ObservableCollection<IDocUserControl> DocObject
        {
            get { return docObject; }
            set
            {
                docObject = value;
                RaisePropertyChanged("DocObject");
            }
        }      
    }
}
