using System;
using SafetyProgram.Base;

namespace SafetyProgram.Core.Models
{
    /// <summary>
    /// Defines a Quantity class. A class that holds quantity information together.
    /// </summary>
    public sealed class Quantity : IQuantity
    {
        /// <summary>
        /// Construct an instance of the quantity class. A class that holds quantity information together (number and amount).
        /// </summary>
        /// <param name="_value">The value (number) associated with this Quantity.</param>
        /// <param name="unit">The units associated with the Value.</param>
        public Quantity(decimal _value, string unit)
        {
            this._value = _value;
            this.unit = unit;
        }

        private decimal _value;

        /// <summary>
        /// Get the value associated with this quantity.
        /// </summary>
        /// <example>100</example>
        public decimal Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                ValueChanged.Raise(this, _value);
            }
        }

        /// <summary>
        /// Occurs when the value changes.
        /// </summary>
        public event EventHandler<
            GenericPropertyChangedEventArg<
                decimal>> ValueChanged;

        private string unit;

        /// <summary>
        /// Get or set the unit associated with this quantity.
        /// </summary>
        /// <example>mg</example>
        public string Unit
        {
            get
            {
                return unit;
            }
            set
            {
                unit = value;
                UnitChanged.Raise(this, unit);
            }
        }     
   
        /// <summary>
        /// Occurs when the unit changes.
        /// </summary>
        public event EventHandler<
            GenericPropertyChangedEventArg<
                string>> UnitChanged;

        public string Identifier
        {
            get { return ModelIdentifiers.QUANTITY_IDENTIFIER; }
        }
    }
}
