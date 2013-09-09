using System;
using System.Collections.ObjectModel;
using SafetyProgram.Base;

namespace SafetyProgram.Core.Models
{
    /// <summary>
    /// Defines an interface for a Chemical in the SafetyProgram. The chemical 
    /// requires a name and hazards associated with it.
    /// </summary>
    public interface IChemical : 
        IApplicationModel,
        IHasMany<IHazard>
    {
        /// <summary>
        /// Gets the name of the chemical.
        /// </summary>
        /// <example>Acetonitrile</example>
        string Name { get; set; }

        /// <summary>
        /// Occurs when the name of the chemical changes.
        /// </summary>
        event EventHandler<
            GenericPropertyChangedEventArg<
            string>> NameChanged;
    }
}
