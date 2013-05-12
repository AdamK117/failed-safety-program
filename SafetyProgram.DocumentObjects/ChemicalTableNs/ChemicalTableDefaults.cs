using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.ModelObjects;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs
{
    public static class ChemicalTableDefaults
    {
        public static ChemicalTable DefaultTable(IConfiguration appConfiguration)
        {
            return new ChemicalTable(
                appConfiguration,
                DefaultChemicals(),
                DefaultHeader(),
                DefaultView()
                );
        }

        public static ObservableCollection<ICoshhChemicalObject> DefaultChemicals()
        {
            return new ObservableCollection<ICoshhChemicalObject>();
        }

        public static string DefaultHeader()
        {
            return "Chemical Table";
        }

        public static Func<ChemicalTable, UserControl> DefaultView()
        {
            return (chemTable) => new ChemicalTableView(chemTable);
        }
    }
}
