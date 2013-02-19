using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace SafetyProgram.UserControls
{
    public class CoshhDocDataObject<T> : IDocDataHolder<T>
    {
        protected T data;
        protected ObservableCollection<CoshhDocDataObject<T>> parent;
        protected bool selected = false;

        public CoshhDocDataObject(ObservableCollection<CoshhDocDataObject<T>> parent, T data)
        {
            this.parent = parent;
            this.data = data;
        }

        public T Data()
        {
            return data;
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
            return true;
        }
    }
}
