using System;

namespace SafetyProgram.Base
{
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
