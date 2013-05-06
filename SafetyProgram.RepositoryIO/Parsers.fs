module SafetyProgram.RepositoryIO.ReposIO

    open SafetyProgram.RepositoryIO.IO
    open System.Xml
    open System.Xml.Linq
    open SafetyProgram.Static
    open SafetyProgram.Base.Interfaces
    open System.IO

    let ReposXmlParser<'T when 'T :> IStorable> (repos : XDocument) (nodeName : string) (ctor : unit->'T) (callback : 'T->unit) = 
        //Parses an Xml repository with the following information
        //  nodeName: The name of the Xml node that contains the data to be parsed
        //  ctor: A constructor that makes the relevant object
        //  callback: A callback that is triggered when the object is made & parsed
        let reposElement = repos.Element(XName.Get XmlNodeNames.Repository)

        if reposElement <> null then
            reposElement.Elements(XName.Get nodeName)
            |> Seq.map(fun element ->
                let model = ctor()
                model.LoadData(element)
                callback(model)
                model
            )
        else raise(new InvalidDataException("No repository root node was found in the supplied XDocument"))

    let ReposXmlParserNoCallback<'T when 'T :> IStorable> (repos : XDocument) (nodeName : string) (ctor : unit->'T) =
        //Parses an Xml repository using ReposXmlParser
        //  Supplies a blank callback (fun model->()) to use it
        ReposXmlParser repos nodeName ctor (fun model->())

    
