using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThesesSystem.Common.DataGenerators;

namespace Common.Tests.DataGenerators
{

    [TestClass]
    public class GenerateRealNumberTests
    {
        [TestMethod]
        public void GenerateRealNumberShouldReturnRealNumberInIntervalFrom5To50()
        {
            var generator = DefaultRandomGenerator.Instance;

            var RealNumber = generator.GenerateRealNumber(5, 50);

            Assert.IsTrue(RealNumber >= 5);
            Assert.IsTrue(RealNumber <= 50);
        }

        [TestMethod]
        public void GenerateRealNumberShouldReturnRealNumberGreaterThan500()
        {
            var generator = DefaultRandomGenerator.Instance;

            var RealNumber = generator.GenerateRealNumber(500);

            Assert.IsTrue(RealNumber >= 500);
        }

        [TestMethod]
        public void GenerateRealNumberShouldReturnARealNumber()
        {
            var generator = DefaultRandomGenerator.Instance;

            var RealNumber = generator.GenerateRealNumber();

            Assert.IsInstanceOfType(RealNumber, typeof(double));
        }
    }
}
