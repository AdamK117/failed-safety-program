using System;
using SafetyProgram.DocObjects;

namespace SafetyProgram.Document.Commands
{
    public class DeleteDocObjectICommand : DocumentCommandsBase
    {
        /// <summary>
        /// Construct an ICommand that deletes the currently selected item in the CoshhDocument.
        /// </summary>
        /// <param name="document">CoshhDocument from which selected items will be deleted.</param>
        public DeleteDocObjectICommand(CoshhDocument document)
            : base(document)
        {
            //Monitor changes in the CoshhDocument's selection (can't delete if there isn't a selection).
            document.SelectionChanged += (DocObject docObject) => RaiseCanExecuteChanged();
        }        

        /// <summary>
        /// Can only execute if there is currently something selected
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <returns></returns>
        public override bool CanExecute(object parameter)
        {
            return document.Selected == null ? false : true;
        }

        /// <summary>
        /// Deletes the currently selected DocObject from the CoshhDocument
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <exception cref="NotSupportedException">Thrown if Execute is called but CanExecute == false</exception>
        public override void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                document.Body.Remove(document.Selected);
                document.ClearSelection();
            }
            else throw new NotSupportedException("Call to execute made when it cant execute (CanExecute() == false)");            
        }
    }
}
