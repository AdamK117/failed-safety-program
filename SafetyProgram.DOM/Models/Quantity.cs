
namespace SafetyProgram.DOM.Models
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
        /// Gets the value associated with this quantity.
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
            }
        }

        private string unit;

        /// <summary>
        /// Gets or Sets the unit associated with this quantity.
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
            }
        }
    }
}
