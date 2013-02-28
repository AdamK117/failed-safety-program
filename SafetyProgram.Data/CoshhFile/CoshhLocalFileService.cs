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

        private XElement chemicalXml(IEnumerable<CoshhDocDataObject<CoshhChemicalModel>> data)
        {
            XElement chemicalxml = new XElement("chemicals", "No Chemicals");
            if (data == null) { return chemicalxml; }

            chemicalxml = new XElement("chemicals",
                from c in data
                select
                    new XElement("chemical",
                    new XElement("name", c.Data().Name),
                    new XElement("amount",
                        new XElement("value", c.Data().Value),
                        new XElement("unit", c.Data().Unit)
                    ),
                    hazardXml(c.Data().Hazards)
                )
            );

            return chemicalxml;
        }
        private XElement apparatusesXml(IEnumerable<CoshhDocDataObject<CoshhApparatusModel>> data)
        {
            XElement apparatusesxml = new XElement("apparatuses", "No Apparatuses");
            if (data == null) { return apparatusesxml; }

            apparatusesxml = new XElement("apparatuses",
                from c in data
                select
                    new XElement("apparatus",
                        new XElement("name", c.Data().Name),
                        hazardXml(c.Data().Hazards)
                    )
                );

            return apparatusesxml;
        }
        private XElement processesXml(IEnumerable<CoshhDocDataObject<CoshhProcessModel>> data)
        {
            XElement processesxml = new XElement("processes", "No Processes");
            if (data == null) { return processesxml; }

            processesxml = new XElement("processes",
                from c in data
                select
                    new XElement("process",
                        new XElement("name", c.Data().Name),
                        hazardXml(c.Data().Hazards)
                    )
            );

            return processesxml;
        }
        private XElement hazardXml(IEnumerable<HazardModel> hazards)
        {
            XElement hazardxml = new XElement("hazards", "");
            if (hazards == null) { return hazardxml; }

            hazardxml = new XElement("hazards",
                from hazard in hazards
                select
                    new XElement("hazard", hazard.Hazard)
            );

            return hazardxml;
        }

        private void serializeTest()
        {
            XmlSerializer a = new XmlSerializer(typeof(CoshhChemicalModel));
            TextWriter b = new StreamWriter(@"C:\serializethis.xml");
            a.Serialize(b, new CoshhChemicalModel());
            b.Close();
        }

        public bool Save(CoshhFileData data)
        {
            if (String.IsNullOrWhiteSpace(path)) { return false; }

            ICollection<XElement> docXml = new List<XElement>();
            foreach (IDocUserControl uc in data.DocObject)
            {
                if (uc is ChemicalTableIDocUserControl)
                {
                    docXml.Add(chemicalTableXmlGenerator(uc as ChemicalTableIDocUserControl));
                }
            }

            XElement completedxml = new XElement
                (
                    "coshh",
                    from c in docXml
                    select
                        c
                );
            completedxml.Save(path);

            return true;
        }

        private XElement chemicalTableXmlGenerator(ChemicalTableIDocUserControl iDoc)
        {
            return chemicalXml(iDoc.Data() as IEnumerable<CoshhDocDataObject<CoshhChemicalModel>>);
        }

        public bool Load(CoshhFileData data)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Coshh Documents (.xml)|*.xml";
            openFileDialog1.Multiselect = false;

            DialogResult dialogResult = openFileDialog1.ShowDialog();

            if (dialogResult != DialogResult.OK) { return false; }
            else if (!setPath(openFileDialog1.FileName)) { return false; }
            else if (!load(Path(), data)) { return false; }
            return true;
        }

        private bool load(string path, CoshhFileData coshhData)
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
            this.path = path;
            return true;
        }
    }
}
