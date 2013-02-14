using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using SafetyProgram.Models.DataModels;
using SafetyProgram.FSharp;


namespace SafetyProgram.Data.ChemicalData
{
    public class LoadedChemicals : IModelsHolder
    {
        private List<LoadedChemical> chemicals;
        private string path = "";

        public LoadedChemicals()
        {
            Load("C:\\5items.xml");
        }

        public bool Search(string searchString)
        {
            return Load(path, searchString);
        }

        public bool Refresh()
        {
            return Load(path);
        }

        public bool Load(string path, string search = "")
        {
            CoshhXmlReader.XmlParser parser = new CoshhXmlReader.XmlParser();
            chemicals = new List<LoadedChemical>();

            chemicals = parser.GetChemicalModels(path).ToList().ConvertAll<LoadedChemical>(x => new LoadedChemical(x));
            Test123 = parser.GetChemicalModels(path).ToList();
            return true;
        }        

        public bool Delete(object model)
        {
            return chemicals.Remove(model as LoadedChemical);
        }

        public List<ChemicalModel> Test123 { get; set; }

        public List<IModelHolder> models()
        {
            return chemicals.Cast<IModelHolder>().ToList();
        }
    }
}
