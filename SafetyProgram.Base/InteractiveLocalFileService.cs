using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Base
{
    public sealed class InteractiveLocalFileService<T> : IService<T>
    {
        private readonly bool 
            canNew = true,
            canLoad = true,
            canSave = true, 
            canSaveAs = true;

        private string path;

        private readonly IConfiguration appConfiguration;
        private readonly ILocalFileFactory<T> itemFactory;

        public InteractiveLocalFileService(IConfiguration appConfiguration, ILocalFileFactory<T> itemFactory)
        {
            if (
                appConfiguration != null &&
                itemFactory != null
                )
            {
                this.appConfiguration = appConfiguration;
                this.itemFactory = itemFactory;
            }
            else throw new ArgumentNullException();
        }

        public T New()
        {
            if (CanNew())
            {
                path = "";
                return itemFactory.CreateNew();
            }
            else throw new InvalidOperationException("Cannot create a new document");
        }

        public bool CanNew()
        {
            return canNew;
        }

        public T Load()
        {
            if (CanLoad())
            {
                var openFileDialog = new OpenFileDialog()
                {
                    Filter = "Some XML Docs (.xml)|*.xml",
                    Multiselect = false,
                    CheckFileExists = true
                };

                switch (openFileDialog.ShowDialog())
                {
                    case DialogResult.OK:
                        if (File.Exists(openFileDialog.FileName))
                        {
                            path = openFileDialog.FileName;

                            var xElement = XElement.Load(path);

                            return itemFactory.Load(xElement);
                        }
                        else throw new FileNotFoundException("The file selected does not exist", openFileDialog.FileName);

                    default:
                        throw new ArgumentException("User cancelled out of selecting a file to load");

                }
            }
            else throw new InvalidOperationException();
        }

        public bool CanLoad()
        {
            return canLoad;
        }

        public void Save(T data)
        {
            if (CanSave(data))
            {
                if (String.IsNullOrEmpty(path))
                {
                    SaveAs(data);
                }
                else
                {
                    var xDocument = new XDocument();

                    xDocument.Add(itemFactory.Store(data));

                    xDocument.Save(path);
                }
            }
            else throw new InvalidOperationException();
        }

        public bool CanSave(T data)
        {
            return canSave;
        }

        public void SaveAs(T data)
        {
            if (CanSaveAs(data))
            {
                var saveFileDialog = new SaveFileDialog()
                {
                    Filter = "SomeRandomXml|*.xml",
                    Title = "Save As"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    path = saveFileDialog.FileName;
                    Save(data);
                }
                else throw new ArgumentException("User cancelled out of SaveAs dialog");
            }
            else throw new InvalidOperationException();  
        }

        public bool CanSaveAs(T data)
        {
            return canSaveAs;
        }

        public void Close(T data)
        {
            //Close connections etc.
        }
    }
}
