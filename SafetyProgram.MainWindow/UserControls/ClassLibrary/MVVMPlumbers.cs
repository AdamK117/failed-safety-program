using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using SafetyProgram.Data;
using SafetyProgram.Models.DataModels;
using SafetyProgram.UserControls;

namespace SafetyProgram.MainWindow.UserControls.ClassLibrary
{
    public class MVVMPlumbers
    {
        public static void genericCollectionChanged
            <ViewModel, ICoshhDocDataObjectModel>
            (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e, ObservableCollection<ViewModel> viewModels, Func<ICoshhDocDataObjectModel, ViewModel> converter)
            where ViewModel : BaseViewModel
            where ICoshhDocDataObjectModel : IDocDataHolder<BaseElementModel>
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    bool mod = new bool();
                    foreach (ViewModel vm in viewModels)
                    {
                        foreach (ICoshhDocDataObjectModel item in e.OldItems)
                        {
                            if (vm.GetModel() == item.Data())
                            {
                                viewModels.Remove(vm);
                                mod = true;
                                break;
                            }
                        }
                        if (mod == true) { break; }
                    }
                    break;

                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    foreach (ICoshhDocDataObjectModel model in e.NewItems)
                    {
                        viewModels.Insert(e.NewStartingIndex, converter(model));
                    }
                    break;

                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    viewModels.Clear();
                    break;
            }
        }
    }
}
