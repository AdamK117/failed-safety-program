﻿using System;
using SafetyProgram.Base;

namespace SafetyProgram.Core.Models
{
    /// <summary>
    /// Defines an interface for a Chemical Table in the Safety Document. The table contains a collection of chemicals and a header.
    /// </summary>
    public interface IChemicalTable : 
        IDocumentObject,
        IHasManyT<ICoshhChemical>
    {
        /// <summary>
        /// Gets or Sets the header of the ChemicalTable.
        /// </summary>
        /// <example>Highly toxic chemicals used in this experiment.</example>
        string Header { get; set; }

        /// <summary>
        /// Occurs when the header of the ChemicalTable changes.
        /// </summary>
        event EventHandler<
            GenericPropertyChangedEventArg<
                string>> HeaderChanged;
    }
}
