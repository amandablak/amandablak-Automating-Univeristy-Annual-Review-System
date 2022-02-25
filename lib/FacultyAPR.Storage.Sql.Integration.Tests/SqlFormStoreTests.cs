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
    public class SqlFormStoreTests
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
        public async Task CreateStructure_GetStructure_SameObject()
        {
            var formId = Guid.NewGuid();
            var sectionId = Guid.NewGuid();
            var structure = FormDataGenerators.GenerateFormStructure(formId, sectionId);
            
            var store = (IFormStructureStore)(new SqlFormStore(_optionsMock.Object));
            await store.Create(structure);

            var result = await store.Get(structure.FormId);
            Assert.AreEqual(structure, result);
        }

        [TestMethod]
        public async Task UpdateStructure_CreateStructure_SameObject()
        {
            var formId = Guid.NewGuid();
            var sectionId = Guid.NewGuid();
            var structure = FormDataGenerators.GenerateFormStructure(formId, sectionId);

            var store = (IFormStructureStore)(new SqlFormStore(_optionsMock.Object));
            await store.Create(structure);
            var newStructure = FormDataGenerators.GenerateFormStructure(formId, sectionId);
    
            await store.Update(newStructure);
            Assert.AreNotEqual(newStructure, structure);
        }

            [TestMethod]
        public async Task GetStructure_CreateStructure_SameObject()
        {
            var formId = Guid.NewGuid();
            var sectionId = Guid.NewGuid();
            var structure = FormDataGenerators.GenerateFormStructure(formId, sectionId);
            
            var store = (IFormStructureStore)(new SqlFormStore(_optionsMock.Object));
            await store.Create(structure);

            var result = await store.Get(structure.FormId);
            Assert.AreEqual(structure, result);
        }
        [TestMethod]
        public async Task UpdateStructure_GetStructure_SameObject()
        {
            var formId = Guid.NewGuid();
            var sectionId = Guid.NewGuid();
            var structure = FormDataGenerators.GenerateFormStructure(formId, sectionId);
            
            var store = (IFormStructureStore)(new SqlFormStore(_optionsMock.Object));
            await store.Update(structure);

            var result = await store.Get(structure.FormId);
            Assert.AreNotEqual(structure, result);
        }
        [TestMethod]
        public async Task CreateContent_GetContent_SameObject()
        {
             var formId = Guid.NewGuid();
             var sectionId = Guid.NewGuid();
             var content = FormDataGenerators.GenerateFormContent(formId, sectionId);
            
             var store = (IFormContentStore)(new SqlFormStore(_optionsMock.Object));
             await store.Create(content.FacultyId , content);

             var result = await store.Get(content.FacultyId, content.FormId);
             Assert.AreEqual(content, result);
        }

        [TestMethod]
        public async Task UpdateContent_CreateContent_SameObject()
        {
            var formId = Guid.NewGuid();
            var sectionId = Guid.NewGuid();
            var content = FormDataGenerators.GenerateFormContent(formId, sectionId);
            
            var store = (IFormContentStore)(new SqlFormStore(_optionsMock.Object));
            await store.Create(content.FacultyId , content);


            var newContent = FormDataGenerators.GenerateFormContent(formId, sectionId);
            var updatedReturn = await store.Update(newContent.FacultyId, newContent.FormId, newContent);

            Assert.AreNotEqual(newContent, content);
            Assert.AreEqual(newContent, updatedReturn);
        }

        [TestMethod]
        public async Task GetContent_CreateContent_SameObject()
        {
             var formId = Guid.NewGuid();
             var sectionId = Guid.NewGuid();
             var content = FormDataGenerators.GenerateFormContent(formId, sectionId);
            
             var store = (IFormContentStore)(new SqlFormStore(_optionsMock.Object));
             await store.Create(content.FacultyId , content);

             var result = await store.Get(content.FacultyId, content.FormId);
             Assert.AreEqual(content, result);
        }

        [TestMethod]
        public async Task CreateContent_ContentGood_SameObject()
        {
            var formId = Guid.NewGuid();
            var sectionId = Guid.NewGuid();
            var content = FormDataGenerators.GenerateFormContent(formId, sectionId);
            
            var store = (IFormContentStore)(new SqlFormStore(_optionsMock.Object));
            var result = await store.Create(content.FacultyId , content);
            
            Assert.AreEqual(content, result);
        }
        [TestMethod]
        public async Task CreateStructure_StructureGood_SameObject()
        {
            var formId = Guid.NewGuid();
            var sectionId = Guid.NewGuid();
            var structure = FormDataGenerators.GenerateFormStructure(formId, sectionId);
            
            var store = (IFormStructureStore)(new SqlFormStore(_optionsMock.Object));
            var result = await store.Create(structure);
            
            Assert.AreEqual(structure, result);
        }
        [TestMethod]
        public async Task GetAll_ValidFacultyId_Stubs()
        {
            var formId = Guid.NewGuid();
            var sectionId = Guid.NewGuid();
            var structure = FormDataGenerators.GenerateFormStructure(formId, sectionId);
            var content = FormDataGenerators.GenerateFormContent(formId, sectionId);
            var store = new SqlFormStore(_optionsMock.Object);
            var newStructure = await ((IFormStructureStore)store).Create(structure);
            content.FormId = newStructure.FormId;
            await ((IFormContentStore)store).Create(content.FacultyId , content);

            var result = await ((IFormContentStore)store).GetAll(content.FacultyId);
            
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public async Task GetAll_InValidFacultyId_EmptyList()
        {
            var formId = Guid.NewGuid();
            var sectionId = Guid.NewGuid();
            var structure = FormDataGenerators.GenerateFormStructure(formId, sectionId);
            var content = FormDataGenerators.GenerateFormContent(formId, sectionId);
            var store = new SqlFormStore(_optionsMock.Object);
            var newStructure = await ((IFormStructureStore)store).Create(structure);
            content.FormId = newStructure.FormId;
            await ((IFormContentStore)store).Create(content.FacultyId , content);

            var result = await ((IFormContentStore)store).GetAll(Guid.NewGuid());
            
            Assert.AreEqual(0, result.Count());   
        }
        
        [TestMethod]
        public async Task DeleteStructure_DeleteStructure_True()
        {
            var formId = Guid.NewGuid();
            var sectionId = Guid.NewGuid();
            var structure = FormDataGenerators.GenerateFormStructure(formId, sectionId);
            
            var store = (IFormStructureStore)(new SqlFormStore(_optionsMock.Object));
            await store.Create(structure);

            var result = await store.Delete(structure.FormId);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task DeleteStructure_DeleteStructure_NotFound()
        {
            var formId = Guid.NewGuid();
            var sectionId = Guid.NewGuid();
            var structure = FormDataGenerators.GenerateFormStructure(formId, sectionId);

            var store = (IFormStructureStore)(new SqlFormStore(_optionsMock.Object));
            var result = await store.Delete(structure.FormId);

            Assert.IsTrue(result);
        }
    }
}
