using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using SafetyProgram.Models.DataModels;
using System.Xml;
using System.Xml.Linq;

namespace SafetyProgram.UserControls.MainWindowControls.ChemicalTable
{
    /// <summary>
    /// Defines the IDocObject for a ChemicalTable.
    /// </summary>
    public class ChemicalTableDocObject : IDocObject
    {
        protected ICollection<IDocObject> parent;
        protected IEnumerable<CoshhChemicalModel> data = new List<CoshhChemicalModel>();

        /// <summary>
        /// Create a new instance of a ChemicalTableDocObject.
        /// </summary>
        /// <param name="parent">An ICollection that holds the documents data (IDocObjects)</param>
        /// <param name="chemicals">The raw data that will be displayed in the ChemicalTable</param>
        public ChemicalTableDocObject(ICollection<IDocObject> parent)
        {
            this.parent = parent;
        }

        /// <summary>
        /// Displays the ChemicalTableView
        /// </summary>
        private UserControl display;
        public UserControl Display()
        {
            display = new ChemicalTableView(data);
            return display;
        }

        public bool canRemove()
        {
            return parent.Contains(this) ? true : false;
        }

        public bool Remove()
        {
            return parent.Remove(this);
        }

        public bool canEdit()
        {
            throw new NotImplementedException();
        }

        public bool Edit()
        {
            throw new NotImplementedException();
        }

        public bool CanSelect()
        {
            throw new NotImplementedException();
        }

        public bool IsSelected()
        {
            throw new NotImplementedException();
        }

        public void Select()
        {
            throw new NotImplementedException();
        }

        public void DeSelect()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves the ChemicalTable as an XElement (to be inserted into an XmlDocument)
        /// </summary>
        /// <returns>XElement containing the ChemicalTable's data.</returns>
        public XElement IOSaveXml()
        {
            return new XElement("chemicalTable",
                from chemical in data
                select new XElement("chemical",
                    new XElement("name", chemical.Name),
                    new XElement("amount",
                        new XElement("value", chemical.Value),
                        new XElement("unit", chemical.Unit)
                    ),
                    new XElement("hazards",
                        from hazard in chemical.Hazards
                        select new XElement("hazard",
                            new XElement(
                                "hazard",
                                new XAttribute("signalWord", hazard.SignalWord),
                                new XAttribute("symbol", hazard.Symbol),
                                hazard.Hazard
                            )
                        )
                    )
                )
            );
        }

        /// <summary>
        /// Loads data into the ChemicalTable from an Xml (XDocument) source.
        /// </summary>
        /// <param name="data">An XDocument containing the relevant ChemicalTable data.</param>
        /// <returns>The ChemicalTableDocObject (now filled with data).</returns>
        public IDocObject IOLoadXml(XDocument data)
        {
            throw new NotImplementedException();
        }
    }
}
