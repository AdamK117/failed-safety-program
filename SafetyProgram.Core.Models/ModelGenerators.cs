using System;
using System.Collections.ObjectModel;

namespace SafetyProgram.Core.Models
{
    public static class ModelGenerators
    {
        /// <summary>
        /// Get a paramaterless document generator.
        /// </summary>
        public static Func<IDocument> DocumentGenerator
        {
            get
            {
                return () =>
                    {
                        return new Document(
                            new ObservableCollection<IDocumentObject>(),
                            FormatGenerator());
                    };       
            }            
        }

        /// <summary>
        /// Get a paramaterless format generator.
        /// </summary>
        public static Func<IFormat> FormatGenerator
        {
            get
            {
                return () =>
                    {
                        return new A4Format();
                    };
            }
        }
    }
}
