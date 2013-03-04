module CoshhFileWriter

    #if INTERACTIVE
    #r @"V:\SafetyProgram\SafetyProgram.Models\bin\Debug\SafetyProgram.Models.dll"
    #r @"V:\SafetyProgram\SafetyProgram.UserControls\bin\Debug\SafetyProgram.UserControls.dll"
    #endif

    open SafetyProgram.Models.DataModels
    open SafetyProgram.UserControls
    open System.Collections.Generic
    open System.Xml

    type public XmlWrite() = 

        let coshhChemicalWriter (xmlWriter : XmlWriter) (model : CoshhChemicalModel) = 
            if model <> null then
                xmlWriter.WriteStartElement("chemical") //a

                if model.Name <> null then xmlWriter.WriteElementString("name", model.Name)

                xmlWriter.WriteStartElement("amount") //b
                if model.Value.ToString() <> null then xmlWriter.WriteElementString("value", model.Value.ToString())
                if model.Unit <> null then xmlWriter.WriteElementString("unit", model.Unit)
                xmlWriter.WriteEndElement() //b

                if model.Hazards <> null then hazardsWriter(xmlWriter, model.Hazards)

                xmlWriter.WriteEndElement() //a

        let commentsWriter(xmlWriter : XmlWriter) (comments) = 
            if comments <> "" then xmlWriter.WriteElementString("comments", comments)

        let titleWriter(xmlWriter : XmlWriter) (title) = 
            if title <> "" then xmlWriter.WriteElementString("title", title)

        let cosshApparatusWriter(xmlWriter : XmlWriter) (model : CoshhApparatusModel) = 
            if model <> null then
                xmlWriter.WriteStartElement("apparatus") //a

                if model.Name <> null then xmlWriter.WriteElementString("name", model.Name)

                if model.Hazards <> null then hazardsWriter(xmlWriter, model.Hazards)

                if model.UsageComments <> null then xmlWriter.WriteElementString("usagecomments", model.UsageComments)

                xmlWriter.WriteEndElement() //a

        let coshhProcessWriter(xmlWriter : XmlWriter) (model : CoshhProcessModel) =
            if model <> null then
                xmlWriter.WriteStartElement("process") //a

                if model.Name <> null then xmlWriter.WriteElementString("name", model.Name)

                if model.Hazards <> null then hazardsWriter(xmlWriter, model.Hazards)

                if model.UsageComments <> null then xmlWriter.WriteElementString("usagecomments", model.UsageComments)

                xmlWriter.WriteEndElement()

        let hazardWriter(xmlWriter : XmlWriter, hazardmodel : HazardModel) = 
            xmlWriter.WriteStartElement("hazard")
            if hazardmodel.SignalWord <> null then xmlWriter.WriteAttributeString("signalWord", hazardmodel.SignalWord)  
            if hazardmodel.Symbol <> null then xmlWriter.WriteAttributeString("symbol", hazardmodel.Symbol) 
            if hazardmodel.Hazard <> null then xmlWriter.WriteString(hazardmodel.Hazard)      
            xmlWriter.WriteEndElement()  

        let hazardsWriter (xmlWriter, hazards) = 
            if hazards.Count <> 0 then
                xmlWriter.WriteStartElement("hazards")
                hazards |> Seq.iter(fun x -> this.hazardWriter(xmlWriter, x))
                xmlWriter.WriteEndElement()

        let modelSeqHandler(models, func) =
                models |> Seq.iter(func)

        let writeIt(path : string, iDoc : IDocUserControl) = 
            use xmlWriter = path |> XmlWriter.Create
            xmlWriter.WriteStartDocument() //a
            match iDoc.Data() with
            | :? IEnumerable<CoshhChemicalModel> as models -> modelSeqHandler(models, coshhChemicalWriter(xmlWriter))
            | :? IEnumerable<CoshhApparatusModel> as models -> modelSeqHandler(models, cosshApparatusWriter(xmlWriter))
            | :? IEnumerable<CoshhProcessModel> as models -> modelSeqHandler(models, coshhProcessWriter(xmlWriter))
            | _ ->
            xmlWriter.WriteEndDocument() //b
            xmlWriter.Flush()