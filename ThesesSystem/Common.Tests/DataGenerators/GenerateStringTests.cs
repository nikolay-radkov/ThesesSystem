namespace Common.Tests.DataGenerators
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ThesesSystem.Common.DataGenerators;

    [TestClass]
    public class GenerateStringTests
    {
        [TestMethod]
        public void GenerateStringShouldReturnStringWithLengthInIntervalFrom0To10()
        {
            var generator = DefaultRandomGenerator.Instance;

            var str = generator.GenerateString(0, 10);

            Assert.IsTrue(str.Length >= 0);
            Assert.IsTrue(str.Length <= 10);
        }

        [TestMethod]
        public void GenerateStringShouldReturnStringWithLengthGreaterThan100()
        {
            var generator = DefaultRandomGenerator.Instance;

            var str = generator.GenerateString(100, 200);

            Assert.IsTrue(str.Length >= 100);
        }

        [TestMethod]
        public void GenerateStringShouldReturnAString()
        {
            var generator = DefaultRandomGenerator.Instance;

            var str = generator.GenerateString(2, 3);

            Assert.IsInstanceOfType(str, typeof(string));
        }
    }
}
