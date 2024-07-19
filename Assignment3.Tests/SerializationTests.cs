using NUnit.Framework;
using System.Reflection.Metadata;
using Assignment3.Utility; 

namespace Assignment3.Tests
{
    public class SerializationTests
    {
        private SLL users;
        private readonly string testFileName = "test_users.bin";
        private User testAdd = new User(1, "Adrian Capuno", "acapuno@gmail.com", "password");

        [SetUp]
        public void Setup()
        {
            // Uncomment the following line
            this.users = new SLL();

            users.AddLast(new User(1, "Joe Blow", "jblow@gmail.com", "password"));
            users.AddLast(new User(2, "Joe Schmoe", "joe.schmoe@outlook.com", "abcdef"));
            users.AddLast(new User(3, "Colonel Sanders", "chickenlover1890@gmail.com", "kfc5555"));
            users.AddLast(new User(4, "Ronald McDonald", "burgers4life63@outlook.com", "mcdonalds999"));
        }

        [TearDown]
        public void TearDown()
        {
            this.users.Clear();
        }

        /// <summary>
        /// Tests the object was serialized.
        /// </summary>
        [Test]
        public void TestSerialization()
        {
            SerializationHelper.SerializeUsers(users, testFileName);
            Assert.IsTrue(File.Exists(testFileName));
        }

        /// <summary>
        /// Tests the object was deserialized.
        /// </summary>
        [Test]
        public void TestDeSerialization()
        {
            SerializationHelper.SerializeUsers(users, testFileName);
            
            SLL deserializedUsers = SerializationHelper.DeserializeUsers(testFileName);

            Assert.IsTrue(users.Count() == deserializedUsers.Count());

            for (int i = 0; i < users.Count(); i++)
            {
                User expected = users.GetValue(i);
                User actual = deserializedUsers.GetValue(i);

                Assert.AreEqual(expected.Id, actual.Id);
                Assert.AreEqual(expected.Name, actual.Name);
                Assert.AreEqual(expected.Email, actual.Email);
                Assert.AreEqual(expected.Password, actual.Password);
            }
        }

        [Test]
        public void ToArray_ReturnsArrayRepresentationOfList()
        {
            SerializationHelper.SerializeUsers(users, testFileName);
            ILinkedListADT deserializedUsers = SerializationHelper.DeserializeUsers(testFileName);

            User[] originalArray = users.ToArray();
            User[] deserializedArray = deserializedUsers.ToArray(); 

            Assert.That(deserializedArray.Length, Is.EqualTo(originalArray.Length), "Arrays should have the same length.");

            for (int i = 0; i < originalArray.Length; i++)
            {
                Assert.That(deserializedArray[i].Id, Is.EqualTo(originalArray[i].Id), $"User Id at index {i} should match.");
                Assert.That(deserializedArray[i].Name, Is.EqualTo(originalArray[i].Name), $"User Name at index {i} should match.");
                Assert.That(deserializedArray[i].Email, Is.EqualTo(originalArray[i].Email), $"User Email at index {i} should match.");
                Assert.That(deserializedArray[i].Password, Is.EqualTo(originalArray[i].Password), $"User Password at index {i} should match.");
            }
        }

        [Test]        
        public void Prepend_AddInitialItems()
        {
            ILinkedListADT testuser;
            testuser = new SLL();

            testuser.AddFirst(testAdd);
            Assert.AreEqual(testAdd, testuser.GetValue(0));
        }

        [Test]
        public void Prepend_AddsItemToFrontOfList()
        {
            SerializationHelper.SerializeUsers(users, testFileName);
            users.Add(testAdd, 0);
            User actual = users.GetValue(0);

            Assert.AreEqual(testAdd, actual);
        }

        [Test]
        public void Append_AddsItemToEndOfList()
        {
            users.AddLast(testAdd);
            Assert.AreEqual(testAdd, users.GetValue(users.Count()-1));
        }

