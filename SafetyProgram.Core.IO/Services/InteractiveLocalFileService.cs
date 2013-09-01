using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using SafetyProgram.Base;

namespace SafetyProgram.Core.IO
{
    /// <summary>
    /// Defines an implementation for a service that retrieves fils 
    /// from the local file system and prompts the user for file selections
    /// and save locations.
    /// </summary>
    /// <typeparam name="T">The type of in-memory object handled by the service.</typeparam>
    public sealed class InteractiveLocalFileService<T> : IIOService<T>
    {
        private readonly bool
            canNew = true,
            canLoad = true,
            canSave = true,
            canSaveAs = true;

        private string path;

        private readonly ILocalStorageConverter<T, XElement> itemFactory;
        private readonly IGenerator<T> itemGenerator;

        /// <summary>
        /// Construct an instance of the interactive file service.
        /// </summary>
        /// <param name="itemGenerator"></param>
        /// <param name="itemConverter"></param>
        public InteractiveLocalFileService(IGenerator<T> itemGenerator, 
            ILocalStorageConverter<T, XElement> itemConverter)
        {
            Helpers.NullCheck(itemGenerator, itemConverter);

            this.itemFactory = itemConverter;
            this.itemGenerator = itemGenerator;
        }

        public T New()
        {
            if (CanNew())
            {
                path = "";
                return itemGenerator.CreateNew();
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
                    Filter =  "Xml Document (.xml)|*.xml",
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
                        else throw new FileNotFoundException("File not found", openFileDialog.FileName);

                    default:
                        throw new ArgumentException();
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

        public void Disconnect()
        {
            //Close connections etc.
        }
    }
}