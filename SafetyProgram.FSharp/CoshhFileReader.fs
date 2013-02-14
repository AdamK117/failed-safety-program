module SafetyProgram.FSharp.CoshhXmlReader
    
    #if INTERACTIVE
    #r @"U:\SafetyProgram\SafetyProgram.Models\bin\Debug\SafetyProgram.Models.dll"
    #endif

    open SafetyProgram.Models.DataModels
    open System.Xml

    type public XmlParser() = 

        let loadXml(path) = 
            let xDoc = new XmlDocument()
            xDoc.Load(path : string)
            xDoc

        let findNode(xDoc : XmlDocument, nodeName) =
            xDoc.SelectNodes(nodeName)
                |> Seq.cast<XmlNode>

        let getAttribute(node : XmlNode, value : string) =
            if node.Attributes.ItemOf(value) <> null then node.Attributes.ItemOf(value).ToString()
            else ""

        let getInnerText(node : XmlNode, value) = 
            if node.SelectSingleNode(value) <> null then node.SelectSingleNode(value).InnerText
            else ""

        let fileNodeParse(path, nodeName, nodeParser) = 
            findNode(loadXml(path), nodeName)
                |> Seq.map(nodeParser)

        let xmlDocumentNodeParse(xDoc : XmlDocument, nodeName, nodeParser) = 
            findNode(xDoc, nodeName)
                |> Seq.map(nodeParser)

        //Hazard Models parser
        member private this.hazardParser (hazardNode : XmlNode) = 
            let hazardModel = new HazardModel()
            hazardModel.Hazard <- hazardNode.InnerText
            hazardModel.SignalWord <- getAttribute(hazardNode, "signalword")
            hazardModel.Symbol <- getAttribute(hazardNode, "symbol")
            hazardModel
        
        //Hazard list parser
        member private this.hazardsListParser (hazardsNode : XmlNode) =
            let hazList = new System.Collections.ObjectModel.ObservableCollection<HazardModel>()
            hazardsNode.SelectNodes("hazard")
                |> Seq.cast<XmlNode>
                |> Seq.map(this.hazardParser)
                |> Seq.iter(fun haz -> hazList.Add(haz))
            hazList

        //Base Models parser
        member private this.baseModelParser (xmlData : XmlNode, ?model : BaseElementModel) =
            let model = defaultArg model (new BaseElementModel())

            model.Name <- getInnerText(xmlData, "name")
            model.Hazards <- this.hazardsListParser(xmlData.SelectSingleNode("hazards"))
            model
        
        //Chemical Models parser
        member private this.chemicalModelParser (xmlNode : XmlNode, ?model : ChemicalModel) = 
            let model = defaultArg model (new ChemicalModel())

            this.baseModelParser(xmlNode, model) :?> ChemicalModel
        
        //Coshh Chemical Models parser
        member private this.coshhchemicalModelParser (xmlNode : XmlNode, ?model : CoshhChemicalModel) =
            let model = defaultArg model (new CoshhChemicalModel())

            let model = this.chemicalModelParser(xmlNode, model) :?> CoshhChemicalModel
            model.Unit <- xmlNode.SelectSingleNode("amount").SelectSingleNode("unit").InnerText
            model
        
        //Apparatus Models parser
        member private this.apparatusModelParser (xmlNode : XmlNode, ?model : ApparatusModel) = 
            let model = defaultArg model (new ApparatusModel())

            this.baseModelParser(xmlNode, model) :?> ApparatusModel

        //Coshh Apparatus Models parser
        member private this.coshhapparatusModelParser (xmlNode : XmlNode, ?model : CoshhApparatusModel) =
            let model = defaultArg model (new CoshhApparatusModel())

            let model = this.apparatusModelParser(xmlNode, model) :?> CoshhApparatusModel
            model.UsageComments <- getInnerText(xmlNode, "usagecomments")
            model
        
        //Process Models parser
        member private this.processModelParser (xmlNode : XmlNode, ?model : ProcessModel) =
            let model = defaultArg model (new ProcessModel())

            this.baseModelParser(xmlNode, model) :?> ProcessModel
        
        //Coshh Process Models parser
        member private this.coshhprocessModelParser (xmlNode : XmlNode, ?model : CoshhProcessModel) =
            let model = defaultArg model (new CoshhProcessModel())

            let model = this.processModelParser(xmlNode, model) :?> CoshhProcessModel
            model.UsageComments <- getInnerText(xmlNode, "usagecomments")
            model
        
        //Exposed members
        member this.GetCoshhChemicalModels(xmlDocument : XmlDocument) =
            xmlDocumentNodeParse(xmlDocument, "/coshh/chemicals/chemical", this.coshhchemicalModelParser)

        member this.GetCoshhChemicalModels(path : string) =
            fileNodeParse(path, "/coshh/chemicals/chemical", this.coshhchemicalModelParser)

        member this.GetChemicalModels(xmlDocument : XmlDocument) = 
            xmlDocumentNodeParse(xmlDocument, "//chemical", this.chemicalModelParser)

        member this.GetChemicalModels(path : string) = 
            fileNodeParse(path, "//chemical", this.chemicalModelParser)

        member this.GetCoshhApparatusModels(xmlDocument : XmlDocument) = 
            xmlDocumentNodeParse(xmlDocument, "/coshh/apparatuses/apparatus", this.coshhapparatusModelParser)

        member this.GetCoshhApparatusModels(path : string) = 
            fileNodeParse(path, "/coshh/apparatuses/apparatus", this.coshhapparatusModelParser)

        member this.GetApparatusModels(xmlDocument : XmlDocument) =
            xmlDocumentNodeParse(xmlDocument, "//apparatus", this.apparatusModelParser)

        member this.GetApparatusModels(path : string) =
            fileNodeParse(path, "//apparatus", this.apparatusModelParser)
        
        member this.GetCoshhProcessModels(xmlDocument : XmlDocument) =
            xmlDocumentNodeParse(xmlDocument, "/coshh/processes/process", this.coshhprocessModelParser)

        member this.GetCoshhProcessModels(path : string) =
            fileNodeParse(path, "/coshh/processes/process", this.coshhprocessModelParser)

        member this.GetProcessModels(xmlDocument : XmlDocument) = 
            xmlDocumentNodeParse(xmlDocument, "//process", this.processModelParser)

        member this.GetProcessModels(path : string) = 
            fileNodeParse(path, "//process", this.processModelParser)
