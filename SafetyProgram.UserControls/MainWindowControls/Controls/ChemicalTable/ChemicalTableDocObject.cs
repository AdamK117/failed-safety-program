using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using SafetyProgram.Models.DataModels;

namespace SafetyProgram.UserControls.MainWindowControls.ChemicalTable
{
    public class ChemicalTableDocObject : IDocObject
    {
        protected ICollection<IDocObject> parent;

        public ChemicalTableDocObject(ICollection<IDocObject> parent, IEnumerable<CoshhChemicalModel> chemicals)
        {
            display = new ChemicalTableView(chemicals);
            this.parent = parent;
        }

        private UserControl display;
        public UserControl Display()
        {
            return display;
        }

        public bool canRemove()
        {
            return parent.Contains(this) ? true : false;
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

        public bool CanSelect()
        {
            throw new NotImplementedException();
        }

        public bool Selected()
        {
            throw new NotImplementedException();
        }

        public bool Selected(bool selected)
        {
            throw new NotImplementedException();
        }
    }
}
