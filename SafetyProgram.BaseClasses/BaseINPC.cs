using System.ComponentModel;
using System;

namespace SafetyProgram.BaseClasses
{
    [Serializable]
    public abstract class BaseINPC : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
