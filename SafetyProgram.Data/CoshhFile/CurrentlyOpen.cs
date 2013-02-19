using System.Windows.Forms;
using System;
using SafetyProgram.UserControls;

namespace SafetyProgram.Data.CoshhFile
{
    public class CurrentlyOpen : BaseINPC
    {
        public CoshhFileData Data;
        private ICoshhDataService service;

        public CurrentlyOpen()
        {
            Data = new CoshhFileData();
            service = new CoshhLocalFile();
            dataChangedMonitor();
        }

        #region Metadata & Plumbing

        //Ugly event handlers
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

        private IDocDataHolder<object> selected;
        public IDocDataHolder<object> Selected(IDocDataHolder<object> selected)
        {
            if (this.selected != selected)
            {
                this.selected = selected;
                if (SelectionChangedEvent != null) { SelectionChangedEvent(selected); }
            }
            return selected;
        }
        public IDocDataHolder<object> Selected()
        {
            return selected;
        }
        public delegate void selectionChangedDelegate(IDocDataHolder<object> selection);
        public event selectionChangedDelegate SelectionChangedEvent;

        public void DeleteSelected() { selected.Remove(); }

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
