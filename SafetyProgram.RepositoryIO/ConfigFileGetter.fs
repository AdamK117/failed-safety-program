module SafetyProgram.RepositoryIO.ConfigFileGetter
    open SafetyProgram.Configuration
    open SafetyProgram.ModelObjects
    open SafetyProgram.Static
    open SafetyProgram.Base.Interfaces
    open System
    open System.IO
    open System.Collections.Generic
    open System.Xml
    open System.Xml.Linq

    //Parses an Xml repository file. Serializes the IStorables data within it into objects.
    let XmlParser<'T when 'T :> IStorable<'T>>(rep : IRepositoryInfo) (callback : 'T->unit) (ctor : unit->'T) = 
        if File.Exists(rep.Path) then
            XDocument.Load(rep.Path)
                .Element(XName.Get XmlNodeNames.REPOSITORY)
                .Elements(XName.Get(ctor().Identifier))
                |> Seq.map(
                    fun xmlElement -> 
                        let newObj = ctor().LoadFromXml(xmlElement)
                        callback(newObj)
                        newObj
                )
        else raise(InvalidDataException("The path specified for a repositories info does not exist"))

    //Loads a repository containing IStorable objects
    let LoadRepository<'T when 'T :> IStorable<'T>> (callback : 'T->unit) (ctor : unit->'T) (repository : IRepositoryInfo) = 
        match repository.Source with
            | "local" -> XmlParser<'T> repository callback ctor
            | _ -> raise(new InvalidDataException("Unknown repository source: e.g. 'local' or 'database'"))
            
    //Retrieves IStorable objects from repositories contained within a configuration file
    //  Uses the ctor to construct an instance of the IStorable
    //  Calls the callback each time an IStorable is created
    //  Repos type if used to define the type of objects held in the repository
    let Retrieve<'T when 'T :> IStorable<'T>>(callback : 'T->unit) (configFile : IConfiguration) (ctor : unit->'T) = 
        let reqContentType = ctor().Identifier
        configFile.RepositoriesInfo
        //Filter only to the correct type of repository information (chemicals, apparatus, hazards, etc.)
        |> Seq.filter(fun repositoryInfo -> repositoryInfo.ContentType = reqContentType)
        //Get a sequence of commands which load the repositories
        |> Seq.map(fun repositoryInfo -> async { return LoadRepository callback ctor repositoryInfo })
        //Parallelize the commands
        |> Async.Parallel
        //Run the commands, returning a seq<'T> for each command
        |> Async.RunSynchronously
        //Concat the seq<'T> response from each individual command into a total response
        |> Seq.concat

    let RetrieveO<'T when 'T :> IStorable<'T>>(configFile : IConfiguration, ctor : unit->'T) = 
        Retrieve (fun model -> ()) configFile ctor