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
        public bool Save(CoshhFileData data)
        {
            if (String.IsNullOrWhiteSpace(path)) { return false; }

            XElement titlexml = new XElement("title", data.Title);

            XElement chemicalxml = chemicalXml(data.Chemicals);
            XElement apparatusesxml = apparatusesXml(data.Apparatuses);
            XElement processesxml = processesXml(data.Processes);

            XElement additionalCommentsXml = new XElement("additionalcomments", data.AdditionalComments);

            XElement completedxml = new XElement("coshh", titlexml, chemicalxml, apparatusesxml, processesxml, additionalCommentsXml);
            completedxml.Save(path);

            return true;
        }

        public bool Load(CoshhFileData data)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "XML Files (.xml)|*.xml";
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

            coshhData.Title = xdoc.SelectSingleNode("/coshh/title/text()") == null ? coshhData.Title : xdoc.SelectSingleNode("/coshh/title/text()").InnerText;

            CoshhXmlReader.XmlParser parser = new CoshhXmlReader.XmlParser();

            createCollection<CoshhChemicalModel>
                (
                    parser.GetCoshhChemicalModels(xdoc),
                    coshhData.Chemicals
                );

            coshhData.DocObject.Add
                (
                    new ChemicalTableIDocUserControl
                        (
                            coshhData.DocObject,
                            createCoshhDocDataObjectCollection<CoshhChemicalModel>(parser.GetCoshhChemicalModels(xdoc))
                        )
                );

            createCollection<CoshhApparatusModel>
                (
                    parser.GetCoshhApparatusModels(xdoc),
                    coshhData.Apparatuses
                );

            createCollection<CoshhProcessModel>
                (
                    parser.GetCoshhProcessModels(xdoc),
                    coshhData.Processes
                );

            coshhData.AdditionalComments = xdoc.SelectSingleNode("/coshh/additionalcomments/text()") == null ? coshhData.AdditionalComments : xdoc.SelectSingleNode("/coshh/additionalcomments/text()").InnerText;

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

        private void createCollection<T>(IEnumerable<T> data, ObservableCollection<CoshhDocDataObject<T>> parent)
        {            
            foreach (T model in data)
            {
                parent.Add(new CoshhDocDataObject<T>(parent, model));
            }
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
