using System.Runtime.InteropServices;
using System.Windows;

namespace SafetyProgram.Base.Interfaces
{
    public interface ICopyPasteable
    {
        string ComIdentity { get; }

        IDataObject GetDataObject();
    }
}
