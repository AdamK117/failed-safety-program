using System;
using System.Collections.ObjectModel;
using SafetyProgram.Base;

namespace SafetyProgram.Core.Models
{
    /// <summary>
    /// Defines an implementation for IChemicalTable. The underlying data required for a ChemicalTable.
    /// </summary>
    public sealed class ChemicalTable : IChemicalTable
    {
        /// <summary>
        /// Construct an instance of the ChemicalTable class. A class that holds all the data associated with a ChemicalTable.
        /// </summary>
        /// <param name="header">The header of the ChemicalTable.</param>
        /// <param name="chemicals">The Chemicals inside the ChemicalTable.</param>
        public ChemicalTable(string header, ObservableCollection<ICoshhChemical> chemicals)
        {
            Helpers.NullCheck(header, chemicals);

            this.header = header;
            this.Chemicals = chemicals;
        }

        private string header;

        /// <summary>
        /// Get or set the header of the table.
        /// </summary>
        /// <example>Hazardous chemicals used in this experiment.</example>
        public string Header
        {
            get
            {
                return header;
            }
            set
            {
                header = value;
                HeaderChanged.Raise(this, header);
            }
        }

        /// <summary>
        /// Occurs when the Header of this ChemicalTable changes.
        /// </summary>
        public event EventHandler<
            GenericPropertyChangedEventArg<
                string>> HeaderChanged;

        /// <summary>
        /// Get the chemicals contained in the table.
        /// </summary>
        public ObservableCollection<ICoshhChemical> Chemicals { get; private set; }

        /// <summary>
        /// Gets the unique IDocObj identifier for a ChemicalTable.
        /// </summary>
        public string Identifier
        {
            get { return ModelIdentifiers.CHEMICAL_TABLE_IDENTIFIER; }
        }
    }
}
