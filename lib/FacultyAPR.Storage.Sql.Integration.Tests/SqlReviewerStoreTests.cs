using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MySqlConnector;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using Moq;
using System.Linq;
using FacultyAPR.Testing.Utilities;

namespace FacultyAPR.Storage.Sql.Integration.Tests
{
    [TestClass]
    public class SqlReviewerStoreTests
    {
        private static SqlFormStoreOptions _options;

        private static Mock<IOptionsMonitor<SqlFormStoreOptions>> _optionsMock = new Mock<IOptionsMonitor<SqlFormStoreOptions>>();


        [TestInitialize]
        public async Task Setup()
        {
            var config = new ConfigurationBuilder()
            .AddJsonFile("config.json")
            .Build();
            _options = new SqlFormStoreOptions {ConnectionString = config["ConnectionString"]};
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
        public async Task GetAll_GetExistingForm_ExpectedStub()
        {
            var formId = Guid.NewGuid();
            var sectionId = Guid.NewGuid();
            var structure = FormDataGenerators.GenerateFormStructure(formId, sectionId);
            var content = FormDataGenerators.GenerateFormContent(formId, sectionId);

            var structureStore = (IFormStructureStore)(new SqlFormStore(_optionsMock.Object));
            var contentStore = (IFormContentStore)structureStore;
            var reviewerStore = new SqlReviewerStore(_optionsMock.Object);
            await structureStore.Create(structure);
            await contentStore.Create(content.FacultyId, content);

            var result = await reviewerStore.GetAll(content.ReviewerId);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(formId, result.First().FormId);
            Assert.AreEqual(content.FacultyId, result.First().FacultyId);
            Assert.AreEqual(structure.FormYear, result.First().FormYear);
            Assert.AreEqual(content.State, result.First().State);
            Assert.AreEqual(structure.Rank, result.First().Rank);
        }
    }
}
