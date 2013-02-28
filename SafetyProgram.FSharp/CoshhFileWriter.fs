module CoshhFileWriter

    #if INTERACTIVE
    #r @"V:\SafetyProgram\SafetyProgram.Models\bin\Debug\SafetyProgram.Models.dll"
    #endif

    open SafetyProgram.Models.DataModels
    open SafetyProgram.UserControls
    open System.Collections.Generic
    open System.Xml

    type public XmlWrite() = 
        member private this.commentsWriter(comments, xmlWriter : XmlWriter) = 
            if comments <> "" then xmlWriter.WriteElementString("comments", comments)

        member private this.titleWriter(title, xmlWriter : XmlWriter) = 
            if title <> "" then xmlWriter.WriteElementString("title", title)

        member private this.cosshApparatusWriter(xmlWriter : XmlWriter, model : CoshhApparatusModel) = 
            if model <> null then
                xmlWriter.WriteStartElement("apparatus") //a

                if model.Name <> null then xmlWriter.WriteElementString("name", model.Name)

                if model.Hazards <> null then this.hazardsWriter(model.Hazards, xmlWriter)

                if model.UsageComments <> null then xmlWriter.WriteElementString("usagecomments", model.UsageComments)

                xmlWriter.WriteEndElement() //a

        member private this.coshhProcessWriter(xmlWriter : XmlWriter, model : CoshhProcessModel) =
            if model <> null then
                xmlWriter.WriteStartElement("process") //a

                if model.Name <> null then xmlWriter.WriteElementString("name", model.Name)

                if model.Hazards <> null then this.hazardsWriter(model.Hazards, xmlWriter)

                if model.UsageComments <> null then xmlWriter.WriteElementString("usagecomments", model.UsageComments)

                xmlWriter.WriteEndElement()

        member private this.coshhChemicalWriter(xmlWriter : XmlWriter, model : CoshhChemicalModel) = 
            if model <> null then
                xmlWriter.WriteStartElement("chemical") //a

                if model.Name <> null then xmlWriter.WriteElementString("name", model.Name)

                xmlWriter.WriteStartElement("amount") //b
                if model.Value.ToString() <> null then xmlWriter.WriteElementString("value", model.Value.ToString())
                if model.Unit <> null then xmlWriter.WriteElementString("unit", model.Unit)
                xmlWriter.WriteEndElement() //b

                if model.Hazards <> null then this.hazardsWriter(model.Hazards, xmlWriter)

                xmlWriter.WriteEndElement() //a

        member private this.hazardWriter(hazardmodel : HazardModel, xmlWriter : XmlWriter) = 
            xmlWriter.WriteStartElement("hazard")
            if hazardmodel.SignalWord <> null then xmlWriter.WriteAttributeString("signalWord", hazardmodel.SignalWord)  
            if hazardmodel.Symbol <> null then xmlWriter.WriteAttributeString("symbol", hazardmodel.Symbol) 
            if hazardmodel.Hazard <> null then xmlWriter.WriteString(hazardmodel.Hazard)      
            xmlWriter.WriteEndElement()

        member private this.hazardsWriter(hazards, xmlWriter) = 
            if hazards.Count <> 0 then
                xmlWriter.WriteStartElement("hazards")
                hazards |> Seq.iter(fun x -> this.hazardWriter(x, xmlWriter))
                xmlWriter.WriteEndElement()        

        member private this.modelSeqHandler(models, func) =
                models |> Seq.iter(func)

        member public this.docWrite(path : string, model : CoshhChemicalModel) = 
            use xmlWriter = path |> XmlWriter.Create
            xmlWriter.WriteStartDocument()
            this.coshhChemicalWriter(xmlWriter, model)
            xmlWriter.WriteEndDocument()
            xmlWriter.Flush()

        member public this.writeIt(path : string, iDoc : IDocUserControl) = 
            use xmlWriter = path |> XmlWriter.Create
            xmlWriter.WriteStartDocument() //a
            match iDoc.Data() with
            | :? IEnumerable<CoshhChemicalModel> as model -> this.modelSeqHandler(model, this.coshhChemicalWriter(xmlWriter))
            | :? IEnumerable<CoshhApparatusModel> as model -> this.modelSeqHandler(model, this.cosshApparatusWriter(xmlWriter))
            | :? IEnumerable<CoshhProcessModel> as model -> this.modelSeqHandler(model, this.coshhProcessWriter(xmlWriter))
            | _ ->
            xmlWriter.WriteEndDocument() //b
            xmlWriter.Flush()

