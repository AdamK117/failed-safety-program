using Microsoft.Practices.ServiceLocation;

using SafetyProgram.ICommands;
using SafetyProgram.Models.DataModels;
using SafetyProgram.Data;

using SafetyProgram.Data.DOM;
using SafetyProgram.UserControls;

namespace SafetyProgram.RibbonView
{
    public class RibbonViewModel : BaseINPC
    {
        private CoshhDocument currentlyOpen;

        public RibbonViewModel()
        {
            currentlyOpen = ServiceLocator.Current.GetInstance<CoshhDocument>();

            currentlyOpen.SelectionChanged += new CoshhDocument.selectionChangedDelegate(currentlyOpen_SelectionChangedEvent);
            currentlyOpen.IsOpenChanged += new CoshhDocument.isOpenChangedDelegate(currentlyOpen_IsOpenChangedEvent);
        }

        void currentlyOpen_SelectionChangedEvent(object selection)
        {
            RaisePropertyChanged("ChemicalContextTabVisibility");
            RaisePropertyChanged("ApparatusContextTabVisibility");
            RaisePropertyChanged("ProcessContextTabVisibility");
            RaisePropertyChanged("CurrentSelection");
        }

        public object CurrentSelection
        {
            get { return currentlyOpen.Selected(); }
            set { currentlyOpen.Selected(value as IDocObject); }
        }

        public string ChemicalContextTabVisibility
        {
            get { return CurrentSelection is CoshhChemicalModel ? "Visible" : "Collapsed"; }
        }
        public string ApparatusContextTabVisibility
        {
            get { return CurrentSelection is CoshhApparatusModel ? "Visible" : "Collapsed"; }
        }
        public string ProcessContextTabVisibility
        {
            get { return CurrentSelection is CoshhProcessModel ? "Visible" : "Collapsed"; }
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
