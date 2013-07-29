using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SafetyProgram.DocumentUi.Commands;

namespace SafetyProgram.UI.Document
{
    public interface IDocumentContextMenuViewModel
    {
        IDocumentICommands Commands { get; }
    }
}
