module SafetyProgram.RepositoryIO.ConfigFileIO
    open SafetyProgram.Configuration
    open SafetyProgram.ModelObjects
    open SafetyProgram.Static
    open SafetyProgram.Base.Interfaces
    open System
    open System.IO
    open System.Collections.Generic
    open System.Xml
    open System.Xml.Linq
    open SafetyProgram.RepositoryIO.FactoryMatcher

    let LoadRepository<'T> (callback : 'T->unit) (repository : IRepositoryInfo) = 
        

    let Retrieve<'T> (configFile : IConfiguration) = 
        let reqContentType = GetIdent typedefof<'T>
        configFile.RepositoriesInfo
        //Filter only to the correct type of repository information (chemicals, apparatus, hazards, etc.)
        |> Seq.filter(fun repositoryInfo -> repositoryInfo.ContentType = reqContentType)



