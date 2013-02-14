using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Linq;
using System.Windows.Forms;
using SafetyProgram.FSharp;

using SafetyProgram.Models.DataModels;
using System.Collections.ObjectModel;
using System.Xml;

namespace SafetyProgram.Data
{
    public class CoshhDataFile : CoshhData
    {
        #region Constructors

        //Ctor for a loaded file
        public CoshhDataFile(string path)
        {
            if (Path(path)) { Load(); }
        }

        //Ctor for a new file
        public CoshhDataFile()
        {
            path = "";
        }

        #endregion

        private string path;
        public string Path() { return path; }
        public bool Path(string path)
        {
            this.path = path;
            if (File.Exists(path))
            {
                return true;
            }
            else { return false; }
        }        

        #region Methods

        private XElement chemicalXml()
        {
            XElement chemicalxml = new XElement("chemicals", "No Chemicals");
            if (this.Chemicals == null) { return chemicalxml; }

            chemicalxml = new XElement("chemicals",
                from c in this.Chemicals
                select
                    new XElement("chemical",
                    new XElement("name", c.Name),
                    new XElement("amount",
                        new XElement("value", c.Value),
                        new XElement("unit", c.Unit)
                    ),
                    hazardXml(c.Hazards)
                )
            );

            return chemicalxml;
        }
        private XElement apparatusesXml()
        {
            XElement apparatusesxml = new XElement("apparatuses", "No Apparatuses");
            if (this.Apparatuses == null) { return apparatusesxml; }

            apparatusesxml = new XElement("apparatuses",
                from c in this.Apparatuses
                select
                    new XElement("apparatus",
                        new XElement("name", c.Name),
                        hazardXml(c.Hazards)
                    )
                );

            return apparatusesxml;
        }
        private XElement processesXml()
        {
            XElement processesxml = new XElement("processes", "No Processes");
            if (this.Processes == null) { return processesxml; }

            processesxml = new XElement("processes",
                from c in this.Processes
                select
                    new XElement("process",
                        new XElement("name", c.Name),
                        hazardXml(c.Hazards)
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
        public override bool Save()
        {
            if (String.IsNullOrWhiteSpace(Path())) { if (!SaveAs()) { return false; } }

            XElement titlexml = new XElement("title", this.Title);

            XElement chemicalxml = chemicalXml();
            XElement apparatusesxml = apparatusesXml();
            XElement processesxml = processesXml();

            XElement additionalCommentsXml = new XElement("additionalcomments", this.AdditionalComments);

            XElement completedxml = new XElement("coshh", titlexml, chemicalxml, apparatusesxml, processesxml, additionalCommentsXml);
            completedxml.Save(Path());

            return true;
        }

        public override bool Load()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "XML Files (.xml)|*.xml";
            openFileDialog1.Multiselect = false;

            DialogResult dialogResult = openFileDialog1.ShowDialog();

            if (dialogResult != DialogResult.OK) { return false; }
            else if (!Path(openFileDialog1.FileName)) { return false; }
            else if (!Load(Path())) { return false; }
            return true;
        }
        public bool Load(string path)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(path);

            Title = xdoc.SelectSingleNode("/coshh/title/text()") == null ? Title : xdoc.SelectSingleNode("/coshh/title/text()").InnerText;

            CoshhXmlReader.XmlParser parser = new CoshhXmlReader.XmlParser();

            Chemicals = parser.GetCoshhChemicalModels(xdoc).ToList();
            Apparatuses = parser.GetCoshhApparatusModels(xdoc).ToList();
            Processes = parser.GetCoshhProcessModels(xdoc).ToList();

            AdditionalComments = xdoc.SelectSingleNode("/coshh/additionalcomments/text()") == null ? AdditionalComments : xdoc.SelectSingleNode("/coshh/additionalcomments/text()").InnerText;

            return true;
        }
        
        public override bool SaveAs()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Coshh Safety Document|*.xml";
            saveFileDialog.Title = "Save As";

            DialogResult userResponse = saveFileDialog.ShowDialog();

            if (userResponse == DialogResult.OK)
            {
                Path(saveFileDialog.FileName);
                Save();
                return true;
            }
            else { return false; }
        }

        public override bool Close()
        {
            return true;
        }

        #endregion
    }
}
