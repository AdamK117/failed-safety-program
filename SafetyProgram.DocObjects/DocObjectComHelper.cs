using System.Windows;

namespace SafetyProgram.DocObjects
{
    public abstract class DocObjectComHelper<T>
    {
        protected string comIdentifier;

        /// <summary>
        /// Constructs an instance of the DocObjectComHelper class.
        /// This is used to help with COM interaction, allowing easy(er) DataObject creation
        /// </summary>
        /// <param name="comIdentifier"></param>
        public DocObjectComHelper(string comIdentifier)
        {
            this.comIdentifier = comIdentifier;
        }

        /// <summary>
        /// Populates and returns a DataObject from the data provided.
        /// </summary>
        /// <param name="data">Data input</param>
        /// <returns>Populated DataObject, ready for the clipboard.</returns>
        public DataObject MakeDataObject(T data)
        {
            DataObject dataObject = new DataObject();

            dataObject.SetData(comIdentifier, cloneDataInput(data));
            dataObject.SetData(DataFormats.Text, getTextFormat(data));
            dataObject.SetData(DataFormats.Rtf, getRtfFormat(data));

            return dataObject;
        }

        /// <summary>
        /// Gets the ComIdentifier for the DocObjectComHelper
        /// </summary>
        public string ComIdentifier
        {
            get
            {
                return comIdentifier;
            }
        }

        /// <summary>
        /// Gets a clone of the raw data input
        /// </summary>
        /// <param name="data">Input data</param>
        /// <returns>Cloned input data</returns>
        protected abstract T cloneDataInput(T data);

        /// <summary>
        /// Gets the text format for the raw data input (for notepad and text-only applications)
        /// </summary>
        /// <param name="data">Raw data input</param>
        /// <returns>Data as a text string</returns>
        protected abstract string getTextFormat(T data);

        /// <summary>
        /// Gets the RTF format of the raw data input
        /// </summary>
        /// <param name="data">The raw data input</param>
        /// <returns>RTF string format for the raw data</returns>
        protected abstract string getRtfFormat(T data);
    }
}
