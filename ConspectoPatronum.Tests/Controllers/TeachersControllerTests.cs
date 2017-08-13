using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using ConspectoPatronum.Domain;
using Moq;
using ConspectoPatronum.Core.Services;
using ConspectoPatronum.Controllers;
using System.Web.Mvc;
using ConspectoPatronum.Models;

namespace ConspectoPatronum.Tests.Controllers
{
    [TestClass]
    public class TeachersControllerTests
    {
        private TeachersController _controller;
        private Mock<ISubjectsService> _subjectsMock;
        private Mock<ITeachersService> _teachersMock;

        [TestInitialize]
        public  void TeachersControllerTestsInitialize()
        {
            InitializeSubjectsMock();
            InitializeTeachersMock();
            _controller = new TeachersController(
                _subjectsMock.Object,
                _teachersMock.Object);
        }

        private void InitializeSubjectsMock()
        {
            var subjects = new List<Subject>()
            {
                new Subject()
                {
                    Id = 1,
                    Description = "...",
                    Difficulty = Difficulty.Hard,
                    FromSemester = Semester.First,
                    ToSemester = Semester.Second,
                    Title = "Алгебра",
                    Teacher = new Teacher() { Name = "Макаренко" }
                },
                new Subject()
                {
                    Title = "Матанализ",
                    Description = "...",
                    Difficulty = Difficulty.Hard,
                    FromSemester = Semester.First,
                    ToSemester = Semester.Third,
                    Teacher = new Teacher { Name = "Name" }
                }
            };
            _subjectsMock = new Mock<ISubjectsService>();
            _subjectsMock.Setup(m => m.Add(It.IsAny<Subject>())).Callback<Subject>(subjects.Add);
            _subjectsMock.Setup(m => m.Count()).Returns(subjects.Count());
            _subjectsMock.Setup(m => m.Delete(It.IsAny<int>())).Callback<int>(id =>
            {
                var subject = subjects.FirstOrDefault(item => item.Id == id);
                subjects.Remove(subject);
            });
            _subjectsMock.Setup(m => m.GetAll()).Returns(subjects);
            _subjectsMock.Setup(m => m.GetBySemester(It.IsAny<Semester>())).Returns<Semester>(
                semester => subjects
                    .Where(subject => subject.FromSemester <= semester
                        && semester <= subject.ToSemester)
                    .ToList());
            _subjectsMock.Setup(m => m.GetByTeacher(It.IsAny<Teacher>())).Returns<Teacher>(
                teacher => subjects
                    .Where(subject => subject.Teacher.Name == teacher.Name)
                    .ToList());
            _subjectsMock.Setup(m => m.GetByTitle(It.IsAny<string>())).Returns<string>(
                title => subjects.FirstOrDefault(subject => subject.Title == title));
            _subjectsMock.Setup(m => m.Update(It.IsAny<Subject>())).Callback<Subject>(
                subject =>
                {
                    var oldItem = subjects.FirstOrDefault(item => item.Id == subject.Id);
                    subjects.Remove(oldItem);
                    subjects.Add(subject);
                });
        }

        private void InitializeTeachersMock()
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
            _teachersMock = new Mock<ITeachersService>();
            _teachersMock.Setup(m => m.Add(It.IsAny<Teacher>())).Callback<Teacher>(teachers.Add);
            _teachersMock.Setup(m => m.Count()).Returns(teachers.Count());
            _teachersMock.Setup(m => m.Delete(It.IsAny<int>())).Callback<int>(id =>
            {
                var teacher = teachers.FirstOrDefault(item => item.Id == id);
                teachers.Remove(teacher);
            });
            _teachersMock.Setup(m => m.GetAll()).Returns(teachers);
            _teachersMock.Setup(m => m.GetByName(It.IsAny<string>())).Returns<string>(name =>
                teachers.FirstOrDefault(teacher => teacher.Name == name));
            _teachersMock.Setup(m => m.Update(It.IsAny<Teacher>())).Callback<Teacher>(teacher =>
            {
                var oldItem = teachers.FirstOrDefault(item => item.Id == teacher.Id);
                teachers.Remove(oldItem);
                teachers.Add(teacher);
            });
        }

        [TestMethod]
        public void TeachersTest()
        {
            var result = _controller.All() as ViewResult;
            _teachersMock.Verify(item => item.GetAll(), Times.Once());
            Assert.IsTrue(result.Model is IList<Teacher>);
            var teachers = (List<Teacher>)result.Model;
            Assert.AreEqual(1, teachers.Count);
        }

        [TestMethod]
        public void TeacherTest()
        {
            var teacherName = "Макаренко";
            var result = _controller.Teacher(teacherName) as ViewResult;
            _teachersMock.Verify(item => item.GetByName(teacherName), Times.Once());
            Assert.IsTrue(result.Model is TeachersSubjectsViewModel);
            var model = (TeachersSubjectsViewModel)result.Model;
            Assert.AreEqual(1, model.Subjects.Count);
            Assert.AreEqual("Алгебра", model.Subjects[0].Title);
            Assert.AreEqual(teacherName, model.Teacher.Name);
        }

        // TODO: CRUID tests
    }
}
