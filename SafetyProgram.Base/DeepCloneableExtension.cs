using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Base
{
    public static class DeepCloneableExtensions
    {
        /// <summary>
        /// Gets a data object containing an IEnumerable of IDeepCloneable items. This may be added to the clipboard or used in drag & drop operations.
        /// </summary>
        /// <typeparam name="T">The IDeepCloneable item in the IEnumerable</typeparam>
        /// <param name="data">An IEnumerable containing IDeepCloneables</param>
        /// <param name="comIdentity">The COM identity to wrap into the DataObject</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static IDataObject GetDataObject<T>(this IEnumerable<T> items, string comIdentity)
            where T : IDeepCloneable<T>
        {
            IDataObject dataObject = new DataObject();
            IList<T> clonedItems = new List<T>();

            foreach (IDeepCloneable<T> item in items)
            {
                T clonedItem = item.DeepClone();

                clonedItems.Add(clonedItem);
            }

            dataObject.SetData(comIdentity, clonedItems);

            return dataObject;
        }

        /// <summary>
        /// Attempts to copy the IDeepCloneable contents of an IEnumerable into the clipboard.
        /// </summary>
        /// <typeparam name="T">An object that implements IDeepCloneable</typeparam>
        /// <param name="data">An IEnumerable containing IDeepCloneable items</param>
        /// <param name="comIdentity">The ComIdentity set for the clipboard operation. Example: CoshhChemicalModels</param>
        /// <exception cref="System.Runtime.InteropServices.COMException">Thrown if the clipboard is inaccessable.</exception>
        public static void TryCopy<T>(this IEnumerable<T> data, string comIdentity)
            where T : IDeepCloneable<T>
        {
            try
            {
                Clipboard.SetDataObject(
                    GetDataObject<T>(data, comIdentity)
                    );
            }
            catch (COMException)
            {
                MessageBox.Show("Can't access the clipboard!");
                throw;
            }            
        }

        /// <summary>
        /// Tries to get an IDeepCloneable from the clipboard with the comIdentity
        /// </summary>
        /// <typeparam name="T">An IDeepCloneable</typeparam>
        /// <param name="comIdentity">The identity use in the Com (must be the same as was set when copying it to the clipboard)</param>
        /// <returns></returns>
        public static IDeepCloneable<T> TryPaste<T>(string comIdentity)
            where T : IDeepCloneable<T>
        {
            if (Clipboard.ContainsData(comIdentity))
            {
                try
                {
                    object uncastedData = Clipboard.GetData(comIdentity);
                    T castedData = (T)uncastedData;
                    return castedData;
                }
                catch (COMException)
                {
                    MessageBox.Show("Can't access the clipboard!");
                    throw;
                }
            }
            else return null;
        }

        /// <summary>
        /// Attempts to paste data from the clipboard into the collection.
        /// </summary>
        /// <typeparam name="T">The type of data being pasted</typeparam>
        /// <param name="target">The target of the pasted data</param>
        /// <param name="comIdentity">The Com identity of the pasted data. Example: "CoshhChemicalModels"</param>
        /// <exception cref="System.Runtime.InteropServices.COMException">Thrown if the COM (clipboard etc.) cannot be accessed</exception>
        /// <exception cref="System.OutOfMemoryException">Thrown when the items, T, aren't [Serializable].</exception>
        /// <exception cref="System.InvalidCastException"></exception>
        public static void TryPasteInto<T>(this ICollection<T> target, string comIdentity)
            where T : IDeepCloneable<T>
        {
            if (Clipboard.ContainsData(comIdentity))
            {
                try
                {
                    object uncastedData = Clipboard.GetData(comIdentity);
                    IEnumerable<T> data = (IEnumerable<T>)uncastedData;

                    foreach (T entry in data)
                    {
                        target.Add(entry);
                    }
                }
                catch (COMException)
                {
                    MessageBox.Show("Can't access the clipboard!");
                    throw;
                }
            }
        }

        /// <summary>
        /// Gets a plain DataObject for an ICloneable which only contains a clone of the item.
        /// </summary>
        /// <typeparam name="T">Type of IDeepCloneable to clone into the DataObject</typeparam>
        /// <param name="item">The item to deepclone into an IDataObject</param>
        /// <param name="comIdentity">The COM identity to use when retrieveing from the clipboard etc.</param>
        /// <returns></returns>
        public static IDataObject GetDataObject<T>(this IDeepCloneable<T> item, string comIdentity)
        {
            var dataObject = new DataObject();
            T clonedItem = item.DeepClone();
            dataObject.SetData(comIdentity, item);

            return dataObject;
        }

        /// <summary>
        /// Tries to copy an IDeepCloneable to the clipboard.
        /// </summary>
        /// <typeparam name="T">An IDeepCloneable type.</typeparam>
        /// <param name="item">The item to copy to the clipboard</param>
        /// <param name="comIdentity">The comIdentity for the item (used when retriveing it from the clipboard).</param>
        /// <exception cref="System.Runtime.InteropServices.COMException">Thrown if the clipboard is inaccessable.</exception>
        public static void TryCopy<T>(this IDeepCloneable<T> item, string comIdentity)
            where T : IDeepCloneable<T>
        {
            Clipboard.SetDataObject(GetDataObject<T>(item, comIdentity));
        }        

        public static IEnumerable<T> DeepCloneList<T>(this IEnumerable<T> items)
            where T : IDeepCloneable<T>
        {
            foreach (T item in items)
            {
                yield return item.DeepClone();
            }
        }
    }
}
