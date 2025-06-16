using Microsoft.VisualStudio.TestTools.UnitTesting;
using NonogramApp.Data;
using NonogramApp.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace NonogramTest.Integration
{
    [TestClass]
    [DoNotParallelize]
    public class DataManagerTests
    {
        private string testFilePath;

        [TestInitialize]
        public void Setup()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string sourcePath = Path.GetFullPath(Path.Combine(baseDir, @"..\..\..\TestData\testusers.json"));
            string tempPath = Path.GetFullPath(Path.Combine(baseDir, @"..\..\..\TestData\temp_users.json"));

            File.Copy(sourcePath, tempPath, overwrite: true);

            // use testusers.json
            DataManager.FilePath = Path.GetFullPath(tempPath);
            testFilePath = tempPath;
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Clean up the temp file after each test
            if (File.Exists(testFilePath))
            {
                File.Delete(testFilePath);
            }
        }

        [TestMethod]
        public void AddUser_ShouldAddNewUserToFile()
        {
            // Data for test 
            var user = new User { Username = "newuser", Password = "1234" };

            // Use DataManager function
            DataManager.AddUser(user);
            List<User> users = DataManager.LoadUsers();

            //Check if newuser is equal to whats inside the json
            Assert.IsTrue(users.Exists(u => u.Username == "newuser"));
        }

        [TestMethod]
        public void GetUserByUsername_ShouldReturnCorrectUser()
        {
            // Requires testuser1 in json file before test is run
            User? user = DataManager.GetUserByUsername("testuser1");

            // Check if the username is in the json file
            Assert.IsNotNull(user);
            Assert.AreEqual("testuser1", user.Username);
        }

        [TestMethod]
        public void NewUserId_ShouldHaveUniqueId()
        {
            // Data for test 
            var user1 = new User { Username = "newuser1", Password = "12345" };
            var user2 = new User { Username = "newuser2", Password = "12346" };
            var user3 = new User { Username = "newuser3", Password = "12347" };

            // Use DataManager function
            DataManager.AddUser(user1);
            DataManager.AddUser(user2);
            DataManager.AddUser(user3);
            List<User> users = DataManager.LoadUsers();

            // Check if the id for the new users are unique
            var userIds = users.Where(u => u.Username.StartsWith("newuser")).Select(u => u.Id).ToList();
            Assert.AreEqual(userIds.Count, userIds.Distinct().Count());
        }
    }
}

