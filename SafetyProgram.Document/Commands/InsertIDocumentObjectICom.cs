using System;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Document.Commands
{
    public sealed class InsertIDocumentObjectICom : ExtendedICommand<IDocument>
    {
        private readonly Func<IDocumentObject> iDocumentObjectCtor;

        public InsertIDocumentObjectICom(IDocument document, Func<IDocumentObject> iDocumentObjectCtor) : base(document) 
        {
            this.iDocumentObjectCtor = iDocumentObjectCtor;
        }

        /// <summary>
        /// Can always execute.
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <returns></returns>
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Inserts a ChemicalTable into the CoshhDocument
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <exception cref="NotSupportedException">Thrown if Execute is called but CanExecute == false</exception>
        public override void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                data.Body.Items.Add(iDocumentObjectCtor());
            }
            else throw new NotSupportedException("Call to execute made when it cant execute (CanExecute() == false)");
        }
    }
}