        [Test]
        [TestCase(1)]     
        [TestCase(2)]      
        public void RemoveAt_RemovesItemAtIndex(int index)
        {
            SerializationHelper.SerializeUsers(users, testFileName);
            ILinkedListADT deserializedUsers = SerializationHelper.DeserializeUsers(testFileName);

            users.Remove(index);

            User expected = users.GetValue(index);
            User actual = deserializedUsers.GetValue(index+1);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RemoveAt_RemoveItemsAtStart()
        {
            SerializationHelper.SerializeUsers(users, testFileName);
            SLL deserializedUsers = SerializationHelper.DeserializeUsers(testFileName);
            users.RemoveFirst();

            User expected = users.GetValue(0);
            User actual = deserializedUsers.GetValue(1);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RemoveAt_RemoveItemsAtEnd()
        {
            SerializationHelper.SerializeUsers(users, testFileName);
            SLL deserializedUsers = SerializationHelper.DeserializeUsers(testFileName);
            users.RemoveLast();

            int index = users.Count() -1;
            User expected = users.GetValue(index);
            User actual = deserializedUsers.GetValue(index);

            Assert.AreEqual(users.Count(), deserializedUsers.Count() -1);
            Assert.AreEqual(expected.Name, actual.Name);

        }

        [Test]
        public void RemoveAtusersIsEmpty_ThrowsException()
        {
            users.Clear();
            Assert.Throws<InvalidOperationException>(() => users.RemoveLast(), "List is empty.");
        }

        [Test]
        [TestCase(1)]     
        [TestCase(2)]      
        public void InsertAt_InsertsItemAtSpecificIndex(int index)
        {
            SerializationHelper.SerializeUsers(users, testFileName);
            users.Add(testAdd, index);

            User actual = users.GetValue(index);

            Assert.AreEqual(testAdd, actual);
        }

        [Test]
        public void Size_ReturnsCorrectSize()
        {
            Assert.AreEqual(4, users.Count());
        }

        [Test]
        public void Clear_ClearsTheList()
        {
            users.Clear();
            Assert.AreEqual(0, users.Count());
        }

        [Test]
        public void Contains_ReturnsTrueIfItemExists()
        {
            users.AddLast(testAdd);
            Assert.IsTrue(users.Contains(testAdd));
        }

        [Test]
        public void IsEmpty_Initially_ReturnsTrue()
        {
            ILinkedListADT testuser;
            testuser = new SLL();

            Assert.IsTrue(testuser.IsEmpty(), "List should be empty initially.");
        }

        [Test]
        public void IsEmpty_AfterAddingItem_ReturnsFalse()
        {
            users.Clear();
            users.AddLast(testAdd);
            Assert.IsFalse(users.IsEmpty(), "List should not be empty after adding an item.");
        }

        [Test]
        public void GetFirst_ReturnsFirstItem()
        {
            SerializationHelper.SerializeUsers(users, testFileName);
            SLL deserializedUsers = SerializationHelper.DeserializeUsers(testFileName);

            User expected = users.GetValue(0);
            User actual = deserializedUsers.GetValue(1);

            Assert.AreEqual(deserializedUsers.GetValue(0), users.GetValue(0), "Getvalue should return value of first item.");
        }

        [Test]
        public void Reverse_EmptyList_KeepsEmpty()
        {
            var list = new SLL();
            list.Reverse();
            Assert.IsTrue(list.IsEmpty());
        }

        [Test]
        public void Reverse_SingleElementList_KeepsSame()
        {
            var list = new SLL();
            list.AddLast(testAdd);
            list.Sort();
            Assert.AreEqual("Adrian Capuno", list.GetValue(0).Name);
        }

        [Test]
        public void Reverse_MultipleElements_ReversesList()
        {

            SerializationHelper.SerializeUsers(users, testFileName);
            SLL deserializedUsers = SerializationHelper.DeserializeUsers(testFileName);

            users.Reverse();

            User expected = users.GetValue(0);
            User actual = deserializedUsers.GetValue(users.Count()-1);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Sort_EmptyList_KeepsEmpty()
        {
            var list = new SLL();
            list.Sort();
            Assert.IsTrue(list.IsEmpty());
        }

        [Test]
        public void Sort_SingleElementList_KeepsSame()
        {
            var list = new SLL();

            list.AddLast(testAdd);
            list.Sort();
            Assert.AreEqual("Adrian Capuno", list.GetValue(0).Name);
        }

        [Test]
        public void Sort_MultipleElements_SortsList()
        {
            users.Sort();

            User expected = users.GetValue(0);

            Assert.AreEqual("Colonel Sanders", expected.Name);
        }
    }
}
