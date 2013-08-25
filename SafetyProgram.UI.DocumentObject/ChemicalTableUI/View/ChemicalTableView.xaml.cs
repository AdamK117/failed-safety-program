﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SafetyProgram.Core.Models;

namespace SafetyProgram.UI.DocumentObject.ChemicalTableUI
{
    /// <summary>
    /// Interaction logic for ChemicalTableView.xaml
    /// </summary>
    internal sealed partial class ChemicalTableView : UserControl
    {
        private readonly IChemicalTableViewModel viewModel;

        public ChemicalTableView(IChemicalTableViewModel viewModel)
        {
            this.viewModel = viewModel;
            this.DataContext = viewModel;

            viewModel.SelectedChemicals.CollectionChanged += (selectedChemicals, collectionArgs) =>
                {
                    if (collectionArgs.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
                    {
                        Chemicals.SelectedItems.Clear();
                    }
                };

            InitializeComponent();

            this.InputBindings.AddRange(viewModel.Hotkeys);
        }

        #region Selection logic

        /// <summary>
        /// Handles the selection of chemicals in the ChemicalTable ListView (needed because ListView doesn't have a multiselect XAML Dependancy property
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Chemicals_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (ICoshhChemical chemical in e.RemovedItems)
            {
                viewModel.SelectedChemicals.Remove(chemical);
            }

            foreach (ICoshhChemical chemical in e.AddedItems)
            {
                viewModel.SelectedChemicals.Add(chemical);
            }
        }

        #endregion

        #region Drag&Drop logic

        //Does the DragDrop
        private void Chemicals_MouseMove(object sender, MouseEventArgs e)
        {
            //If the user is left clicking on the ChemicalTable ListView.
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                ListView chemicalTable = (ListView)sender;

                //Try and do a DragDrop operation
                //DragDrop.DoDragDrop(
                //    chemicalTable, 
                //    //viewModel.SelectedChemicals.GetDataObject(),
                //    DragDropEffects.Move
                //);
            }
        }

        //Provides effects when drag enters (preview etc.)
        private void Chemicals_DragEnter(object sender, DragEventArgs e)
        {
        }

        //Generally used to revert Chemicals_DragEnter
        private void Chemicals_DragLeave(object sender, DragEventArgs e)
        {
        }

        //Gives feedback while dragging over
        private void Chemicals_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.None;
        }

        //Performs the drop
        private void Chemicals_Drop(object sender, DragEventArgs e)
        {
            //if (e.Data.GetDataPresent(viewModel.ComHelper.ComIdentifier))
            //{
            //    List<CoshhChemicalObject> draggedChemicals = (List<CoshhChemicalObject>)e.Data.GetData(viewModel.ComHelper.ComIdentifier);

            //    foreach (CoshhChemicalObject chemical in draggedChemicals)
            //    {
            //        viewModel.Chemicals.Add(chemical);
            //    }
            //}
        }

        #endregion
    }
}
