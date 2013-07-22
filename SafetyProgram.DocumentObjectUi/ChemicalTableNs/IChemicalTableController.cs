using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SafetyProgram.DocumentObjectUi.ChemicalTableNs.Commands;
using SafetyProgram.Models;

namespace SafetyProgram.DocumentObjectUi.ChemicalTableNs
{
    /// <summary>
    /// Defines an interface for a Chemical table controller.
    /// </summary>
    public interface IChemicalTableController : IDocumentObjectController
    {
        /// <summary>
        /// Get the commands available to the chemical table.
        /// </summary>
        IChemicalTableCommands Commands { get; }

        /// <summary>
        /// Gets the underlying IChemicalTable model.
        /// </summary>
        IChemicalTable Model { get; }
    }
}
