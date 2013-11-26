namespace SafetyProgram.UI.Models

open System.Collections.ObjectModel
open SafetyProgram.Core.IO.Services
open SafetyProgram.Base

type ContentHolder = {
    Content : GuiDocument
    DataType : DataType
    CommandController : ICommandController
    Selection : ObservableCollection<obj>
}