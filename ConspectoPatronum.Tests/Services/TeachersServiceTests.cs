using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConspectoPatronum.Services;
using ConspectoPatronum.Core.Repositories;
using Moq;
using ConspectoPatronum.Domain;
using System.Collections.Generic;
using System.Linq;

namespace ConspectoPatronum.Tests.Services
{
    [TestClass]
    public class TeachersServiceTests
    {
        private TeachersService _teachersService;
        private Mock<IRepository<Teacher>> _mock;

        [TestInitialize]
        public void TeachersServiceTestsInitialize()
        {
            var teachers = new List<Teacher>()
            {
                new Teacher()
                {
                    Id = 1,
                    Name = "Макаренко",
                    Category = TeacherCategory.Maniac,
                    Photo = new Image()
                }
            };

            _mock = new Mock<IRepository<Teacher>>();
            _mock.Setup(m => m.Add(It.IsAny<Teacher>())).Callback<Teacher>(teachers.Add);
            _mock.Setup(m => m.Count()).Returns(teachers.Count);
            _mock.Setup(m => m.Delete(It.IsAny<int>()))
                .Callback<int>(id =>
                {
                    var teacher = teachers.Find(item => item.Id == id);
                    teachers.Remove(teacher);
                });
            _mock.Setup(m => m.GetAll()).Returns(teachers.AsQueryable<Teacher>());
            _mock.Setup(m => m.GetById(It.IsAny<int>()))
                .Returns<int>(id => teachers.Find(item => item.Id == id));
            _mock.Setup(m => m.SaveOrUpdate(It.IsAny<Teacher>()))
                .Callback<Teacher>(teacher =>
                {
                    var oldTeacher = teachers.Find(item => item.Id == teacher.Id);
                    if (oldTeacher != null)
                    {
                        teachers.Remove(oldTeacher);
                    }
                    teachers.Add(teacher);
                });
            _teachersService = new TeachersService(_mock.Object);
        }

        [TestMethod]
        public void AddTeacherTest()
        {
            var teacher = new Teacher();
            _teachersService.Add(teacher);
            _mock.Verify(item => item.Add(teacher), Times.Once());
        }

        [TestMethod]
        public void CountTeachersTest()
        {
            var count = _teachersService.Count();
            Assert.AreEqual(1, count);
            _mock.Verify(item => item.Count(), Times.Once);
        }

        [TestMethod]
        public void DeleteTeacherTest()
        {
            _teachersService.Delete(1);
            _mock.Verify(item => item.Delete(1), Times.Once());
        }

        [TestMethod]
        public void GetAllTeachersTest()
        {
            var teachers = _teachersService.GetAll().ToList();
            Assert.AreEqual(1, teachers.Count);
            Assert.AreEqual("Макаренко", teachers[0].Name);
            _mock.Verify(item => item.GetAll(), Times.Once());
        }

        [TestMethod]
        public void GetTeacherByIdTest()
        {
            var teacher = _teachersService.GetById(1);
            _mock.Verify(m => m.GetById(1), Times.Once());
            Assert.AreEqual("Макаренко", teacher.Name);
        }

        [TestMethod]
        public void GetTeacherByNameTest()
        {
            var teacher = _teachersService.GetByName("Макаренко");
            Assert.AreEqual(1, teacher.Id);
            Assert.AreEqual(TeacherCategory.Maniac, teacher.Category);
            _mock.Verify(item => item.GetAll(), Times.Once());
        }

        [TestMethod]
        public void UpdateTeacherTest()
        {
            var newTeacher = new Teacher() { Id = 1 };
            _teachersService.Update(newTeacher);
            _mock.Verify(item => item.SaveOrUpdate(newTeacher), Times.Once());
        }
    }
}
