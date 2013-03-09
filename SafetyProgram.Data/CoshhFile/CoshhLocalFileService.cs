using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml;

using SafetyProgram.Models.DataModels;
using SafetyProgram.FSharp;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using SafetyProgram.UserControls;
using SafetyProgram.UserControls.MainWindowControls.ChemicalTable;
using System.Xml.Serialization;
using System.IO;

namespace SafetyProgram.Data.CoshhFile
{
    class CoshhLocalFile : ICoshhDataService
    {
        private string path;

        public bool Save(CoshhFileData data)
        {
            if (String.IsNullOrWhiteSpace(path)) { return false; }

            CoshhXmlWriter.XmlWrite writer = new CoshhXmlWriter.XmlWrite();
            writer.writeDocument(path, data.DocObject);

            setPath(path);

            return true;
        }

        public bool Load(CoshhFileData data)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Coshh Documents (.xml)|*.xml";
            openFileDialog1.Multiselect = false;

            DialogResult dialogResult = openFileDialog1.ShowDialog();

            if (dialogResult != DialogResult.OK) { return false; }
            else if (!setPath(openFileDialog1.FileName)) { return false; }
            else if (!loadFile(Path(), data)) { return false; }
            return true;
        }

        private bool loadFile(string path, CoshhFileData coshhData)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(path);

            CoshhXmlReader.XmlParser parser = new CoshhXmlReader.XmlParser();

            coshhData.DocObject.Add
                (
                    new ChemicalTableIDocUserControl
                        (
                            coshhData.DocObject,
                            createCoshhDocDataObjectCollection<CoshhChemicalModel>(parser.GetCoshhChemicalModels(xdoc))
                        )
                );

            return true;
        }

        private ObservableCollection<CoshhDocDataObject<T>> createCoshhDocDataObjectCollection<T>(IEnumerable<T> data)
        {
            ObservableCollection<CoshhDocDataObject<T>> oc = new ObservableCollection<CoshhDocDataObject<T>>();

            foreach (T model in data)
            {
                oc.Add(new CoshhDocDataObject<T>(oc, model));
            }
            return oc;
        }

        public bool SaveAs(CoshhFileData data)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Coshh Safety Document|*.xml";
            saveFileDialog.Title = "Save As";

            DialogResult userResponse = saveFileDialog.ShowDialog();

            if (userResponse == DialogResult.OK)
            {
                path = saveFileDialog.FileName;
                Save(data);
                return true;
            }
            else { return false; }
        }

        public bool Close()
        {
            //Disconnect etc here
            return true;
        }

        public string Path()
        {
            return path;
        }

        private bool setPath(string path)
        {
            if (File.Exists(path))
            {
                this.path = path;
                return true;
            }
            else { return false; }            
        }
    }
}
