using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FacultyAPR.Storage.Blob.Tests
{
    [TestClass]
    public class IBlobStoreTests
    {
        [TestMethod]
        public async Task ReadToArrayAsync_ConvertsStreamToByteArray_ContentsMatch()
        {
            var testStream = new MemoryStream();
            var testData = "TestData";
            var bytes = Encoding.ASCII.GetBytes(testData);
            await testStream.WriteAsync(bytes, 0, bytes.Length);
            testStream.Seek(0, SeekOrigin.Begin); 
            var store = new Mock<IBlobStore>();
            store
                .Setup(m => m.ReadBlob(It.IsAny<string>()))
                .Returns(Task.FromResult<Stream>(testStream));
            
            var result = await IBlobStoreExtensions.ReadToArrayAsync(store.Object, "Doesn'tMatter");
            var resultStr = System.Text.Encoding.Default.GetString(result);
            
            Assert.AreEqual(testData, resultStr);
        }
        
    }
}
