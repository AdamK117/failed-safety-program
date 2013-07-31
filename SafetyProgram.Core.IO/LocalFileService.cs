using System;
using System.Xml.Linq;
using SafetyProgram.Base;

namespace SafetyProgram.Core.IO
{
    public sealed class LocalFileService<T> : IIOService<T>
    {
        private readonly IStorageConverter<T, XElement> ioFactory;

        public LocalFileService(IStorageConverter<T, XElement> ioFactory, string path)
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
