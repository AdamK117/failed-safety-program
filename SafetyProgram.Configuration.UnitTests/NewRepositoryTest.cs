using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Configuration.UnitTests
{
    [TestClass]
    public class NewRepositoryTest
    {
        public class MockEntry
        {
            public MockEntry(string name)
            {
                this.Name = name;
            }

            public string Name
            {
                get;
                set;
            }
        }

        private IList<MockEntry> mockEntries = new List<MockEntry>()
        {
            new MockEntry("Batman"),
            new MockEntry("Superman"),
            new MockEntry("Green Goblin"),
            new MockEntry("Ironman"),
            new MockEntry("Captain America"),
            new MockEntry("Wonder Woman")
        };

        //Solid dependancy on the implementation here.
        private INewRepository<MockEntry> mockRepository()
        {
            //Mock a service the repository will use.
            var mockedService = new Mock<ICallbackService<MockEntry>>();

            //Mock blank loading (no callback) method on the service.
            mockedService
                .Setup(
                    svc => svc.LoadContent()
                )
                .Returns(
                    () => { return mockEntries; }
                );
            
            //Mock callback loading method on the service (will push entries through the callback).
            mockedService
                .Setup(
                    svc => svc.LoadContent(
                        It.IsAny<Action<MockEntry>>()
                    )
                )
                //Callback argument, push all the entries one by one through this callback.
                .Callback<Action<MockEntry>>(
                    callback =>
                    {
                        foreach (MockEntry entry in mockEntries)
                        {
                            callback(entry);
                        }
                    }
                )
                //Return value: return all the entries loaded.
                .Returns(
                    () => { return mockEntries; }
                );
            
            return new NewRepository<MockEntry>(mockedService.Object);
        }

        [TestMethod]
        public void GetAllTest()
        {
            //Expected Behaviours:
            //  -1) Entries are only retrieved from the mocked source.
            //      Test: Check each retrieved entry against the mocked source.
            //  -2) Entries are only retrieved once from the mocked source.
            //      Test: Track which entries are returned. Each should only occur once in the return value.
            //  -CONSIDERATION: Retrieved entries may not be in the same order as the source (internal loading mechanism differences).
            //      RESTRICTION: Checks use "contains" not "retrievedEntries[i] == mockedSource[i]"

            var repository = mockRepository();

            var retrievedEntries = repository.GetAll();

            var retrievedEntriesTracker = new List<MockEntry>(mockEntries);

            foreach (MockEntry retrievedEntry in retrievedEntries)
            {
                //-1) Test: Check each retrieved entry against the mocked source.
                Assert.IsTrue(mockEntries.Contains(retrievedEntry), "Unknown entry retrieved (not from mock source).");

                //-2) Test: Track which entries are returned. Each should only occur once in the return value.
                Assert.IsTrue(retrievedEntriesTracker.Contains(retrievedEntry), "The retrieved entry has already occured in the return value.");

                retrievedEntriesTracker.Remove(retrievedEntry);
            }
            
            Assert.IsTrue(retrievedEntriesTracker.Count == 0, "Not all mock entries were contained in the return value.");
        }

        [TestMethod]
        public void GetAllCallbackTest()
        {
            //Expected Behaviours:
            //  -1) The callback is called once for each entry retrieved from the mocked source.
            //      Test: Track which mocked entries have been callbacked. Each should go through the callback once.
            //  -2) The callback is called on all entries in the mocked source.
            //      Test: Track which mocked entries have been callbacked. All mocked entries should go through the callback.  
            //  -3) The callback is only called on entries from the mocked source. No unexpected entries are introduced.
            //      Test: Entries are checked against the mocked source. All callback entries should only be from the mocked source.
            //  -4) The return value contains entries only from the mocked source. Each mocked entry occurs only once in the return.
            //      Test: Check each return entry against the mocked source. Track which entries have been checked.
            //  -CONSIDERATION: Retrieved entries may not be in the same order as the mocked source (internal loading mechanisms may differ).
            //      -RESOLUTION: Do not used indexed check methods. Example: favour mockedSource.Contains(retrievedEntry) v.s. mockedSource[i] == retrievedEntries[i]
            
            var repository = mockRepository();

            //Tracks which entries have been through the callback. If they have, they are removed from this list.
            var callbackTracker = new List<MockEntry>(mockEntries);

            Action<MockEntry> entryCallback = 
                (retrievedEntry) =>
                {
                    //-3) Test: Entries are checked against the mocked source. All callback entries should only be from the mocked source.
                    Assert.IsTrue(mockEntries.Contains(retrievedEntry), "Callback returned an unknown mock entry");

                    //-1) Test: Track which mocked entries have been callbacked. Each should go through the callback once.
                    Assert.IsTrue(callbackTracker.Contains(retrievedEntry), "The retrieved entry has already been callbacked");

                    //Remove the entry from the tracker
                    callbackTracker.Remove(retrievedEntry);
                };

            //EXECUTE METHOD.
            var reposEntries = repository.GetAll(entryCallback);
  
            //-2) Test: Track which mocked entries have been callbacked. All mocked entries should go through the callback.
            Assert.IsTrue(callbackTracker.Count == 0, "Not all mock entries went through the callback");

            //Tracks which entries are in the return value. If they are, they're removed from this list.
            var returnValueTracker = new List<MockEntry>(mockEntries);

            int i = 0;
            foreach (MockEntry retrievedEntry in reposEntries)
            {
                //-4) Test: Check each return entry against the mocked source. Track which entries have been checked.
                Assert.IsTrue(mockEntries.Contains(retrievedEntry), "Unknown mock entry was retrieved from the repository");

                Assert.IsTrue(returnValueTracker.Contains(retrievedEntry), "The retrieved has already occured in the return value.");

                returnValueTracker.Remove(retrievedEntry);

                i++;
            }

            Assert.IsTrue(returnValueTracker.Count == 0, "Not all mock entries were contained in the return value.");
        }

        [TestMethod]
        public void GetTest()
        {
            //Expected Behaviours:
            //  -1) Returned entries are only retrieved from the mocked source.
            //      Test: Check each returned entry against the mocked source. Fail if it does not occur in the source.
            //  -2) Returned entries are only retrieved once from the mocked source.
            //      Test: Track which entries are returned. Each should be from the mocked source and only occur once.
            //  -3) Returned entries are filtered properly by the supplied predicate.
            //      Test: Check the returned entries against the expected filtered entries. Fail if the results are not as expected.
            //  -4) Callbacked entries are only called on EXPECTED mock entries retrieved from the mocked source.
            //      Test: Check each callbacked entry against the EXPECTED mocked entries. Fail if it does not occur in the expected results.
            //  -5) Callbacked entries are only called once for each EXPECTED mock entry.
            //      Test: Track which entries are callbacked. Each should be from the EXPECTED entries and occur only once.
            //  -6) Callbacked entries are filtered properly by the supplied predicate        
            //      Test: Check the returned entries against the EXPECTED filtered entries. Fail if the results are not as expected.
           
            //SETUP: Define a filter.
            Func<MockEntry, bool> filter = (mockEntry) => mockEntry.Name.Length > 7;

            //SETUP: Manually produce a filtered list using the filter.
            var expectedEntries = 
                new List<MockEntry>(mockEntries)
                .Where(filter)
                .ToList();

            //SETUP: Clone the expected entries into the trackers.
            var callbackEntriesTracker = new List<MockEntry>(expectedEntries);
            var returnEntriesTracker = new List<MockEntry>(expectedEntries);

            
            //SETUP: Define a callback
            bool callbackCalled = false;
            Action<MockEntry> callback =
                (retrievedEntry) =>
                {
                    callbackCalled = true;
                    //TEST: Check if the entry is in the unfiltered list. Helpful Assertion if the filtration returns badly.
                    Assert.IsTrue(mockEntries.Contains(retrievedEntry), "Callback returned an unknown mock entry (not even in unfiltered entries)");

                    // -4) TEST: Check each callbacked entry against the expected FILTERED mocked entries. Fail if it does not occur in the expected results.
                    Assert.IsTrue(expectedEntries.Contains(retrievedEntry), "Callback returned an unknown mock entry (may be from unfiltered?)");

                    // -5) TEST: Track which entries are callbacked. Each should be from the EXPECTED entries and occur only once.
                    Assert.IsTrue(callbackEntriesTracker.Contains(retrievedEntry), "The retrieved entry has already been callbacked");

                    //Remove the entry from the tracker
                    callbackEntriesTracker.Remove(retrievedEntry);
                };

            //SETUP: Retrieve a mocked repository.
            var repository = mockRepository();

            //EXECUTION: Call the Get(filter, callback) method.
            var reposEntries = repository.Get(filter, callback);

            //TEST: Track which mocked entries have been callbacked. All mocked entries should go through the callback.
            Assert.IsTrue(callbackCalled, "Callback was not called");
            Assert.IsTrue(callbackEntriesTracker.Count == 0, "Not all mock entries went through the callback");

            int i = 0;
            foreach (MockEntry retrievedEntry in reposEntries)
            {
                //-4) Test: Check each return entry against the mocked source. Track which entries have been checked.
                Assert.IsTrue(expectedEntries.Contains(retrievedEntry), "Unknown mock entry was retrieved from the repository");

                Assert.IsTrue(returnEntriesTracker.Contains(retrievedEntry), "The retrieved has already occured in the return value.");

                returnEntriesTracker.Remove(retrievedEntry);

                i++;
            }

            Assert.IsTrue(returnEntriesTracker.Count == 0, "Not all mock entries were contained in the return value.");                        
        }
    }
}
