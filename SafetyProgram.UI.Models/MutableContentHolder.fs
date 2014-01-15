namespace SafetyProgram.UI.Models

open System.Collections.ObjectModel
open SafetyProgram.Core.Services
open SafetyProgram.Base

type ContentHolder = {
    Content : GuiDocument
    DataType : DataType
    CommandController : ICommandController
}

module ContentHolderHelpers =
    let defaultConstructor model dataType = { 
        Content = new GuiDocument(model); 
        DataType = dataType; 
        CommandController = new CommandController(); 
    }