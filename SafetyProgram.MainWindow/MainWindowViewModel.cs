using System;
using System.Linq;
using System.Collections.ObjectModel;

using Microsoft.Practices.ServiceLocation;

using SafetyProgram.Models.DataModels;
using SafetyProgram.Data;

namespace SafetyProgram.MainWindow
{
    public class MainWindowViewModel : BaseINPC
    {
        private ActiveCoshhData currentlyOpen;

        public MainWindowViewModel()
        {     
            currentlyOpen = ServiceLocator.Current.GetInstance<ActiveCoshhData>();
            currentlyOpen.IsOpenChangedEvent += new ActiveCoshhData.isOpenChangedDelegate(currentlyOpen_IsOpenChangedEvent);
        }

        void currentlyOpen_IsOpenChangedEvent(bool isOpen)
        {
            RaisePropertyChanged("IsOpen");
        }
        public string IsOpen
        {
            get { return currentlyOpen.IsOpen() ? "Visible" : "Hidden"; }
        }

        #region Data

        public string Title
        {
            get { return currentlyOpen.Data.Title; }
            set
            {
                currentlyOpen.Data.Title = value;
                RaisePropertyChanged("Title");
            }
        }

        public string AdditionalComments
        {
            get { return currentlyOpen.Data.AdditionalComments; }
            set
            { 
                currentlyOpen.Data.AdditionalComments = value;
                RaisePropertyChanged("AdditionalComments");
            }
        }

        #endregion
    }
}
