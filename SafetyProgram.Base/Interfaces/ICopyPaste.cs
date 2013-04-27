using System.Runtime.InteropServices;
using System.Windows;

namespace SafetyProgram.Base.Interfaces
{
    public interface ICopyPasteable
    {
        string ComIdentity { get; }

        IDataObject GetDataObject();
    }

    public static class ICopyPasteableMethods
    {
        public static void CopyToClipboard(ICopyPasteable obj)
        {
            try
            {
                Clipboard.SetDataObject(obj.GetDataObject(), true);
            }
            catch (COMException)
            {
                MessageBox.Show("Can't access the clipboard!");
                throw;
            }
        }

        public static T GetFromClipboard<T>(ICopyPasteable obj)
        {
            if (Clipboard.ContainsData(obj.ComIdentity))
            {
                try
                {
                    object clipboardData = Clipboard.GetData(obj.ComIdentity);
                    return (T)clipboardData;
                }
                catch (COMException)
                {
                    MessageBox.Show("Can't Access the Clipboard to paste!");
                    throw;
                }
            }
            else return default(T);
        }
    }
}
