module SafetyProgram.RepositoryIO.FactoryMatcher
    open SafetyProgram.Configuration
    open SafetyProgram.ModelObjects
    open SafetyProgram.Static
    open SafetyProgram.Base.Interfaces
    open System
    open System.IO
    open System.Collections.Generic
    open System.Xml
    open System.Xml.Linq

    //Gets a factory associated with an identity string and IO type (local, etc.)
    let GetFactory (identifier : string, typeIdentifier : string) = 
        match identifier with
            | ChemicalModelObject.COM_IDENTITY -> 
                match typeIdentifier with
                    | "local" -> new ChemicalModelObjectLocalFileFactory()
                    | _ -> raise(new InvalidDataException("Unknown repository source: e.g. 'local' or 'database'"))
            | _-> raise(new InvalidDataException("Unknown identifier"))

    //Retrieves an identity string associated with a type
    let GetIdent (aType) = 
        match aType with
            | t when t = typeof<ICoshhChemicalObject> -> XmlNodeNames.COSHH_CHEMICAL_MODEL_OBJ
            | t when t = typeof<IChemicalModelObject> -> XmlNodeNames.CHEMICAL_MODEL_OBJ
            | t when t = typeof<IHazardModelObject> -> XmlNodeNames.HAZARD_MODEL_OBJ
            | _ -> raise(new InvalidDataException("Unknown object type"))