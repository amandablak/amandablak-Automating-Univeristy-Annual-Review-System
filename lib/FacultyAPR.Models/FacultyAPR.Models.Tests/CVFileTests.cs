using FacultyAPR.Models.CV;
using System;
using System.IO;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FacultyAPR.Models.Tests
{
    [TestClass]
    public class CVFileTests
    {
        [TestMethod]
        public void CVFIle_NullStream_Throws()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(() => new CVFile(null, "string.pdf", DateTimeOffset.Now, Guid.NewGuid()));
            Assert.AreEqual("file", ex.ParamName);
        }

        [DataTestMethod]
        [DataRow(".png")]
        [DataRow(".jpeg")]
        [DataRow(".jpg")]
        public void CVFIle_InvalidExtension_Throws(string ext)
        {
            var stream = new Mock<Stream>();
            var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(() => new CVFile(stream.Object, "asd" + ext, DateTimeOffset.Now, Guid.NewGuid()));
            Assert.AreEqual($"File type .{ext} not allowed", ex.ParamName);
        }

        [DataTestMethod]
        [DataRow(".docx")]
        [DataRow(".doc")]
        [DataRow(".pdf")]
        [DataRow(".DOCX")]
        [DataRow(".DOC")]
        [DataRow(".PDF")]
        public void CVFIle_Happy_NoThrow(string ext)
        {
            var stream = new Mock<Stream>();
            new CVFile(stream.Object, "asd" + ext, DateTimeOffset.Now, Guid.NewGuid());
        }

        [TestMethod]
        public void CVFIle_DefaultYear_Throw()
        {
            var stream = new Mock<Stream>();
            var ex = Assert.ThrowsException<ArgumentNullException>(() => new CVFile(stream.Object, "asd.pdf", default, Guid.NewGuid()));
            Assert.AreEqual("formYear", ex.ParamName);
        }

        [TestMethod]
        public void CVFIle_EmptyFileName_Throw()
        {
            var stream = new Mock<Stream>();
            var ex = Assert.ThrowsException<ArgumentNullException>(() => new CVFile(stream.Object, "", DateTimeOffset.Now, Guid.NewGuid()));
            Assert.AreEqual("fileName", ex.ParamName);
        }

        [TestMethod]
        public void CVFIle_DefaultId_Throw()
        {
            var stream = new Mock<Stream>();
            var ex = Assert.ThrowsException<ArgumentNullException>(() => new CVFile(stream.Object, "asd.pdf", DateTimeOffset.Now, default));
            Assert.AreEqual("facultyId", ex.ParamName);
        }
    }
}
