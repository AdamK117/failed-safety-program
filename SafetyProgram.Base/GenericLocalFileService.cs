using System;
using System.IO;
using System.Xml.Linq;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Base
{
    public sealed class GenericLocalFileService<T> : IService<T>
    {
        private readonly string path;
        private readonly ILocalFileFactory<T> itemFactory;

        public GenericLocalFileService(ILocalFileFactory<T> itemFactory, string path)
        {
            if (itemFactory != null) this.itemFactory = itemFactory;
            else throw new ArgumentNullException();

            if (File.Exists(path)) this.path = path;
            else throw new FileNotFoundException("Specified file path '" + path + "' could not be found.");
        }

        public T New()
        {
            return itemFactory.CreateNew();
        }

        public bool CanNew()
        {
            return true;
        }

        public T Load()
        {
            var file = XDocument.Load(path);

            var fileNode = file.Element(itemFactory.XmlIdentifier);
            if (fileNode != null)
            {
                return itemFactory.Load(fileNode);
            }
            else throw new InvalidDataException("No appropriate Xml root '" + itemFactory.XmlIdentifier + "' was found in the specified document");
        }

        public bool CanLoad()
        {
            return true;
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

        public void Close(T data)
        {
            throw new NotImplementedException();
        }
    }
}
