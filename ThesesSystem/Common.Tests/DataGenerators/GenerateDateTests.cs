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
    public class GenerateDateTests
    {
        [TestMethod]
        public void GenerateDateShouldReturnDateGreaterThanNow()
        {
            var generator = DefaultRandomGenerator.Instance;

            var date = generator.GenerateDate(DateTime.Now);

            Assert.IsTrue((date - DateTime.Now).Days > 0);
        }
    }
}
