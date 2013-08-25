using System;
using System.Collections.Generic;
using System.Windows.Input;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Core.Models;

namespace SafetyProgram.Core.Commands
{
    public sealed class DocumentICommands : IDocumentICommands
    {
        public DocumentICommands(IDocument document, ICommandInvoker commandInvoker)
        { }

        public ICommand DeleteIDocObject
        {
            get { throw new NotImplementedException(); }
        }

        public ICommand InsertChemicalTable
        {
            get { throw new NotImplementedException(); }
        }

        public List<InputBinding> Hotkeys
        {
            get { throw new NotImplementedException(); }
        }
    }
}
