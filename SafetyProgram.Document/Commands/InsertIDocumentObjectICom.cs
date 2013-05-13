using System;
using System.Windows.Input;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Document.Commands
{
    public sealed class InsertIDocumentObjectICom : ICommand
    {
        private readonly IDocument data;
        private readonly Func<IDocumentObject> iDocumentObjectCtor;

        public InsertIDocumentObjectICom(
            IDocument document, 
            Func<IDocumentObject> iDocumentObjectCtor
            )
        {
            this.data = document;
            this.iDocumentObjectCtor = iDocumentObjectCtor;
        }

        /// <summary>
        /// Can always execute.
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Inserts a ChemicalTable into the CoshhDocument
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <exception cref="NotSupportedException">Thrown if Execute is called but CanExecute == false</exception>
        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                data.Body.Items.Add(iDocumentObjectCtor());
            }
            else throw new NotSupportedException("Call to execute made when it cant execute (CanExecute() == false)");
        }

        public event EventHandler CanExecuteChanged;
    }
}
