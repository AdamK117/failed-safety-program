module SafetyProgram.RepositoryIO.IO

    open SafetyProgram.ModelObjects
    open SafetyProgram.Base.Interfaces
    open SafetyProgram.Configuration
    open System.Xml.Linq
    open System.Xml
    open System.Linq
    open System.IO
    open System
    open System.Collections.Generic

    let LoadXmlRepos (repos : IRepositoryInfo) (parser) =
        if File.Exists(repos.Path) then
            XDocument.Load(repos.Path) 
            |> parser
        else raise(InvalidDataException("The specified repository path does not exist"))

    let GetSerializationMethod (repos : IRepositoryInfo) = 
        match repos.Source with
            | "local" -> LoadXmlRepos
            | _ -> raise(InvalidDataException("Unknown source type for a repository found: " + repos.Source))

    let Get<'T> (repos : IRepositoryInfo, parser) : IEnumerable<'T> = 
        let loaderMethod = GetSerializationMethod(repos)
        loaderMethod repos parser

    let GetAll<'T> (config : IConfiguration, parser) = 
        config.Repositories
        |> Seq.map(fun repos -> Get<'T>(repos, parser))

    let Fetch<'T> (repos : IRepositoryInfo, parser, callback : 'T->unit) : unit = 
        let loaderMethod = GetSerializationMethod(repos)
        loaderMethod repos parser
        |> callback

    let FetchAll<'T> (config : IConfiguration, parser, callback : 'T->unit) : unit = 
        config.Repositories
        |> Seq.iter(fun repos -> Fetch<'T>(repos, parser, callback))