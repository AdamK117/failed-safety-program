using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SafetyProgram.Base;

namespace SafetyProgram.Core.Models
{
    /// <summary>
    /// Defines an implementation for IChemical. A class that holds chemical information.
    /// </summary>
    public sealed class Chemical : IChemical
    {
        /// <summary>
        /// Create a new instance of Chemical. An object that holds general chemical information.
        /// </summary>
        /// <param name="name">The name of the Chemical.</param>
        /// <param name="hazards">The Hazards associated with the Chemical.</param>
        public Chemical(string name, ObservableCollection<IHazard> hazards)
        {
            Helpers.NullCheck(name, hazards);

            this.name = name;
            this.content = hazards;
        }

        private string name;

        /// <summary>
        /// Get or Set the name of the Chemical.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                NameChanged.Raise(this, value);
            }
        }

        /// <summary>
        /// Occurs when the name of the Chemical changes.
        /// </summary>
        public event EventHandler<
            GenericPropertyChangedEventArg<
                string>> NameChanged;

        /// <summary>
        /// Get the unique identifier for this type of object.
        /// </summary>
        /// <example>ChemicalObject</example>
        public string Identifier
        {
            get { return ModelIdentifiers.CHEMICAL_IDENTIFIER; }
        }

        private readonly ObservableCollection<IHazard> content;

        /// <summary>
        /// Get the hazards associated with the chemical.
        /// </summary>
        public ObservableCollection<IHazard> Content 
        {
            get { return content; }
        }

        /// <summary>
        /// Get the hazards associated with the chemical (generic interface).
        /// </summary>
        IEnumerable<IApplicationModel> IHasMany.Content
        {
            get { return content; }
        }
    }
}
