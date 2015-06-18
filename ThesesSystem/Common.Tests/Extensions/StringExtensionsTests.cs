namespace Common.Tests.Extensions
{

    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ThesesSystem.Common.Extensions;

    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void TruncateLongStringShouldReturnStringWithLength20()
        {
            var str = "0123456789012345678901234567890123456789";

            var result = str.TruncateLongString(20);
            var actual = 20;

            Assert.AreEqual(result.Length, actual);
        }

        [TestMethod]
        public void TruncateLongStringShouldReturnTruncatedString()
        {
            var str = "0123456789012345678901234567890123456789";

            var result = str.TruncateLongString(20);
            var actual = "01234567890123456...";

            Assert.AreEqual(result, actual);
        }

        [TestMethod]
        public void TruncateLongStringShouldReturnStringEndingWith3DotsWhenLengthIsGreaterThanParameter()
        {
            var str = "Master Joda";

            var actual = str.TruncateLongString(9);
            var expected = "Master...";

            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void TruncateLongStringShouldReturnSameString()
        {
            var str = "abcd";

            var result = str.TruncateLongString(10);

            Assert.AreEqual(result, str);
        }
    }
}
