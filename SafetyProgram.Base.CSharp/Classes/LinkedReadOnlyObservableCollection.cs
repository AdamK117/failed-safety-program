using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace SafetyProgram.Base.CSharp
{
    public static class MutableHelpers
    {
        public static ReadOnlyObservableCollection<TChild>
            EchoCollection<TParent, TChild>(this ObservableCollection<TParent> parent,
            Func<TParent, TChild> linker)
        {
            return new LinkedReadOnlyObservableCollection<TParent, TChild>(
                parent,
                linker);
        }
    }

    public sealed class LinkedReadOnlyObservableCollection<Parent, Child> 
        : ReadOnlyObservableCollection<Child>
    {
        private readonly Func<Parent, Child> childCtor;
        private readonly ObservableCollection<Parent> parents;

        public LinkedReadOnlyObservableCollection(ObservableCollection<Parent> parents, 
            Func<Parent, Child> childCtor)
            /* Create the read only collection of children for the ReadOnlyObservableCollection ctor. */
            : base(
                new ObservableCollection<Child>(
                    parents.Select(
                        (parent) => childCtor(parent))))
        {
            this.parents = parents;
            this.childCtor = childCtor;

            this.parents.CollectionChanged += parents_CollectionChanged;
        }

        /// <summary>
        /// Handles changes in the parent. Echoing to this child.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void parents_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (Parent parent in e.NewItems)
                    {
                        this.Items.Insert(e.NewStartingIndex, childCtor(parent));
                    }
                    break;

                case NotifyCollectionChangedAction.Reset:
                    this.Items.Clear();
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (Parent parent in e.OldItems)
                    {
                        this.Items.RemoveAt(e.OldStartingIndex);
                    }
                    break;

                case NotifyCollectionChangedAction.Replace:
                    foreach (Parent parent in e.OldItems)
                    {
                        this.Items.RemoveAt(e.OldStartingIndex);
                    }
                    foreach (Parent parent in e.NewItems)
                    {
                        this.Items.Insert(e.NewStartingIndex, childCtor(parent));
                    }
                    break;
                
                case NotifyCollectionChangedAction.Move:
                    //TODO: Implement this.
                    break;
            }
        }
    }
}
