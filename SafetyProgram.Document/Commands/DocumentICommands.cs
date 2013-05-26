using System.Collections.Generic;
using System.Windows.Input;
using SafetyProgram.Base;
using SafetyProgram.Base.GenericCommands;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.DocumentObjects;

namespace SafetyProgram.Document.Commands
{
    internal sealed class DocumentICommands : IDocumentICommands
    {
        public DocumentICommands(IDocumentBody documentBody, 
            IConfiguration appConfiguration,
            ICommandInvoker commandInvoker)
        {
            Helpers.NullCheck(documentBody, appConfiguration, commandInvoker);

            InsertChemicalTable = new InsertObjectICom<IDocumentObject>(
                documentBody.Items,
                commandInvoker,
                () => DefaultDocumentObjects.ChemicalTable(appConfiguration, commandInvoker)
                );

            DeleteIDocObject = new DeleteIDocObjectICom(documentBody, commandInvoker);

            Hotkeys = setHotkeys();
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
