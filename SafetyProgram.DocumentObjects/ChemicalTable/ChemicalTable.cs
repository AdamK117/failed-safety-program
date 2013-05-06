﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Xml.Linq;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.DocumentObjects.ChemicalTable.Commands;
using SafetyProgram.DocumentObjects.ChemicalTable.ContextMenus;
using SafetyProgram.DocumentObjects.ChemicalTable.Ribbon;
using SafetyProgram.ModelObjects;
using SafetyProgram.Static;

namespace SafetyProgram.DocumentObjects.ChemicalTable
{
    public sealed class ChemicalTable : DocumentObject
    {
        private readonly IChemicalTableCommands commands;
        private readonly IContextMenu contextMenu;
        private readonly IRibbonTabItem contextualTab;
        private readonly UserControl view;

        private string header;
        private readonly ObservableCollection<ICoshhChemicalObject> chemicals;
        private readonly ObservableCollection<ICoshhChemicalObject> selectedChemicals;

        /// <summary>
        /// Constructs a ChemicalTable DocObject containing no data (blank table).
        /// </summary>
        /// <param name="parent">The document in which the chemical table resides.</param>
        public ChemicalTable()
        {
            chemicals = new ObservableCollection<ICoshhChemicalObject>();            
            header = "Chemical Table";

            selectedChemicals = new ObservableCollection<ICoshhChemicalObject>();

            commands = new ChemicalTableCommands(this);
            contextMenu = new ChemicalTableContextMenu(this);
            contextualTab = new ChemicalTableRibbonTab(this);

            view = new ChemicalTableView(this);
            view.InputBindings.AddRange(commands.Hotkeys);
        }

        public ChemicalTable(ObservableCollection<ICoshhChemicalObject> chemicals, string header)
        {
            this.chemicals = chemicals;
            this.header = header;

            selectedChemicals = new ObservableCollection<ICoshhChemicalObject>();
            commands = new ChemicalTableCommands(this);
            contextMenu = new ChemicalTableContextMenu(this);
            contextualTab = new ChemicalTableRibbonTab(this);

            view = new ChemicalTableView(this);
            view.InputBindings.AddRange(commands.Hotkeys);
        }

        /// <summary>
        /// Gets the ChemicalTable UserControl
        /// </summary>
        public override Control View
        {
            get { return view; }
        }

        /// <summary>
        /// Gets the ribbon assosciated with a ChemicalTable.
        /// </summary>
        public override IRibbonTabItem RibbonTab
        {
            get { return contextualTab; }
        }

        /// <summary>
        /// Gets the ChemicalTable's context menu.
        /// </summary>
        public override IContextMenu ContextMenu
        {
            get 
            { 
                return contextMenu; 
            }
        }

    #region ChemicalTable specific

        /// <summary>
        /// Gets the commands available to the ChemicalTable.
        /// </summary>
        public IChemicalTableCommands Commands
        {
            get { return commands; }
        }

        /// <summary>
        /// Gets the header for the chemicalTable
        /// </summary>
        public string Header
        {
            get { return header; }
            set
            {
                header = value;
                RaisePropertyChanged("Header");
            }
        }

        /// <summary>
        /// Gets the chemicals in the ChemicalTable
        /// </summary>
        public ObservableCollection<ICoshhChemicalObject> Chemicals
        {
            get { return chemicals; }
        }

        /// <summary>
        /// Gets/Sets the chemical selected in the table.
        /// </summary>
        public ObservableCollection<ICoshhChemicalObject> SelectedChemicals
        {
            get { return selectedChemicals; }
        }

    #endregion

    #region ISelectable implementation

        /// <summary>
        /// Deselects the chemicals in the ChemicalTable on top of base implementation.
        /// </summary>
        public override void DeSelect()
        {
            base.DeSelect();
            SelectedChemicals.Clear();
        }

    #endregion

    #region IStorable Implementation

        /// <summary>
        /// Saves the ChemicalTable to an XElement
        /// </summary>
        /// <returns>The ChemicalTable's data in an XElement</returns>
        public override XElement WriteToXElement()
        {
            return (
                new XElement(XmlNodeNames.ChemicalTableObj,
                    new XElement("header", Header),
                    chemicals.Count > 0 ? 
                        from chemical in chemicals
                        select chemical.WriteToXElement()
                    :
                        null
                )
            );
        }

        /// <summary>
        /// Loads chemicals from an XElement into the ChemicalTable
        /// </summary>
        /// <param name="data">ChemicalTable data in XElement format</param>
        public override IDocumentObject LoadFromXml(XElement data)
        {
            string loadedHeader = "";
            ObservableCollection<ICoshhChemicalObject> loadedChemicals = new ObservableCollection<ICoshhChemicalObject>();            

            var headerElement = data.Element("header");
            if (headerElement != null)
            {
                loadedHeader = headerElement.Value;
            }

            var coshhChemicalsElements = data.Elements(XmlNodeNames.CoshhChemicalObj);
            Func<XElement, ICoshhChemicalObject> chemicalCtor = new CoshhChemicalObject().LoadFromXml;

            foreach (XElement coshhChemicalElement in coshhChemicalsElements)
            {
                var chemicalObject = chemicalCtor(coshhChemicalElement);
                loadedChemicals.Add(chemicalObject);
            }

            return new ChemicalTable(loadedChemicals, loadedHeader);
        }

        public override string Identifier { get { return XmlNodeNames.ChemicalTableObj; } }

        public override string Error
        {
            get { throw new System.NotImplementedException(); }
        }

        public override string this[string columnName]
        {
            get { throw new System.NotImplementedException(); }
        }

    #endregion
    }
}