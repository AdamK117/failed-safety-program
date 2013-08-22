using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace SafetyProgram.Base
{
    public sealed class ViewModelOc<Model, ViewModel> : ObservableCollection<ViewModel>
        where ViewModel : IViewModel<Model>
    {
        private bool surpressEvents;
        private readonly Func<Model, ViewModel> viewModelCtor;
        private readonly ObservableCollection<Model> models;

        public ViewModelOc(ObservableCollection<Model> models, Func<Model, ViewModel> viewModelCtor)
        {
            surpressEvents = false;
            this.viewModelCtor = viewModelCtor;            
            this.models = models;

            foreach (Model model in models)
            {
                this.Add(viewModelCtor(model));
            }

            //Monitor changes in the raw Oc (to map onto this derived one)
            models.CollectionChanged += modelsChanged;

            //Monitor changes in this Oc (to map into the raw)
            this.CollectionChanged += viewModelsChanged;
        }
        
        /// <summary>
        /// Defines behaviour that will echo down into the underlying raw data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void viewModelsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (surpressEvents == false)
            {
                surpressEvents = true;

                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        foreach (ViewModel item in e.NewItems)
                        {
                            models.Insert(e.NewStartingIndex, item.Model);
                        }
                        break;

                    case NotifyCollectionChangedAction.Reset:
                        models.Clear();
                        break;

                    case NotifyCollectionChangedAction.Remove:
                        foreach (ViewModel item in e.OldItems)
                        {
                            models.Remove(item.Model);
                        }
                        break;

                    //Remove the old models, add in the new ones
                    case NotifyCollectionChangedAction.Replace:
                        foreach (ViewModel item in e.OldItems)
                        {
                            models.RemoveAt(e.OldStartingIndex);
                        }
                        foreach (ViewModel item in e.NewItems)
                        {
                            models.Insert(e.NewStartingIndex, item.Model);
                        }
                        break;

                    case NotifyCollectionChangedAction.Move:
                        foreach (ViewModel item in e.OldItems)
                        {
                            models.Move(e.OldStartingIndex, e.NewStartingIndex);
                        }
                        break;
                }

                surpressEvents = false;
            }            
        }

        void modelsChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (surpressEvents == false)
            {
                surpressEvents = true;

                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        foreach (Model item in e.NewItems)
                        {
                            this.Insert(e.NewStartingIndex, viewModelCtor(item));
                        }
                        break;

                    case NotifyCollectionChangedAction.Reset:
                        this.Clear();
                        break;

                    case NotifyCollectionChangedAction.Remove:
                        foreach (Model item in e.OldItems)
                        {
                            int baseIndex = e.OldStartingIndex;
                            this.RemoveItem(baseIndex);
                        }
                        break;

                    case NotifyCollectionChangedAction.Move:
                        foreach (Model item in e.OldItems)
                        {
                            this.Move(e.OldStartingIndex, e.NewStartingIndex);
                        }
                        break;

                    case NotifyCollectionChangedAction.Replace:
                        foreach (Model item in e.OldItems)
                        {
                            this.RemoveAt(e.OldStartingIndex);
                        }
                        foreach (Model item in e.NewItems)
                        {
                            this.Insert(e.NewStartingIndex, viewModelCtor(item));
                        }
                        break;
                }

                surpressEvents = false;
            }            
        }
    }
}
