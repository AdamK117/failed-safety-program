﻿using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Prism.Events;

using SafetyProgram.Models.DataModels;
using System.Windows.Forms;
using System;

namespace SafetyProgram.Data
{
    public class ActiveCoshhData : BaseINPC
    {
        public FactoryObj Factory;
        public CoshhData Data;
        private ICoshhDataService service;

        public ActiveCoshhData()
        {
            Data = new CoshhData();
            Factory = new FactoryObj(this);
            service = new CoshhLocalFile();
            dataChangedMonitor();
        }

        #region Metadata & Plumbing

        private void dataChangedMonitor()
        {
            Data.Apparatuses.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Data_CollectionChanged);
            Data.Chemicals.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Data_CollectionChanged);
            Data.Processes.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Data_CollectionChanged);
            Data.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Data_PropertyChanged);
        }

        private void Data_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            dataChangedHandler();
        }

        private void Data_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            dataChangedHandler();
        }

        private void dataChangedHandler()
        {
            FileChanged(true);
        }

        private bool isOpen;
        public bool IsOpen(bool isOpen)
        {
            if (this.isOpen != isOpen)
            {
                this.isOpen = isOpen;
                if (IsOpenChangedEvent != null) { IsOpenChangedEvent(isOpen); }
            }
            return isOpen;
        }
        public bool IsOpen() { return isOpen; }
        public delegate void isOpenChangedDelegate(bool isOpen);
        public event isOpenChangedDelegate IsOpenChangedEvent;

        private bool fileChanged;
        public bool FileChanged(bool fileChanged)
        {
            if (this.fileChanged != fileChanged)
            {
                this.fileChanged = fileChanged;
                if (FileChangedEvent != null) { FileChangedEvent(fileChanged); }
            }
            return fileChanged;
        }
        public bool FileChanged() { return fileChanged; }
        public delegate void fileChangedDelegate(bool fileChanged);
        public event fileChangedDelegate FileChangedEvent;

        private object selected;
        public object Selected(object selected)
        {
            if (this.selected != selected)
            {
                this.selected = selected;
                if (SelectionChangedEvent != null) { SelectionChangedEvent(selected); }
            }
            return selected;            
        }
        public object Selected() { return selected; }
        public delegate void selectionChangedDelegate(object selection);
        public event selectionChangedDelegate SelectionChangedEvent;

        #endregion

        #region Factory Commands

        public class FactoryObj
        {
            private ActiveCoshhData parent;

            public FactoryObj(ActiveCoshhData parent) { this.parent = parent; }

            public void DeleteSelected()
            {
                parent.Data.Apparatuses.Remove(parent.Selected() as CoshhApparatusModel);
                parent.Data.Processes.Remove(parent.Selected() as CoshhProcessModel);
                parent.Data.Chemicals.Remove(parent.Selected() as CoshhChemicalModel);
            }

            public bool Add(object model)
            {
                if (model is CoshhApparatusModel) { parent.Data.Apparatuses.Add(model as CoshhApparatusModel); }
                else if (model is CoshhProcessModel) { parent.Data.Processes.Add(model as CoshhProcessModel); }
                else if (model is CoshhChemicalModel) { parent.Data.Chemicals.Add(model as CoshhChemicalModel); }
                else { return false; }

                return true;
            }

            public void Remove(object model)
            {
                parent.Data.Apparatuses.Remove(model as CoshhApparatusModel);
                parent.Data.Processes.Remove(model as CoshhProcessModel);
                parent.Data.Chemicals.Remove(model as CoshhChemicalModel);
            }
        }

        #endregion

        #region Service Commands

        public bool Save()
        {
            if (service.Save(Data))
            {
                FileChanged(false);
                return true;
            }
            else { return false; }                
        }

        public bool SaveAs()
        {
            if (service.SaveAs(Data))
            {
                FileChanged(false);
                return true;                    
            }
            else { return false; }
        }

        public void SaveAsPDF()
        {
            throw new Exception("Placeholder Command, PDF's not yet implemented");
        }

        public bool Load()
        {
            if (Close())
            {
                if (service.Load(Data))
                {
                    IsOpen(true);
                    FileChanged(false);
                    Selected(null);
                    return true;
                }
                else { return false; }
            }
            else { return false; }
        }

        public bool Close()
        {
            if (FileChanged())
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to save changes you made to " + Data.Title + "?", "Save changes.", MessageBoxButtons.YesNoCancel);

                switch (dialogResult)
                {
                    case DialogResult.Yes:
                        if (!Save()) { goto case DialogResult.Cancel; }
                        break;

                    case DialogResult.No:
                        break;

                    case DialogResult.Cancel:
                        return false;
                }
            }
            Data.Clear();
            service.Close();

            IsOpen(false);
            Selected(null);
            FileChanged(false);

            return true;
        }

        public bool New()
        {
            if (Close())
            {
                Data.Clear();
                IsOpen(true);
                Selected(null);
                FileChanged(true);
                return true;
            }
            else { return false; }
        }

        #endregion
    }
}
