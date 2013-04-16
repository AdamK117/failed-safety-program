using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using SafetyProgram.Models.DataModels;
using System.Collections.Generic;

namespace SafetyProgram.DocObjects.ChemicalTable
{
    /// <summary>
    /// Interaction logic for ChemicalTableView.xaml
    /// </summary>
    public partial class ChemicalTableView : UserControl
    {
        private readonly ChemicalTable viewModel;

        public ChemicalTableView(ChemicalTable viewModel)
        {
            this.viewModel = viewModel;
            DataContext = viewModel;

            viewModel.SelectedChemicals.CollectionChanged += (selectedChemicals, collectionArgs) =>
                {
                    if (collectionArgs.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
                    {
                        Chemicals.SelectedItems.Clear();
                    }
                };

            InitializeComponent();
        }

        /// <summary>
        /// Handles the selection of chemicals in the ChemicalTable ListView (needed because ListView doesn't have a multiselect XAML Dependancy property
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Chemicals_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (CoshhChemicalModel chemical in e.RemovedItems)
            {
                viewModel.SelectedChemicals.Remove(chemical);
            }

            foreach (CoshhChemicalModel chemical in e.AddedItems)
            {
                viewModel.SelectedChemicals.Add(chemical);
            }
        }

        /// <summary>
        /// Selects the ChemicalTable when it is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChemicalTable_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            viewModel.Select();
        }

        private void Chemicals_MouseMove(object sender, MouseEventArgs e)
        {
            //If the user is left clicking on the ChemicalTable ListView.
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                ListView chemicalTable = (ListView)sender;

                //Try and do a DragDrop operation
                DragDrop.DoDragDrop(
                    chemicalTable, 
                    new ChemicalTableComObject(
                        viewModel.SelectedChemicals,
                        "CoshhChemicalModels"
                    ).GetDataObject(),
                    DragDropEffects.Move
                );
            }
        }

        private void Chemicals_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.All;
            if (e.Data.GetDataPresent("CoshhChemicalModels"))
            {
            }
        }

        private void Chemicals_DragLeave(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.None;
        }

        private void Chemicals_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.All;

            if (e.Data.GetDataPresent("CoshhChemicalModels"))
            {
            }
        }

        private void Chemicals_Drop(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.All;
            if (e.Data.GetDataPresent("CoshhChemicalModels"))
            {
                List<CoshhChemicalModel> draggedChemicals = (List<CoshhChemicalModel>)e.Data.GetData("CoshhChemicalModels");

                foreach (CoshhChemicalModel chemical in draggedChemicals)
                {
                    viewModel.Chemicals.Add(chemical);
                }
            }
        }
    }
}
