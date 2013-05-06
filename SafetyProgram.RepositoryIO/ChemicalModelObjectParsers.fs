module SafetyProgram.RepositoryIO.ChemicalModelObjectParsers
    open System.Xml
    open System.Xml.Linq
    open System.Collections.Generic
    open SafetyProgram.ModelObjects
    open SafetyProgram.Configuration
    open SafetyProgram.RepositoryIO.ReposIO
    open SafetyProgram.Static

    let IChemModelObjParserr (callback : IChemicalModelObject->unit) (repos: XDocument)  : IEnumerable<IChemicalModelObject> = 
        ReposXmlParser (repos) (XmlNodeNames.ChemicalModelObj) (fun () -> new ChemicalModelObject() :> IChemicalModelObject) (callback)

    let IChemModelObjParser (repos : XDocument) : IEnumerable<IChemicalModelObject> = 
        IChemModelObjParserr (fun model->()) (repos)

    let GetIChemModelObjs (repos : IRepositoryInfo) : IEnumerable<IChemicalModelObject> = 
        IO.Get(repos, IChemModelObjParser)

    let FetchIChemModelObjs (repos : IRepositoryInfo, callback : IChemicalModelObject->unit) = 
        IO.Fetch(repos, IChemModelObjParserr callback, fun model -> ())