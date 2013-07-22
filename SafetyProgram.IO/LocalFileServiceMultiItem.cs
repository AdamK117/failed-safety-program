using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace SafetyProgram.IO
{
    public sealed class LocalFileServiceMultiItem<TContent> : IServiceMultiItem<TContent>
    {
        public LocalFileServiceMultiItem(IStorageConverter<TContent, XElement> ioFactory, string path)
        { }

        public IEnumerable<TContent> Load(Action<TContent> callback)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TContent> Load()
        {
            throw new NotImplementedException();
        }

        public bool CanLoad()
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }
    }
}
