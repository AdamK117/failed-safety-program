﻿using Microsoft.Practices.ServiceLocation;

using SafetyProgram.ICommands;
using SafetyProgram.Models.DataModels;
using SafetyProgram.Data;

using SafetyProgram.Data.ChemicalData;

namespace SafetyProgram.RibbonView
{
    public class RibbonViewModel : BaseINPC
    {
        private ActiveCoshhData currentlyOpen;

        public RibbonViewModel()
        {
            currentlyOpen = ServiceLocator.Current.GetInstance<ActiveCoshhData>();

            currentlyOpen.SelectionChangedEvent += new ActiveCoshhData.selectionChangedDelegate(currentlyOpen_SelectionChangedEvent);
            currentlyOpen.IsOpenChangedEvent += new ActiveCoshhData.isOpenChangedDelegate(currentlyOpen_IsOpenChangedEvent);

            LoadedChemicals = new LoadedChemicals();
            RaisePropertyChanged("LoadedChemicals");
        }

        void currentlyOpen_IsOpenChangedEvent(bool isOpen)
        {
            HideBackstage();
            RaisePropertyChanged("RibbonVisibility");
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
            set { currentlyOpen.Selected(value); }
        }

        public bool RibbonVisibility { get { return currentlyOpen.IsOpen(); } }

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

        public LoadedChemicals LoadedChemicals { get; set; }

        #region ICommands (Buttons etc.)

        private ICommandsHolder iCommandsHolder = ServiceLocator.Current.GetInstance<ICommandsHolder>();
        public ICommandsHolder ICommandsHolder { get { return iCommandsHolder; } }

        #endregion        
    }
}
