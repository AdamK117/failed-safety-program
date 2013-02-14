using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Prism.Events;

using SafetyProgram.Models.DataModels;
using System.Windows.Forms;

namespace SafetyProgram.Data
{
    public class ActiveCoshhData : BaseINPC
    {
        public FactoryObj Factory;
        public ServiceObj Service;
        public CoshhData Data;

        public ActiveCoshhData()
        {
            Data = new CoshhData();
            Factory = new FactoryObj(this);
            Service = new ServiceObj(this);
        }

        public void DataChanged() { RaisePropertyChanged("Data"); }
        public void FileChange() { RaisePropertyChanged("File"); }

        private bool isOpen = false;
        public bool IsOpen() { return isOpen; }
        public bool IsOpen(bool setter)
        {
            isOpen = setter;
            RaisePropertyChanged("IsOpen");
            return isOpen;
        }

        private bool fileChanged = false;
        public bool FileChanged() { return fileChanged; }
        public bool FileChanged(bool changed)
        {
            fileChanged = changed;
            RaisePropertyChanged("FileChanged");
            return fileChanged;
        }

        private object selected;
        public object Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                RaisePropertyChanged("Selected");
            }
        }

        public class FactoryObj
        {
            private ActiveCoshhData parent;

            public FactoryObj(ActiveCoshhData parent) { this.parent = parent; }

            public void DeleteSelected()
            {
                parent.Data.Apparatuses.Remove(parent.Selected as CoshhApparatusModel);
                parent.Data.Processes.Remove(parent.Selected as CoshhProcessModel);
                parent.Data.Chemicals.Remove(parent.Selected as CoshhChemicalModel);

                FactoryEvent();
            }

            public bool Add(object model)
            {
                if (model is CoshhApparatusModel) { parent.Data.Apparatuses.Add(model as CoshhApparatusModel); }
                else if (model is CoshhProcessModel) { parent.Data.Processes.Add(model as CoshhProcessModel); }
                else if (model is CoshhChemicalModel) { parent.Data.Chemicals.Add(model as CoshhChemicalModel); }
                else { return false; }

                FactoryEvent();
                return true;
            }

            public void Remove(object model)
            {
                parent.Data.Apparatuses.Remove(model as CoshhApparatusModel);
                parent.Data.Processes.Remove(model as CoshhProcessModel);
                parent.Data.Chemicals.Remove(model as CoshhChemicalModel);

                FactoryEvent();
            }

            private void FactoryEvent()
            {
                //parent.FileChanged(true);
                //parent.DataChanged();
            }
        }

        public class ServiceObj
        {
            private ActiveCoshhData parent;

            public ServiceObj(ActiveCoshhData parent) { this.parent = parent; }

            public bool Save()
            {
                if (parent.Data.Save())
                {
                    parent.FileChanged(false);
                    return true;
                }
                else { return false; }
            }

            public bool SaveAs()
            {
                if (parent.Data.SaveAs())
                {
                    parent.FileChanged(false);
                    return true;
                }
                else { return false; }
            }

            public void SaveAsPDF()
            {
                parent.Data.SaveAsPDF();
            }

            public bool LoadFile()
            {
                if (Close())
                {
                    parent.Data = new CoshhDataFile();
                    if (parent.Data.Load())
                    {
                        parent.IsOpen(true);
                        parent.FileChanged(false);
                        parent.Selected = null;
                        parent.DataChanged();
                        return true;
                    }
                    else { return false; }
                }
                else { return false; }
            }

            public bool Close()
            {
                if (parent.FileChanged())
                {
                    DialogResult dialogResult = MessageBox.Show("Do you want to save changes you made to " + parent.Data.Title + "?", "Save changes.", MessageBoxButtons.YesNoCancel);

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

                parent.Data.Close();
                parent.IsOpen(false);
                parent.Selected = null;
                parent.FileChanged(false);
                parent.DataChanged();
                return true;
            }

            public bool NewFile()
            {
                if (Close())
                {
                    parent.Data = new CoshhDataFile();
                    parent.IsOpen(true);
                    parent.Selected = null;
                    parent.FileChanged(true);
                    parent.DataChanged();
                    return true;
                }
                else { return false; }
            }
        }
    }
}
