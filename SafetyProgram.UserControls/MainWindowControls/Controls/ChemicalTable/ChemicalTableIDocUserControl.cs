using System;
using System.Collections.Generic;
using System.Windows.Controls;
using SafetyProgram.Models.DataModels;
using System.Collections.ObjectModel;

namespace SafetyProgram.UserControls.MainWindowControls.ChemicalTable
{
    public class ChemicalTableIDocUserControl : IDocUserControl
    {
        protected ICollection<IDocUserControl> parent;
        protected ObservableCollection<CoshhDocDataObject<CoshhChemicalModel>> data;
        protected bool selected;

        public ChemicalTableIDocUserControl(ICollection<IDocUserControl> parent, ObservableCollection<CoshhDocDataObject<CoshhChemicalModel>> data)
        {
            this.parent = parent;
            this.data = data;
        }

        public UserControl Display()
        {
            return new ChemicalTableView(data); 
        }

        public bool canRemove()
        {
            return parent.Contains(this);
        }

        public bool Remove()
        {
            return parent.Remove(this);
        }

        public bool canEdit()
        {
            throw new NotImplementedException();
        }

        public bool Edit()
        {
            throw new NotImplementedException();
        }


        public bool Selected()
        {
            return selected;
        }

        public bool Selected(bool isSelected)
        {
            selected = isSelected;
            return selected;
        }


        public bool CanSelect()
        {
            return false;
        }
    }
}
