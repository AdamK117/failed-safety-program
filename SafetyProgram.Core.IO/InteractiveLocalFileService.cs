using System;
using System.Xml.Linq;
using SafetyProgram.Base;

namespace SafetyProgram.IO
{
    /// <summary>
    /// Defines an implementation for an I/O service that uses the local file system on the client machine.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class InteractiveLocalFileService<T> : IIOService<T>
    {
        private readonly IStorageConverter<T, XElement> ioFactory;

        /// <summary>
        /// Construct an instance of a local file system I/O service that uses user prompts.
        /// </summary>
        public InteractiveLocalFileService(IStorageConverter<T, XElement> ioFactory)
        {
            Helpers.NullCheck(ioFactory);

            this.ioFactory = ioFactory;
        }

        public T New()
        {
            throw new NotImplementedException();
        }

        public bool CanNew()
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public void Save(T data)
        {
            throw new NotImplementedException();
        }

        public bool CanSave(T data)
        {
            throw new NotImplementedException();
        }

        public void SaveAs(T data)
        {
            throw new NotImplementedException();
        }

        public bool CanSaveAs(T data)
        {
            throw new NotImplementedException();
        }

        public T Load()
        {
            throw new NotImplementedException();
        }

        public bool CanLoad()
        {
            throw new NotImplementedException();
        }
    }
}
