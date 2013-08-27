using System;
using SafetyProgram.Base;

namespace SafetyProgram.Core.Models
{
    /// <summary>
    /// Defines an implementation of IHazard. A class that holds Hazard information.
    /// </summary>
    public sealed class Hazard : IHazard
    {
        /// <summary>
        /// Construct an instance of the hazard class. A class that holds hazard information together.
        /// </summary>
        /// <param name="signalWord">The Signal Word associated with the Hazard. e.g. H310</param>
        /// <param name="riskPhrase">The Risk Phrase associated with the Hazard. e.g. R20</param>
        /// <param name="warning">The Warning associated with the Hazard. e.g. Flammable</param>
        public Hazard(string signalWord, string riskPhrase, string warning)
        {
            this.signalWord = signalWord;
            this.riskPhrase = riskPhrase;
            this.warning = warning;
        }

        private string signalWord;

        /// <summary>
        /// Get or Set the Signal Word associated with this Hazard.
        /// </summary>
        /// <example>H314</example>
        public string SignalWord
        {
            get
            {
                return signalWord;
            }
            set
            {
                signalWord = value;
                SignalWordChanged.Raise(this, signalWord);
            }
        }

        /// <summary>
        /// Occurs when the SignalWord of the Hazard changes.
        /// </summary>
        public event EventHandler<GenericPropertyChangedEventArg<string>> SignalWordChanged;

        private string riskPhrase;

        /// <summary>
        /// Get or Set the Risk Phrase associated with this Hazard.
        /// </summary>
        /// <example>R30</example>
        public string RPhrase
        {
            get
            {
                return riskPhrase;
            }
            set
            {
                riskPhrase = value;
                RPhraseChanged.Raise(this, riskPhrase);
            }
        }
        
        /// <summary>
        /// Occurs when the RPhrase of the Hazard changes.
        /// </summary>
        public event EventHandler<GenericPropertyChangedEventArg<string>> RPhraseChanged;

        private string warning;

        /// <summary>
        /// Get or Set the Warning associated with the Hazard.
        /// </summary>
        /// <example>Flammable in contact with air.</example>
        public string Warning
        {
            get
            {
                return warning;
            }
            set
            {
                warning = value;
                WarningChanged.Raise(this, warning);
            }
        }

        /// <summary>
        /// Occurs when the Warning of the Hazard changes.
        /// </summary>
        public event EventHandler<GenericPropertyChangedEventArg<string>> WarningChanged;

        /// <summary>
        /// Get the unique IDocObj identifier associated with the Hazard object.
        /// </summary>
        public string Identifier
        {
            get { return ModelIdentifiers.HAZARD_IDENTIFIER; }
        }
    }
}
