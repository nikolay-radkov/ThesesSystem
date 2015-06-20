namespace ThesesSystem.Web.Tests.Infrastructure
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ThesesSystem.Web.Infrastructure.Constants;

    [TestClass]
    public class ReturningMimeTypeTests
    {
        [TestMethod]
        public void MimeTypeCreatorMustReturnProperStringWhenParameterIs7z()
        {
            var actual = MimeTypeCreator.GetFileMimeType("7z");
            var expected = "application/x-7z-compressed";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MimeTypeCreatorMustReturnProperStringWhenParameterIsRar()
        {
            var actual = MimeTypeCreator.GetFileMimeType("rar");
            var expected = "application/x-rar-compressed";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MimeTypeCreatorMustReturnProperStringWhenParameterIsZip()
        {
            var actual = MimeTypeCreator.GetFileMimeType("zip");
            var expected = "application/zip";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MimeTypeCreatorMustReturnProperStringWhenParameterIsTar()
        {
            var actual = MimeTypeCreator.GetFileMimeType("tar");
            var expected = "application/x-tar";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MimeTypeCreatorMustNotReturnMimeTypeForBzip2WhenParameterIsBzip()
        {
            var actual = MimeTypeCreator.GetFileMimeType("bzip");
            var expected = "application/x-bzip2";

            Assert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        public void MimeTypeCreatorMustReturnOctetMimeTypeWhenParameterIsUnknown()
        {
            var actual = MimeTypeCreator.GetFileMimeType("docx");
            var expected = System.Net.Mime.MediaTypeNames.Application.Octet;

            Assert.AreEqual(expected, actual);
        }
    }
}
