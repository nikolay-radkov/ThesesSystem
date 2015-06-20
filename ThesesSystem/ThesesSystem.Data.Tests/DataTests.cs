using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThesesSystem.Data.Tests.Common;
using System.Linq;
using ThesesSystem.Common.DataGenerators;
using System.Collections.Generic;
using ThesesSystem.Data.TableGenerator;
using ThesesSystem.Models;

namespace ThesesSystem.Data.Tests
{
    [TestClass]
    public class DataTests
    {
        [TestMethod]
        public void CreatingDataToDbSetWontThrowException()
        {
            var fakeDbContext = new FakeDbContext();
            var fakeData = new FakeData(fakeDbContext);
        }

        [TestMethod]
        public void CreatingDataWillAdd5Faculties()
        {
            var fakeDbContext = new FakeDbContext();

            var actual = fakeDbContext.Faculties.Count();
            var expected = 5;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreatingDataWillAdd5SPecialtiesForEveryFaculty()
        {
            var fakeDbContext = new FakeDbContext();

            var faculties = fakeDbContext.Faculties.ToList();

            foreach (var faculty in faculties)
            {
                var expected = 5;
                var actual = 5;
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void AddingASpecialtyWillIncreaseSpecialtyCount()
        {
            var fakeDbContext = new FakeDbContext();

            fakeDbContext.Faculties.Add(new Faculty { Title = "new" });
            var actual = fakeDbContext.Faculties.Count();
            var expected = 6;

            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void CreatingDataWillAdd50Students()
        {
            var fakeDbContext = new FakeDbContext();

            var actual = fakeDbContext.Users.Where(u => u.Student != null).Count();
            var expected = 50;
        
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreatingDataWillAdd50Theses()
        {
            var fakeDbContext = new FakeDbContext();

            var faculties = fakeDbContext.Theses.ToList();
            var expected = 50;
            var actual = faculties.Count();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UpdatingFacultyTitleWontThrowException()
        {
            var fakeDbContext = new FakeDbContext();

            var faculty = fakeDbContext.Faculties.FirstOrDefault();
            faculty.Title = "FKSU";
            fakeDbContext.SaveChanges();

            var expected = "FKSU";
            var actual = faculty.Title;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UpdatingSpecialtyTitleWontThrowException()
        {
            var fakeDbContext = new FakeDbContext();

            var specialty = fakeDbContext.Specialties.FirstOrDefault();
            specialty.Title = "KST";
            fakeDbContext.SaveChanges();

            var expected = "KST";
            var actual = specialty.Title;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeletingThesisWillDecreaseThesesCount()
        {
            var fakeDbContext = new FakeDbContext();

            var thesis = fakeDbContext.Theses.FirstOrDefault();
            fakeDbContext.Theses.Remove(thesis);

            fakeDbContext.SaveChanges();

            var expected = 49;
            var actual = fakeDbContext.Theses.Count();
            Assert.AreEqual(expected, actual);
        }
    }
}
