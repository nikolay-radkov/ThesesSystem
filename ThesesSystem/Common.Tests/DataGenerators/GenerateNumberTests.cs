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
    public class GenerateNumberTests
    {
        [TestMethod]
        public void GenerateNumberShouldReturnNumberInIntervalFrom0To10()
        {
            var generator = DefaultRandomGenerator.Instance;

            var number = generator.GenerateNumber(0, 10);

            Assert.IsTrue(number >= 0);
            Assert.IsTrue(number <= 10);
        }

                [TestMethod]
        public void GenerateNumberShouldReturnNumberGreaterThan100()
        {
            var generator = DefaultRandomGenerator.Instance;

            var number = generator.GenerateNumber(100);

            Assert.IsTrue(number >= 100);
        }

                [TestMethod]
        public void GenerateNumberShouldReturnANumber()
        {
            var generator = DefaultRandomGenerator.Instance;

            var number = generator.GenerateNumber();

            Assert.IsInstanceOfType(number, typeof(int));
        }
    }
}
