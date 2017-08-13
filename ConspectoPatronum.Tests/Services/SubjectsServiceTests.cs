using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConspectoPatronum.Services;
using ConspectoPatronum.Core.Repositories;
using ConspectoPatronum.Domain;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace ConspectoPatronum.Tests.Services
{
    [TestClass]
    public class SubjectsServiceTests
    {
        private SubjectsService _subjectsService;
        private Mock<IRepository<Subject>> _mock;

        [TestInitialize]
        public void SubjectsServiceTestsInitialize()
        {
            var algebra = new Subject()
            {
                Id = 1,
                Title = "Алгебра",
                Teacher = new Teacher() { Name = "Эйлер" },
                Description = "Описание алгебры",
                Difficulty = Difficulty.Hard,
                FromSemester = Semester.First,
                ToSemester = Semester.Second
            };

            var analysis = new Subject()
            {
                Id = 2,
                Title = "Матан",
                Teacher = new Teacher() { Name = "Коши" },
                Description = "Описание матана",
                Difficulty = Difficulty.Death,
                FromSemester = Semester.First,
                ToSemester = Semester.Third
            };

            var subjects = new List<Subject>() { algebra, analysis };

            _mock = new Mock<IRepository<Subject>>();
            _mock.Setup(m => m.Add(It.IsAny<Subject>())).Callback<Subject>(subjects.Add);
            _mock.Setup(m => m.Count()).Returns(subjects.Count);
            _mock.Setup(m => m.Delete(It.IsAny<int>()))
                .Callback<int>(id =>
            {
                var subject = subjects.Find(item => item.Id == id);
                subjects.Remove(subject);
            });
            _mock.Setup(m => m.GetAll()).Returns(subjects.AsQueryable<Subject>());
            _mock.Setup(m => m.GetById(It.IsAny<int>()))
                .Returns<int>(id => subjects.Find(item => item.Id == id));
            _mock.Setup(m => m.SaveOrUpdate(It.IsAny<Subject>()))
                .Callback<Subject>(subject =>
                {
                    var oldSubject = subjects.Find(item => item.Id == subject.Id);
                    if (oldSubject != null)
                    {
                        subjects.Remove(oldSubject);
                    }
                    subjects.Add(subject);
                });

            _subjectsService = new SubjectsService(_mock.Object);
        }

        [TestMethod]
        public void AddSubjectTest()
        {
            var newSubject = new Subject()
            {
                Id = 0,
                Title = "Теория чисел",
                Teacher = new Teacher() { Name = "Ферма" },
                Description = "Описание теории чисел",
                Difficulty = Difficulty.Freebie,
                FromSemester = Semester.Fourth,
                ToSemester = Semester.Fourth
            };
            _subjectsService.Add(newSubject);
            _mock.Verify(item => item.Add(newSubject), Times.Once());
        }

        [TestMethod]
        public void DeleteSubjectTest()
        {
            _subjectsService.Delete(1);
            _mock.Verify(item => item.Delete(1), Times.Once());
        }

        [TestMethod]
        public void UpdateSubjectTest()
        {
            var newSubject = new Subject()
            {
                Id = 1,
                Description = "New description",
                Difficulty = Difficulty.Freebie,
                FromSemester = Semester.Fifth,
                ToSemester = Semester.Fifth,
                Teacher = new Teacher() { Name = "Name" },
                Title = "New title"
            };
            _subjectsService.Update(newSubject);
            _mock.Verify(item => item.SaveOrUpdate(newSubject), Times.Once());
        }

        [TestMethod]
        public void CountSubjectsTest()
        {
            var count = _subjectsService.Count();
            Assert.AreEqual(2, count);
            _mock.Verify(item => item.Count(), Times.Once());
        }

        [TestMethod]
        public void GetAllSubjectsTest()
        {
            var subjects = _subjectsService.GetAll().ToList();
            Assert.AreEqual("Алгебра", subjects
                .FirstOrDefault(subject => subject.Id == 1)
                .Title);
            Assert.AreEqual("Эйлер", subjects
                .FirstOrDefault(subject => subject.Id == 1)
                .Teacher.Name);
            Assert.AreEqual("Матан", subjects
                .FirstOrDefault(subject => subject.Id == 2)
                .Title);
            Assert.AreEqual("Коши", subjects
                .FirstOrDefault(subject => subject.Id == 2)
                .Teacher.Name);
            _mock.Verify(item => item.GetAll(), Times.Once());
        }

        [TestMethod]
        public void GetSubjectByIdTest()
        {
            var subject = _subjectsService.GetById(1);
            _mock.Verify(m => m.GetById(1), Times.Once());
            Assert.AreEqual("Алгебра", subject.Title);
        }

        [TestMethod]
        public void GetSubjectsByTeacherTest()
        {
            var teacher = new Teacher()
            {
                Id = 1,
                Category = TeacherCategory.Loyal,
                Name = "Эйлер",
                Photo = new Image()
            };
            var subjects = _subjectsService.GetByTeacher(teacher);
            Assert.AreEqual(1, subjects.Count);
            Assert.AreEqual("Алгебра", subjects[0].Title);
            _mock.Verify(item => item.GetAll(), Times.Once());
        }

        [TestMethod]
        public void GetSubjectsBySemesterTest()
        {
            var subjects = _subjectsService.GetBySemester(Semester.Third);
            Assert.AreEqual(1, subjects.Count);
            Assert.AreEqual("Матан", subjects[0].Title);
            _mock.Verify(item => item.GetAll(), Times.Once());
        }

        [TestMethod]
        public void GetSubjectByTitleTest()
        {
            var subject = _subjectsService.GetByTitle("Матан");
            Assert.AreEqual("Коши", subject.Teacher.Name);
            Assert.AreEqual(2, subject.Id);
            _mock.Verify(item => item.GetAll(), Times.Once());
        }
    }
}
