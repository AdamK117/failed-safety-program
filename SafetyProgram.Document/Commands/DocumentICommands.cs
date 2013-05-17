using System;
using System.Collections.Generic;
using System.Windows.Input;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.DocumentObjects;

namespace SafetyProgram.Document.Commands
{
    internal sealed class DocumentICommands : IDocumentICommands
    {
        public DocumentICommands(ICoshhDocument document, ICommandInvoker commandInvoker)
        {
            if (
                document != null &&
                commandInvoker != null
                )
            {
                InsertChemicalTable = new InsertIDocumentObjectICom(
                    document,
                    commandInvoker,
                    () => DefaultDocumentObjects.ChemicalTable(document.AppConfiguration, commandInvoker)
                    );
                DeleteIDocObject = new DeleteIDocObjectICom(document, commandInvoker);

                Hotkeys = setHotkeys();
            }
            else throw new ArgumentNullException();            
        }

        private List<InputBinding> setHotkeys()
        {
            return new List<InputBinding>()
            {
                //Delete Selection: DEL
                new InputBinding(
                    DeleteIDocObject,
                    new KeyGesture(Key.Delete)
                )
            };
        }

        public List<InputBinding> Hotkeys
        {
            get;
            private set;
        }

        public ICommand DeleteIDocObject
        {
            get;
            private set;
        }

        public ICommand InsertChemicalTable
        {
            get;
            private set;
        }
    }
}
