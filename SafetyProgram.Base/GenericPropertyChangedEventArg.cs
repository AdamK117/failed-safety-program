using System;

namespace SafetyProgram.Base
{
    /// <summary>
    /// Defines an EventArg that may be used for generic data. Used almost exclusively for statically typed (i.e. not string based like INotifyPropertyChanged) events.
    /// </summary>
    /// <typeparam name="TPropertyType">The type of the property that is changing.</typeparam>
    public sealed class GenericPropertyChangedEventArg<TPropertyType> : EventArgs
    {
        public GenericPropertyChangedEventArg(TPropertyType newProperty)
        {
            this.newProperty = newProperty;
        }

        private readonly TPropertyType newProperty;
        public TPropertyType NewProperty
        {
            get
            {
                return newProperty;
            }
        }
    }
}
