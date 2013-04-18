using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Xml.Linq;

using SafetyProgram.DocObjects.ChemicalTable.Commands;
using SafetyProgram.DocObjects.ChemicalTable.ContextMenus;
using SafetyProgram.DocObjects.ChemicalTable.Ribbon;

using SafetyProgram.DocObjects.ServiceHelpers;
using SafetyProgram.Models.DataModels;
using SafetyProgram.BaseClasses;

namespace SafetyProgram.DocObjects.ChemicalTable
{
    public class ChemicalTable : DocObject
    {
        private readonly ObservableCollection<CoshhChemicalModel> chemicals;
        private readonly ChemicalTableCommandsHolder commands;
        private readonly IContextMenu contextMenu;
        private readonly IRibbonTabItem ribbon;
        private readonly UserControl view;
        private readonly ObservableCollection<CoshhChemicalModel> selectedChemicals;
        private readonly ChemicalTableComHelper comHelper;

        /// <summary>
        /// Constructs a ChemicalTable DocObject containing no data (blank table).
        /// </summary>
        /// <param name="parent">The document in which the chemical table resides.</param>
        public ChemicalTable()
        {
            chemicals = new ObservableCollection<CoshhChemicalModel>();
            selectedChemicals = new ObservableCollection<CoshhChemicalModel>();

            commands = new ChemicalTableCommandsHolder(this);
            contextMenu = new ChemicalTableContextMenu(this);
            ribbon = new ChemicalTableRibbonTab(this);

            comHelper = new ChemicalTableComHelper("CoshhChemicalModels");

            view = new ChemicalTableView(this);
            view.InputBindings.AddRange(commands.Hotkeys);
        }

        /// <summary>
        /// Gets the ChemicalTable UserControl
        /// </summary>
        public override UserControl View
        {
            get
            { 
                return view; 
            }
        }

        /// <summary>
        /// Gets the ribbon assosciated with a ChemicalTable.
        /// </summary>
        public override IRibbonTabItem RibbonTab
        {
            get
            {
                return ribbon; 
            }
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

        /// <summary>
        /// Gets the commands available to the ChemicalTable.
        /// </summary>
        public ChemicalTableCommandsHolder Commands
        {
            get 
            { 
                return commands; 
            }
        }

        /// <summary>
        /// Gets a Com Helper for the chemical table.
        /// </summary>
        public ChemicalTableComHelper ComHelper
        {
            get
            {
                return comHelper;
            }
        }

        /// <summary>
        /// Gets the chemicals in the ChemicalTable
        /// </summary>
        public ObservableCollection<CoshhChemicalModel> Chemicals
        {
            get 
            { 
                return chemicals; 
            }
        }

        /// <summary>
        /// Gets/Sets the chemical selected in the table.
        /// </summary>
        public ObservableCollection<CoshhChemicalModel> SelectedChemicals
        {
            get 
            { 
                return selectedChemicals; 
            }
        }

        /// <summary>
        /// Deselects the chemicals in the ChemicalTable on top of base implementation.
        /// </summary>
        public override void DeSelect()
        {
            base.DeSelect();
            SelectedChemicals.Clear();
        }

        /// <summary>
        /// Saves the ChemicalTable to an XElement
        /// </summary>
        /// <returns>The ChemicalTable's data in an XElement</returns>
        public override XElement Save()
        {
            return (
                new XElement("chemicals",
                    from chemical in chemicals
                    select new XElement("chemical",
                        new XElement("name", chemical.Name),
                        new XElement("amount",
                            new XElement("value", chemical.Value),
                            new XElement("unit", chemical.Unit)
                        ),
                        Hazards.WriteXElement(chemical.Hazards)
                    )
                )
            );
        }

        /// <summary>
        /// Loads chemicals from an XElement into the ChemicalTable
        /// </summary>
        /// <param name="data">ChemicalTable data in XElement format</param>
        public override void Load(XElement data)
        {
            chemicals.Concat(
                from XElement chemical in data.Elements("chemical")
                select new CoshhChemicalModel()
                {
                    Name = chemical.Element("name").Value,
                    Value = float.Parse(chemical.Element("amount").Element("value").Value),
                    Unit = chemical.Element("amount").Element("unit").Value,
                    Hazards = new ObservableCollection<HazardModel>
                        (
                            Hazards.ReadXElement(chemical.Element("hazards"))
                        )
                }
            );
        }
    }
}