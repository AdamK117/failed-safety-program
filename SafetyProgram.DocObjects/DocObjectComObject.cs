using System.Windows;
using System.Runtime.InteropServices;

namespace SafetyProgram.DocObjects
{
    public abstract class DocObjectComObject<T>
    {
        private T data;
        private DataObject dataObject;

        /// <summary>
        /// Constructs a DocObjectComObject. An object that interacts with the clipboard more easily than manual implementation.
        /// </summary>
        /// <param name="data">Raw data input</param>
        /// <param name="identifier">String identifier for the raw data on the clipboard</param>
        public DocObjectComObject(T data, string identifier)
        {
            this.data = data;

            dataObject = new DataObject();

            dataObject.SetData(identifier, cloneDataInput(data));
            dataObject.SetData(DataFormats.Text, getTextFormat(data));
            dataObject.SetData(DataFormats.Rtf, getRtfFormat(data));
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

        /// <summary>
        /// Copies the DocObjectComObject to the clipboard
        /// </summary>
        public void CopyToClipboard()
        {
            try
            {
                Clipboard.SetDataObject(dataObject);
            }
            catch(COMException)
            {
                MessageBox.Show("Can't access the clipboard!");
            }
        }

        /// <summary>
        /// Gets the DataObject constructed for this DocObjectComObject instance
        /// </summary>
        /// <returns></returns>
        public DataObject GetDataObject()
        {
            return dataObject;
        }
    }
}
