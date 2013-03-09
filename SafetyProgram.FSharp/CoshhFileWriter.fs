module SafetyProgram.FSharp.CoshhXmlWriter

    #if INTERACTIVE
    #r @"V:\SafetyProgram\SafetyProgram.Models\bin\Debug\SafetyProgram.Models.dll"
    #r @"V:\SafetyProgram\SafetyProgram.UserControls\bin\Debug\SafetyProgram.UserControls.dll"
    #endif

    open SafetyProgram.Models.DataModels
    open SafetyProgram.UserControls
    open System.Collections.Generic
    open System.Xml

    type public XmlWrite() = 

        //Hazard Model Writer
        let hazardWriter(xmlWriter : XmlWriter, hazardmodel : HazardModel) = 
            xmlWriter.WriteStartElement("hazard")
            if hazardmodel.SignalWord.Length > 0 then xmlWriter.WriteAttributeString("signalWord", hazardmodel.SignalWord)  
            if hazardmodel.Symbol.Length > 0 then xmlWriter.WriteAttributeString("symbol", hazardmodel.Symbol) 
            if hazardmodel.Hazard <> null then xmlWriter.WriteString(hazardmodel.Hazard)      
            xmlWriter.WriteEndElement()  

        //Hazard Sequence Writer
        let hazardsWriter (xmlWriter : XmlWriter, hazards : seq<HazardModel>) = 
            xmlWriter.WriteStartElement("hazards")
            hazards |> Seq.iter(fun x -> hazardWriter(xmlWriter, x))
            xmlWriter.WriteEndElement()

        //Comments Writer
        let commentsWriter(xmlWriter : XmlWriter) (comments) = 
            if comments <> "" then xmlWriter.WriteElementString("comments", comments)
        
        //Title Writer
        let titleWriter(xmlWriter : XmlWriter) (title) = 
            if title <> "" then xmlWriter.WriteElementString("title", title)

        //CoshhChemicalModel Writer
        let coshhChemicalWriter (xmlWriter : XmlWriter) (model : IDocDataHolder<CoshhChemicalModel>) = 
            if model <> null then
                xmlWriter.WriteStartElement("chemical") //a

                if model.Data().Name <> null then xmlWriter.WriteElementString("name", model.Data().Name)

                xmlWriter.WriteStartElement("amount") //b
                if model.Data().Value.ToString() <> null then xmlWriter.WriteElementString("value", model.Data().Value.ToString())
                if model.Data().Unit <> null then xmlWriter.WriteElementString("unit", model.Data().Unit)
                xmlWriter.WriteEndElement() //b

                if model.Data().Hazards <> null then hazardsWriter(xmlWriter, model.Data().Hazards)

                xmlWriter.WriteEndElement() //a
        
        //CoshhApparatusModel Writer
        let cosshApparatusWriter(xmlWriter : XmlWriter) (model : IDocDataHolder<CoshhApparatusModel>) = 
            if model <> null then
                xmlWriter.WriteStartElement("apparatus") //a

                if model.Data().Name <> null then xmlWriter.WriteElementString("name", model.Data().Name)

                if model.Data().Hazards <> null then hazardsWriter(xmlWriter, model.Data().Hazards)

                if model.Data().UsageComments <> null then xmlWriter.WriteElementString("usagecomments", model.Data().UsageComments)

                xmlWriter.WriteEndElement() //a
        
        //CoshhProcessModelWriter
        let coshhProcessWriter(xmlWriter : XmlWriter) (model : IDocDataHolder<CoshhProcessModel>) =
            if model <> null then
                xmlWriter.WriteStartElement("process") //a

                if model.Data().Name <> null then xmlWriter.WriteElementString("name", model.Data().Name)

                if model.Data().Hazards <> null then hazardsWriter(xmlWriter, model.Data().Hazards)

                if model.Data().UsageComments <> null then xmlWriter.WriteElementString("usagecomments", model.Data().UsageComments)

                xmlWriter.WriteEndElement()
        
        //Model Sequence Writer (Generic)
        let modelSeqHandler(models, nodeName, xmlWriter : XmlWriter, nodeWriter) =
            xmlWriter.WriteStartElement(nodeName) //a
            models |> Seq.iter(nodeWriter(xmlWriter))
            xmlWriter.WriteEndElement() //a
        
        //Coshh Document Writer
        member public this.writeDocument(path : string, iDocs : seq<IDocUserControl>) = 
            let xmlWriterSettings = new XmlWriterSettings()
            xmlWriterSettings.Indent <- true
            use xmlWriter = XmlWriter.Create(path, xmlWriterSettings)

            xmlWriter.WriteStartDocument() //a
            xmlWriter.WriteStartElement("coshh") //b

            iDocs |> Seq.iter(fun x -> this.writeIDocUserControl(path, xmlWriter, x))

            xmlWriter.WriteEndElement() //b
            xmlWriter.WriteEndDocument() //a

            xmlWriter.Flush()
        
        //Document Object Writer
        member public this.writeIDocUserControl(path : string, xmlWriter : XmlWriter, iDoc : IDocUserControl) = 
            match iDoc.Data() with
            | :? IEnumerable<IDocDataHolder<CoshhChemicalModel>> as models -> modelSeqHandler(models, "chemicals", xmlWriter, coshhChemicalWriter)
            | :? IEnumerable<IDocDataHolder<CoshhApparatusModel>> as models -> modelSeqHandler(models, "apparatuses", xmlWriter, cosshApparatusWriter)
            | :? IEnumerable<IDocDataHolder<CoshhProcessModel>> as models -> modelSeqHandler(models, "processes", xmlWriter, coshhProcessWriter)
            | _ -> ()