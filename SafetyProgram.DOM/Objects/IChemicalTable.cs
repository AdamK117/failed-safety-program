using System;
using System.Collections.ObjectModel;
using SafetyProgram.Base;

namespace SafetyProgram.DOM.Objects
{
    /// <summary>
    /// Defines an interface for a Chemical Table in the Safety Document. The table contains a collection of chemicals and a header.
    /// </summary>
    public interface IChemicalTable : IDocObj
    {
        /// <summary>
        /// Gets or Sets the header of the ChemicalTable.
        /// </summary>
        /// <example>Highly toxic chemicals used in this experiment.</example>
        string Header { get; set; }

        /// <summary>
        /// Occurs when the header of the ChemicalTable changes.
        /// </summary>
        event EventHandler<GenericPropertyChangedEventArg<bool>> HeaderChanged;

        /// <summary>
        /// Gets the chemicals in the ChemicalTable.
        /// </summary>
        ObservableCollection<IHazard> Chemicals { get; }
    }
}
