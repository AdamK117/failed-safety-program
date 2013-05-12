using SafetyProgram.Base.Interfaces;
using SafetyProgram.DocumentObjects.ChemicalTableNs;

namespace SafetyProgram.DocumentObjects
{
    public static class DefaultDocumentObjects
    {
        public static IDocumentObject ChemicalTable(IConfiguration appConfiguration)
        {
            return ChemicalTableDefaults.DefaultTable(appConfiguration);
        }
    }
}
