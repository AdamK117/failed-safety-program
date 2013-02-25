module CoshhFileWriter

    #if INTERACTIVE
    #r @"V:\SafetyProgram\SafetyProgram.Models\bin\Debug\SafetyProgram.Models.dll"
    #endif

    open SafetyProgram.Models.DataModels
    open System.Xml

    type public XmlWrite() = 

        member private this.hazardWriter(hazardmodel : HazardModel, xmlWriter : XmlWriter) = 
            xmlWriter.WriteStartElement("hazard")
            xmlWriter.WriteAttributeString("signalWord", hazardmodel.SignalWord)  
            xmlWriter.WriteAttributeString("symbol", hazardmodel.Symbol) 
            xmlWriter.WriteString(hazardmodel.Hazard)         
            xmlWriter.WriteEndElement()
            xmlWriter

        member private this.coshhChemicalWriter(model : CoshhChemicalModel, xmlWriter : XmlWriter) = 
            xmlWriter.WriteStartElement("chemical")
            xmlWriter.WriteElementString("name", model.Name)
            xmlWriter.WriteStartElement("amount")
            xmlWriter.WriteElementString("value", model.Value.ToString())
            xmlWriter.WriteElementString("unit", model.Unit)
            xmlWriter.WriteEndElement()
            xmlWriter.WriteStartElement("hazards")
            model.Hazards |> Seq.map(fun x -> this.hazardWriter(x, xmlWriter))

        member private this.hazardsWriter(hazards, xmlWriter) = 
            hazards |> Seq.fold(fun acc x -> this.hazardWriter(x, acc))

        member public this.docWrite(path : string) = 
            use xmlWriter = path |> XmlWriter.Create
            xmlWriter.WriteStartDocument()
            this.hazardWriter(new HazardModel(), xmlWriter)

    let xw = new XmlWrite()
    let hm = new HazardModel()

