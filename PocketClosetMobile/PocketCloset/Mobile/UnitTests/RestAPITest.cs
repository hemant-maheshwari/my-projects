using System;
using NUnit.Framework;
using PocketCloset.Models;
using PocketCloset.Service;
using PocketCloset.Views;
using PocketCloset.Controller;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestFixture()]
    public class RestAPITest
    {
        RestAPIService restAPIService;
        RestAPICRUDService<User> restAPICRUDService;

        public RestAPITest()
        {
            restAPIService = new RestAPIService(); //pass localhost
            restAPICRUDService = new RestAPICRUDService<User>();
        }

        [Test()]
        public void testCheckUserAsync()
        {
            User testUser = new User("adriang99", "password");
            User expectedUser = new User("User", "Male", "Adrian", "Gallant", "4/14/2020", "adrian@gmail.com", "adriang99", "password");
            expectedUser.userId = 5;

            Assert.AreEqual(expectedUser.userId, restAPIService.checkUserAsync(testUser).Result.userId);
        }

        [Test()]
        public void testCheckUsernameAsync()
        {
            string testUsername = "adriang99";
            bool expectedBool = true;
            Assert.AreEqual(expectedBool, restAPIService.checkUsernameAsync(testUsername).Result);

        }

        [Test()]
        public void testGetUserFromUsernameAsync()
        {
            string testUsername = "adriang99";
            User expectedUser = new User("User", "Male", "Adrian", "Gallant", "4/14/2020", "adrian@gmail.com", "adriang99", "password");
            expectedUser.userId = 5;
            Assert.AreEqual(expectedUser.userId, restAPIService.getUserFromUsernameAsync(testUsername).Result.userId);
        }

        [Test()]
        public void testGetAllFollowersAsync()
        {
            int testUserId = 5;
            int expectedListLength = 6;
            Assert.AreEqual(expectedListLength, restAPIService.getAllFollowersAsync(testUserId).Result.Count);
        }

        [Test()]
        public void testGetAllFollowingAsync()
        {
            int testUserId = 5;
            int expectedListLength = 6;
            Assert.AreEqual(expectedListLength, restAPIService.getAllFollowingAsync(testUserId).Result.Count);
        }

       //[Test()]         ********COMMENTED AFTER SUCCESSFUL TEST SO IT DOESN'T TRY CREATING DUPLICATE USERS********
       //public void testCreateModelAsync()
       // {
       //     bool expectedResult = true;
       //     User testUser = new User("User", "Male", "Unit", "Test", "4/14/2020", "test@gmail.com", "unittest", "password");
       //     Assert.AreEqual(expectedResult, restAPICRUDService.createModelAsync(testUser).Result);

       // }

       [Test()]
       public void testGetModelAsync()
        {
            int testUserId = 5;
            string expectedUsername = "adriang99";
            Assert.AreEqual(expectedUsername, restAPICRUDService.getModelAsync(testUserId).Result.username);
        }

        [Test()]
        public void testUpdateModelAsync()
        {
            User expectedUser = new User("User", "Male", "Adrian", "Gallant", "10/01/1999", "adrian@gmail.com", "adriang99", "password");
            expectedUser.userId = 5;

            bool expectedResult = true;
            Assert.AreEqual(expectedResult, restAPICRUDService.updateModelAsync(expectedUser).Result);
        }

    }
}

