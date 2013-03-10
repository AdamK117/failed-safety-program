module SafetyProgram.FSharp.CoshhXmlWriter

    #if INTERACTIVE
    #r @"V:\SafetyProgram\SafetyProgram.Models\bin\Debug\SafetyProgram.Models.dll"
    #r @"V:\SafetyProgram\SafetyProgram.UserControls\bin\Debug\SafetyProgram.UserControls.dll"
    #endif

    open SafetyProgram.Models.DataModels
    open SafetyProgram.UserControls
    open SafetyProgram.Data.DOM
    open SafetyProgram.UserControls.MainWindowControls.ChemicalTable
    open System.Windows.Controls
    open System.Windows
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
        
        //CoshhApparatusModel Writer
        let cosshApparatusWriter(xmlWriter : XmlWriter) (model : CoshhApparatusModel) = 
            if model <> null then
                xmlWriter.WriteStartElement("apparatus") //a

                if model.Name <> null then xmlWriter.WriteElementString("name", model.Name)

                if model.Hazards <> null then hazardsWriter(xmlWriter, model.Hazards)

                if model.UsageComments <> null then xmlWriter.WriteElementString("usagecomments", model.UsageComments)

                xmlWriter.WriteEndElement() //a
        
        //CoshhProcessModelWriter
        let coshhProcessWriter(xmlWriter : XmlWriter) (model : CoshhProcessModel) =
            if model <> null then
                xmlWriter.WriteStartElement("process") //a

                if model.Name <> null then xmlWriter.WriteElementString("name", model.Name)

                if model.Hazards <> null then hazardsWriter(xmlWriter, model.Hazards)

                if model.UsageComments <> null then xmlWriter.WriteElementString("usagecomments", model.UsageComments)

                xmlWriter.WriteEndElement()
        
        //Model Sequence Writer (Generic)
        let modelSeqHandler(models, nodeName, xmlWriter : XmlWriter, nodeWriter) =
            xmlWriter.WriteStartElement(nodeName) //a
            models |> Seq.iter(nodeWriter(xmlWriter))
            xmlWriter.WriteEndElement() //a
        
        //Coshh Document Writer
        member public this.writeDocument(path : string, doc : CoshhDocument) = 
            let xmlWriterSettings = new XmlWriterSettings()
            xmlWriterSettings.Indent <- true
            use xmlWriter = XmlWriter.Create(path, xmlWriterSettings)

            xmlWriter.WriteStartDocument() //a
            xmlWriter.WriteStartElement("coshh") //b

            doc.Body |> Seq.iter(fun x -> this.writeIDocObject(path, xmlWriter, x))

            xmlWriter.WriteEndElement() //b
            xmlWriter.WriteEndDocument() //a

            xmlWriter.Flush()

        //Write Chemical Table Xml
        member public this.ChemicalTableWriter(chemicalTable : ChemicalTableView, xmlWriter : XmlWriter) = 
            xmlWriter.WriteStartElement("chemicals") //a
            chemicalTable.Data |> Seq.iter(coshhChemicalWriter(xmlWriter))
            xmlWriter.WriteEndElement() //a
            
        //Document Object Writer
        member public this.writeIDocObject(path : string, xmlWriter : XmlWriter, iDocObject : IDocObject) = 
            match iDocObject.Display() with
            | :? ChemicalTableView as chemicalTable -> this.ChemicalTableWriter(chemicalTable, xmlWriter)
            | _ -> ()