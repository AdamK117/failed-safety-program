using System;
using System.Collections.Generic;
using SafetyProgram.Models;

namespace SafetyProgram.DocumentUi.Commands
{
    public sealed class DocumentICommands : IDocumentICommands
    {
        public DocumentICommands(IDocument document)
        { }

        public System.Windows.Input.ICommand DeleteIDocObject
        {
            get { throw new NotImplementedException(); }
        }

        public System.Windows.Input.ICommand InsertChemicalTable
        {
            get { throw new NotImplementedException(); }
        }

        public List<System.Windows.Input.InputBinding> Hotkeys
        {
            get { throw new NotImplementedException(); }
        }
    }
}
