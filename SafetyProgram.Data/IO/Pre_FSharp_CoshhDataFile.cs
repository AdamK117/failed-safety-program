using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Linq;
using System.Windows.Forms;
using SafetyProgram.FSharp;

using SafetyProgram.Models.DataModels;
using System.Collections.ObjectModel;

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

        private List<CoshhChemicalModel> parseChemicalsXml(XDocument xdoc)
        {
            List<CoshhChemicalModel> chemicals = new List<CoshhChemicalModel>();

            chemicals = (from chemical in xdoc.Descendants("chemicals").Elements("chemical")
                         select new CoshhChemicalModel
                         {
                             Name = (chemical.Element("name") == null ? null : chemical.Element("name").Value),
                             Hazards = parseHazardXml(chemical),
                             Value = float.Parse(chemical.Element("amount").Element("value").Value),
                             Unit = chemical.Element("amount").Element("unit").Value,
                         }).ToList<CoshhChemicalModel>();

            return chemicals;
        }
        private List<CoshhApparatusModel> parseApparatusXml(XDocument xdoc)
        {
            List<CoshhApparatusModel> apparatuses = new List<CoshhApparatusModel>();
            apparatuses = (from apparatus in xdoc.Descendants("apparatuses").Elements("apparatus")
                           select new CoshhApparatusModel
                           {
                               Name = apparatus.Element("name").Value,
                               Hazards = parseHazardXml(apparatus)
                           }).ToList<CoshhApparatusModel>();
            return apparatuses;
        }
        private List<CoshhProcessModel> parseProcessXml(XDocument xdoc)
        {
            List<CoshhProcessModel> processes = new List<CoshhProcessModel>();

            processes = (from process in xdoc.Descendants("processes").Elements("process")
                         select new CoshhProcessModel
                         {
                             Name = process.Element("name").Value,
                             Hazards = parseHazardXml(process)
                         }).ToList<CoshhProcessModel>();
            return processes;
        }
        private List<HazardModel> parseHazardXml(XElement xelement)
        {
            List<HazardModel> Hazards = new List<HazardModel>();
            Hazards = (from hazard in xelement.Element("hazards").Elements("hazard")
                       select new HazardModel
                       {
                           Hazard = hazard.Value,
                           SignalWord = hazard.Attribute("signalword") == null ? null : hazard.Attribute("signalword").Value,
                           Symbol = hazard.Attribute("symbol") == null ? null : hazard.Attribute("symbol").Value
                       }).ToList<HazardModel>();
            return Hazards;

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
            XDocument xdoc = new XDocument();
            xdoc = XDocument.Load(path);

            Title = (xdoc.Element("coshh").Element("title") == null ? Title : xdoc.Element("coshh").Element("title").Value);

            CoshhXmlReader.XmlParser parser = new CoshhXmlReader.XmlParser();

            Chemicals = parser.GetCoshhChemicalModels(path).ToList();
            Apparatuses = parser.GetCoshhApparatusModels(path).ToList();
            Processes = parser.GetCoshhProcessModels(path).ToList();

            //Chemicals = parseChemicalsXml(xdoc);
            //Apparatuses = parseApparatusXml(xdoc);
            //Processes = parseProcessXml(xdoc);

            AdditionalComments = xdoc.Element("coshh").Element("additionalcomments") == null ? AdditionalComments : xdoc.Element("coshh").Element("additionalcomments").Value;

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
