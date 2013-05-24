using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SafetyProgram.Document.Commands;

namespace SafetyProgram.Document.ContextMenus
{
    public interface IDocumentContextMenuViewModel
    {
        IDocumentICommands Commands { get; }
    }
}
