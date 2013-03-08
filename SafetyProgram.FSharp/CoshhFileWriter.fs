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
        let commentsWriter(xmlWriter : XmlWriter) (comments) = 
            if comments <> "" then xmlWriter.WriteElementString("comments", comments)


        let titleWriter(xmlWriter : XmlWriter) (title) = 
            if title <> "" then xmlWriter.WriteElementString("title", title)


        let coshhChemicalWriter (xmlWriter : XmlWriter) (model : IDocDataHolder<CoshhChemicalModel>) = 
            if model <> null then
                xmlWriter.WriteStartElement("chemical") //a

                if model.Data().Name <> null then xmlWriter.WriteElementString("name", model.Data().Name)

                xmlWriter.WriteStartElement("amount") //b
                if model.Data().Value.ToString() <> null then xmlWriter.WriteElementString("value", model.Data().Value.ToString())
                if model.Data().Unit <> null then xmlWriter.WriteElementString("unit", model.Data().Unit)
                xmlWriter.WriteEndElement() //b

                //if model.Data().Hazards <> null then hazardsWriter(xmlWriter, model.Data().Hazards)

                xmlWriter.WriteEndElement() //a


        let cosshApparatusWriter(xmlWriter : XmlWriter) (model : IDocDataHolder<CoshhApparatusModel>) = 
            if model <> null then
                xmlWriter.WriteStartElement("apparatus") //a

                if model.Data().Name <> null then xmlWriter.WriteElementString("name", model.Data().Name)

                //if model.Data().Hazards <> null then hazardsWriter(xmlWriter, model.Data().Hazards)

                if model.Data().UsageComments <> null then xmlWriter.WriteElementString("usagecomments", model.Data().UsageComments)

                xmlWriter.WriteEndElement() //a

        let coshhProcessWriter(xmlWriter : XmlWriter) (model : IDocDataHolder<CoshhProcessModel>) =
            if model <> null then
                xmlWriter.WriteStartElement("process") //a

                if model.Data().Name <> null then xmlWriter.WriteElementString("name", model.Data().Name)

                //if model.Data().Hazards <> null then hazardsWriter(xmlWriter, model.Data().Hazards)

                if model.Data().UsageComments <> null then xmlWriter.WriteElementString("usagecomments", model.Data().UsageComments)

                xmlWriter.WriteEndElement()

        let hazardWriter(xmlWriter : XmlWriter, hazardmodel : HazardModel) = 
            xmlWriter.WriteStartElement("hazard")
            if hazardmodel.SignalWord <> null then xmlWriter.WriteAttributeString("signalWord", hazardmodel.SignalWord)  
            if hazardmodel.Symbol <> null then xmlWriter.WriteAttributeString("symbol", hazardmodel.Symbol) 
            if hazardmodel.Hazard <> null then xmlWriter.WriteString(hazardmodel.Hazard)      
            xmlWriter.WriteEndElement()  

        //let hazardsWriter (xmlWriter, hazards : seq<HazardModel>) = 
            //if hazards.Count <> 0 then
                //xmlWriter.WriteStartElement("hazards")
                //hazards |> Seq.iter(fun x -> this.hazardWriter(xmlWriter, x))
                //xmlWriter.WriteEndElement()

        let modelSeqHandler(models, nodeName, xmlWriter : XmlWriter, nodeWriter) =
            xmlWriter.WriteStartElement(nodeName) //a
            models |> Seq.iter(nodeWriter(xmlWriter))
            xmlWriter.WriteEndElement() //a

        member public this.writeIt(path : string, iDoc : IDocUserControl) = 
            use xmlWriter = path |> XmlWriter.Create

            xmlWriter.WriteStartDocument() //a
            xmlWriter.WriteStartElement("coshh") //b

            match iDoc.Data() with
            | :? IEnumerable<IDocDataHolder<CoshhChemicalModel>> as models -> modelSeqHandler(models, "chemicals", xmlWriter, coshhChemicalWriter)
            | :? IEnumerable<IDocDataHolder<CoshhApparatusModel>> as models -> modelSeqHandler(models, "apparatuses", xmlWriter, cosshApparatusWriter)
            | :? IEnumerable<IDocDataHolder<CoshhProcessModel>> as models -> modelSeqHandler(models, "processes", xmlWriter, coshhProcessWriter)
            | _ ->

            xmlWriter.WriteEndElement() //b
            xmlWriter.WriteEndDocument() //a

            xmlWriter.Flush()