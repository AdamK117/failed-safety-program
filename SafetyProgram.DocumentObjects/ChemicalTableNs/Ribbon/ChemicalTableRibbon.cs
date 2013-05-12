using System.Collections.ObjectModel;
using System.Windows.Controls;
using Fluent;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.DocumentObjects.ChemicalTableNs.Commands;
using SafetyProgram.ModelObjects;
using SafetyProgram.Configuration;
using System.Linq;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs.Ribbon
{
    internal sealed class ChemicalTableRibbonTab : IRibbonTabItem
    {
        private readonly ObservableCollection<ICoshhChemicalObject> chemicals;

        public ChemicalTableRibbonTab(ChemicalTable table)
        {
            commands = table.Commands;
            IService<IRepository<IChemicalModelObject>> repositoryService = new LocalFileService<IRepository<IChemicalModelObject>>(
                new RepositoryLocalFileFactory<IChemicalModelObject>(
                    new ChemicalModelObjectLocalFileFactory()
                    ),
                table.AppConfiguration.RepositoriesInfo.First().Path
                );
            var repos = repositoryService.Load();
            view = new ChemicalTableRibbonView(this);
        }

        private readonly IChemicalTableCommands commands;
        public IChemicalTableCommands Commands
        {
            get
            {
                return commands;
            }
        }

        private readonly ChemicalTableRibbonView view;
        public RibbonTabItem View 
        { 
            get 
            { 
                return view; 
            } 
        }

        Control IViewable.View 
        {
            get 
            { 
                return view; 
            } 
        }
    }
}
