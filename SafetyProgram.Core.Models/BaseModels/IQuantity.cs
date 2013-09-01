using System;
using SafetyProgram.Base;

namespace SafetyProgram.Core.Models
{
    /// <summary>
    /// Defines an interface for describing quantities. Previously, these 
    /// were held as separate fields on the chemical class.
    /// </summary>
    public interface IQuantity
    {
        /// <summary>
        /// Get the value associated with this IQuantity
        /// </summary>
        /// <example>100</example>
        decimal Value { get; set; }

        /// <summary>
        /// Occurs when the value changes.
        /// </summary>
        event EventHandler<
            GenericPropertyChangedEventArg<
                decimal>> ValueChanged;

        /// <summary>
        /// Get or Set the units associated with this IQuantity
        /// </summary>
        /// <example>mgs</example>
        string Unit { get; set; }

        /// <summary>
        /// Occurs when the unit changes.
        /// </summary>
        event EventHandler<
            GenericPropertyChangedEventArg<
                string>> UnitChanged;
    }
}
