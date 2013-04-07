using System.ComponentModel;

namespace SafetyProgram.MainWindow.Document.Controls.ChemicalTable.Commands
{
    public class DeleteSelectedICommand : ChemicalTableCommandsBase
    {
        public DeleteSelectedICommand(ChemicalTable table) : base(table) 
        {
            table.PropertyChanged += new PropertyChangedEventHandler(table_PropertyChanged);
        }

        void table_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedChemical") RaiseCanExecuteChanged();
        }

        public override void Execute(object parameter)
        {
            table.Chemicals.Remove(table.SelectedChemical);
            table.SelectedChemical = null;
        }

        public override bool CanExecute(object parameter)
        {
            return table.SelectedChemical == null ? false : true;
        }
    }
}