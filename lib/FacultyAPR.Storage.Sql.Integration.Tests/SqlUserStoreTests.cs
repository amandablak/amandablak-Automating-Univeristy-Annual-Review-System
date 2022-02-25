using Microsoft.VisualStudio.TestTools.UnitTesting;
using FacultyAPR.Models.Form;
using FacultyAPR.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using MySqlConnector;
using Microsoft.Extensions.Options;
using FacultyAPR.Storage.Sql;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using Moq;
using System.Linq;
using System.Data;
namespace FacultyAPR.Storage.Sql.Integration.Tests
{
    [TestClass]
    public class SqlUserStoreTests
    {
        private static SqlUserStoreOptions _options;

        private static Mock<IOptionsMonitor<SqlUserStoreOptions>> _optionsMock = new Mock<IOptionsMonitor<SqlUserStoreOptions>>();


        [TestInitialize]
        public async Task Setup()
        {
            var config = new ConfigurationBuilder()
            .AddJsonFile("config.json")
            .Build();
            _options = new SqlUserStoreOptions {ConnectionString = config["ConnectionString"]};
            StreamReader streamReader = File.OpenText("SetupTestDatabase.sql");
            string setupDbText = streamReader.ReadToEnd();
            using (MySqlConnection connection = new MySqlConnection(config["ConnectionString"]))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand(setupDbText, connection))
                {
                    await command.ExecuteReaderAsync();
                }
            }

            _optionsMock.Setup(m => m.CurrentValue).Returns(_options);
        }

        [TestMethod]
        public async Task Get_CreateGet_SameUser()
        {
            var user = GenerateUser();
            var store = new SqlUserStore(_optionsMock.Object);

            await store.Create(user);

            var result = await store.Get(user.EmailAddress);

            Assert.AreEqual(user, result.Single());

        }

        [TestMethod]
        public async Task Get_MultipleUserTypes_TwoUsers()
        {
            var user = GenerateUser();
            user.UserType = UserType.Faculty;
            var store = new SqlUserStore(_optionsMock.Object);

            await store.Create(user);
            var user2 = new User 
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailAddress = user.EmailAddress,
                UserType = UserType.Admin
            };
            await store.Create(user2);

            var result = await store.Get(user.EmailAddress);

            Assert.AreEqual(2, result.Count());

        }

        [TestMethod]
        public async Task Update_UpdateGet_SameUser()
        {
            var user = GenerateUser();
            var store = new SqlUserStore(_optionsMock.Object);

            await store.Create(user);

            var updatedUser = user;
            updatedUser.UserType = UserType.Admin;
            await store.Update(updatedUser);

            var result = await store.Get(user.EmailAddress);

            Assert.AreEqual(updatedUser, result.Single());
        }


        public async Task Remove_RemoveGet_NoUser()
        {
            var user = GenerateUser();
            var store = new SqlUserStore(_optionsMock.Object);

            await store.Create(user);

            await store.Remove(user);

            var result = await store.Get(user.EmailAddress);

            Assert.AreEqual(0, result.Count());
        }

        private static IUser GenerateUser()
        {
            return new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Quinn",
                LastName = "Wass",
                EmailAddress = "wassq@seattleu.edu",
                UserType = UserType.Faculty
            };
        }
    }
}
