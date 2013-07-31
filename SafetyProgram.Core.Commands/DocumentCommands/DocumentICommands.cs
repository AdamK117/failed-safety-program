using System;
using System.Collections.Generic;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Core.Models;

namespace SafetyProgram.Core.Commands
{
    public sealed class DocumentICommands : IDocumentICommands
    {
        public DocumentICommands(IDocument document, ICommandInvoker commandInvoker)
        {
        }

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
