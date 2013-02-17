using Microsoft.Practices.ServiceLocation;

using SafetyProgram.ICommands;
using SafetyProgram.Models.DataModels;
using SafetyProgram.Data;

using SafetyProgram.Data.CoshhFile;

namespace SafetyProgram.RibbonView
{
    public class RibbonViewModel : BaseINPC
    {
        private CurrentlyOpen currentlyOpen;

        public RibbonViewModel()
        {
            currentlyOpen = ServiceLocator.Current.GetInstance<CurrentlyOpen>();

            currentlyOpen.SelectionChangedEvent += new CurrentlyOpen.selectionChangedDelegate(currentlyOpen_SelectionChangedEvent);
            currentlyOpen.IsOpenChangedEvent += new CurrentlyOpen.isOpenChangedDelegate(currentlyOpen_IsOpenChangedEvent);
        }

        void currentlyOpen_SelectionChangedEvent(object selection)
        {
            RaisePropertyChanged("ChemicalContextTabVisibility");
            RaisePropertyChanged("ApparatusContextTabVisibility");
            RaisePropertyChanged("ProcessContextTabVisibility");
            RaisePropertyChanged("CurrentSelection");
        }

        public ICoshhObject<object> CurrentSelection
        {
            get { return currentlyOpen.Selected(); }
            set { currentlyOpen.Selected(value); }
        }

        public string ChemicalContextTabVisibility
        {
            get { return CurrentSelection is ICoshhObject<CoshhChemicalModel> ? "Visible" : "Collapsed"; }
        }
        public string ApparatusContextTabVisibility
        {
            get { return CurrentSelection is ICoshhObject<CoshhApparatusModel> ? "Visible" : "Collapsed"; }
        }
        public string ProcessContextTabVisibility
        {
            get { return CurrentSelection is ICoshhObject<CoshhProcessModel> ? "Visible" : "Collapsed"; }
        }

        void currentlyOpen_IsOpenChangedEvent(bool isOpen)
        {
            HideBackstage();
            RaisePropertyChanged("RibbonVisibility");
        }

        public bool RibbonVisibility { get { return currentlyOpen.IsOpen(); } }

        public void HideBackstage()
        {
            backstageVisibility = "False";
            RaisePropertyChanged("BackstageVisibility");
        }
        private string backstageVisibility = "False";
        public string BackstageVisibility
        {
            get { return backstageVisibility; }
            set { backstageVisibility = value; }
        }

        #region ICommands (Buttons etc.)

        private ICommandsHolder iCommandsHolder = ServiceLocator.Current.GetInstance<ICommandsHolder>();
        public ICommandsHolder ICommandsHolder { get { return iCommandsHolder; } }

        #endregion        
    }
}
