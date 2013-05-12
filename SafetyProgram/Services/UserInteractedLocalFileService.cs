using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Services
{
    public class UserInteractedLocalFileService<T> : IService<T>
        where T : IDocument
    {
        private readonly bool canNew = true, canLoad = true, canSave = true, canSaveAs = true;
        private string path;

        private readonly IConfiguration appConfig;
        private readonly ILocalFileFactory<T> itemFactory;

        public UserInteractedLocalFileService(IConfiguration appConfig, ILocalFileFactory<T> itemFactory)
        {
            if (appConfig != null) this.appConfig = appConfig;
            else throw new ArgumentNullException();

            if (itemFactory != null) this.itemFactory = itemFactory;
            else throw new ArgumentNullException();
        }

        public T New()
        {
            return itemFactory.CreateNew();
        }

        public bool CanNew()
        {
            return canNew;
        }

        public T Load()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog()
            {
                Filter = "Coshh Documents (.xml)|*.xml",
                Multiselect = false,
                CheckFileExists = true
            };

            switch (openFileDialog1.ShowDialog())
            {
                case DialogResult.OK:
                    if (File.Exists(openFileDialog1.FileName))
                    {
                        path = openFileDialog1.FileName;

                        XElement xDoc = XElement.Load(path);

                        return itemFactory.Load(xDoc);
                    }
                    else throw new FileNotFoundException("The file selected does not exist", openFileDialog1.FileName);

                default:
                    throw new ArgumentException("User cancelled out of selecting a file to load");
            }
        }

        public bool CanLoad()
        {
            return canLoad;
        }

        public void Save(T data)
        {
            if (String.IsNullOrWhiteSpace(path))
            {
                SaveAs(data);
            }
            else
            {
                XDocument xDoc = new XDocument();

                xDoc.Add(itemFactory.Store(data));

                xDoc.Save(path);
            }
        }

        public bool CanSave(T data)
        {
            return canSave;
        }

        public void SaveAs(T data)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Coshh Safety Document|*.xml";
            saveFileDialog.Title = "Save As";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                //TODO: Figure out why this is blocking saving a file
                //Directory.GetAccessControl(path);
                path = saveFileDialog.FileName;
                Save(data);
            }
            else throw new ArgumentException("User cancelled out of SaveAs dialog");
        }

        public bool CanSaveAs(T data)
        {
            return canSaveAs;
        }

        public void Close(T data)
        {
            //Ask to save changes (if applicable)
            if (data.EditedFlag == true)
            {
                switch (MessageBox.Show("Do you want to save changes to " + data.Title + "?", "", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:
                        Save(data);
                        break;

                    case DialogResult.No:
                        break;

                    default:
                        throw new ArgumentException("User cancelled out of closing the document");
                }
            }

            //TODO: Close document implementation
        }
    }
}
