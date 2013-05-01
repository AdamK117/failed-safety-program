using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SafetyProgram.Base.UnitTests
{
    [TestClass]
    public class MonadOcTests
    {
        private ViewModelOc<SimpleBaseObject, MonadSimpleBaseObject> getTestData(ObservableCollection<SimpleBaseObject> source)
        {
            return new ViewModelOc<SimpleBaseObject, MonadSimpleBaseObject>(source, (simpleobj) => new MonadSimpleBaseObject(simpleobj));
        }

        private ObservableCollection<SimpleBaseObject> getTestSourceData()
        {
            var simpleSource = new ObservableCollection<SimpleBaseObject>();

            simpleSource.Add(new SimpleBaseObject("Adam"));
            simpleSource.Add(new SimpleBaseObject("Adam"));
            simpleSource.Add(new SimpleBaseObject("Adam"));

            return simpleSource;
        }

        [TestMethod]
        public void ConstructMonadOc()
        {
            //Tests if the MonadOc constructs correctly
            //  It contains the same number of elements as the parent ("model") oc
            //  Each monad element contains the right model data

            var simpleSource = getTestSourceData();
            var monadOc = getTestData(simpleSource);

            Assert.AreEqual(simpleSource.Count, monadOc.Count, "The number of items in the MonadOc does not equal the number of items in the BaseOc, one is a shadow of the other, they should be the same");

            for (int i = 0; i < monadOc.Count; i++)
            {
                Assert.AreEqual(simpleSource[i], monadOc[i].Model, "The models do not match between the Monad and underlying model");
            }
        }

        [TestMethod]
        public void AddElementToMonadOc()
        {
            //Test if elements added to the end of the MonadOc correctly echo down into the underlying raw model

            var simpleSource = getTestSourceData();
            var monadOc = getTestData(simpleSource);

            var simpleModel = new SimpleBaseObject("My added model");
            var monadModel = new MonadSimpleBaseObject(simpleModel);

            //Checks if the expected collection changed handler has been called
            simpleSource.CollectionChanged += (sender, e) =>
            {
                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                {
                    Assert.AreEqual(e.NewItems[0], simpleModel);
                }
                else Assert.Fail("An unexpected collectionchanged handler was called, only expecting the Add handler to be called");
            };

            //Add an item, triggers the delegate above
            monadOc.Add(monadModel);

            //Redundancy check (the event handler should be adequate but just in case)
            Assert.AreEqual(monadOc.Count, simpleSource.Count, "The Oc<T> is not in sync with MonadOc<IMonad<T>>, the add method for the MonadOc isn't echoing to the underlying Oc<T>");
        }

        [TestMethod]
        public void RemoveElementFromMonadOc()
        {
            //Tests that elements removed from the MonadOc correctly echo into the underlying Oc

            var simpleSource = getTestSourceData();
            var monadOc = getTestData(simpleSource);

            //Set an item to remove
            var removedItem = monadOc[1];

            //Add a handler to the simple source to see that it is correctly executing the right behaviour
            simpleSource.CollectionChanged += (sender, e) =>
                {
                    if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
                    {
                        Assert.AreEqual(e.OldItems[0], removedItem.Model);
                    }
                    else Assert.Fail("Unexpected collectionchanged action was taken on the child Oc when the parent Oc executed");
                };

            //Will trigger the test delegate (above)
            monadOc.Remove(removedItem);

            //Redundancy check, ensures that the count is at least the same
            Assert.AreEqual(monadOc.Count, simpleSource.Count);
        }

        [TestMethod]
        public void ResetMonadOc()
        {
            var simpleSource = getTestSourceData();
            var monadOc = getTestData(simpleSource);

            //Add a handler that checks that the reset handler is called on the child Oc
            simpleSource.CollectionChanged += (sender, e) =>
                {
                    if (e.Action != System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
                    {
                        Assert.Fail("The reset method was not called on the underlying model, some other method was called");
                    }
                };

            //Should trigger the delegate above
            monadOc.Clear();

            //Ensure both are cleared
            Assert.AreEqual(monadOc.Count, 0);
            Assert.AreEqual(simpleSource.Count, 0);
        }

        [TestMethod]
        public void MoveElementInMonadOc()
        {
            var simpleSource = getTestSourceData();
            var monadOc = getTestData(simpleSource);

            var simpleModel = new SimpleBaseObject("MyMovedItem");
            var monadModel = new MonadSimpleBaseObject(simpleModel);

            //Add the model (may fail here if the Add method fails unit test)
            monadOc.Add(monadModel);

            //Move the model
            monadOc.Move(monadOc.IndexOf(monadModel), 1);

            //Check that the element in the underlying sequence has also been moved to the same location
            Assert.AreEqual(monadOc.IndexOf(monadModel), simpleSource.IndexOf(simpleModel), "Synced elements are at different locations after moving");
        }

        [TestMethod]
        public void ReplaceElementInMonadOc()
        {
            var simpleSource = getTestSourceData();
            var monadOc = getTestData(simpleSource);

            var simpleModel = new SimpleBaseObject("A Replacement Item");
            var monadModel = new MonadSimpleBaseObject(simpleModel);

            int replaceLocation = 1;

            simpleSource.CollectionChanged += (sender, e) =>
                {
                    if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Replace)
                    {
                        Assert.AreSame(e.NewItems[0], simpleModel);
                        Assert.AreEqual(e.NewStartingIndex, replaceLocation);
                    }
                };

            monadOc[replaceLocation] = monadModel;

            Assert.AreSame(monadOc[replaceLocation].Model, simpleSource[replaceLocation]);
        }

        [TestMethod]
        public void AddElementToRaw()
        {
            //Checks to see if elements added to the raw data correctly "bubble up" into the nomadOc

            var simpleSource = getTestSourceData();
            var monadOc = getTestData(simpleSource);

            var simpleModel = new SimpleBaseObject("An added model");

            monadOc.CollectionChanged += (sender, e) =>
                {
                    if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
                    {
                        foreach (MonadSimpleBaseObject item in e.NewItems)
                        {
                            Assert.AreSame(item.Model, simpleModel);
                        }
                    }
                    else Assert.Fail("Unexpected handler called on the Oc, should be add only");
                };

            //If wired up correctly, should trigger the delegate (above).
            //  Delegate will check that the echoed value is correct
            simpleSource.Add(simpleModel);

            Assert.AreEqual(monadOc.Count, simpleSource.Count, "Difference between the source and the monadOc, should be the same (after echo)");
        }

        [TestMethod]
        public void RemoveElementFromRaw()
        {
            var simpleSource = getTestSourceData();
            var monadOc = getTestData(simpleSource);

            int removedIndex = 1;

            //Set an item to remove
            var removedSourceItem = simpleSource[removedIndex];
            var removedMonadItem = monadOc[removedIndex];

            //Add a handler to the monad source to see that it is correctly executing the right behaviour
            monadOc.CollectionChanged += (sender, e) =>
            {
                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
                {
                    Assert.AreEqual(((MonadSimpleBaseObject)e.OldItems[0]).Model, removedSourceItem);
                }
                else Assert.Fail("Unexpected collectionchanged action was taken on the child Oc when the parent Oc executed");
            };

            //Will trigger the test delegate (above)
            simpleSource.Remove(removedSourceItem);

            //Redundancy check, ensures that the count is at least the same
            Assert.IsFalse(monadOc.Contains(removedMonadItem), "MonadOc still contains the item removed from the model");
            Assert.AreEqual(monadOc.Count, simpleSource.Count);
        }

        [TestMethod]
        public void ResetRaw()
        {
            //Tests that when the raw ObservableCollection is reset the MonadOc also resets

            var simpleSource = getTestSourceData();
            var monadOc = getTestData(simpleSource);

            //Redundancy check (the test data shouldn't start cleared)
            Assert.AreNotEqual(monadOc.Count, 0);
            Assert.AreNotEqual(simpleSource.Count, 0);

            simpleSource.Clear();

            Assert.AreEqual(monadOc.Count, 0);
            Assert.AreEqual(simpleSource.Count, 0);
        }

        [TestMethod]
        public void MoveElementInRaw()
        {
            var simpleSource = getTestSourceData();
            var monadOc = getTestData(simpleSource);

            var simpleModel = new SimpleBaseObject("My Moved Item");

            int newLocation = 2;

            //Add the model, may fail here if AddElementToRaw() is failing
            simpleSource.Add(simpleModel);

            simpleSource.Move(simpleSource.IndexOf(simpleModel), newLocation);

            Assert.AreEqual(simpleSource[newLocation], monadOc[newLocation].Model);
        }

        [TestMethod]
        public void ReplaceElementInRaw()
        {
            var simpleSource = getTestSourceData();
            var monadOc = getTestData(simpleSource);

            var simpleModel = new SimpleBaseObject("A replacement item");

            int replacementLocation = 1;

            simpleSource[replacementLocation] = simpleModel;

            Assert.AreSame(simpleSource[replacementLocation], monadOc[replacementLocation].Model);
        }
    }

    public class SimpleBaseObject
    {
        public SimpleBaseObject(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }

    public class MonadSimpleBaseObject : IViewModel<SimpleBaseObject>
    {
        private SimpleBaseObject value;

        public MonadSimpleBaseObject(SimpleBaseObject raw)
        {
            value = raw;
        }

        public SimpleBaseObject Model
        {
            get { return value; }
        }
    }
}
