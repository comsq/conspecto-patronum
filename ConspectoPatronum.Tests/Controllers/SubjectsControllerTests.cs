using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConspectoPatronum.Controllers;
using ConspectoPatronum.Core.Services;
using Moq;
using ConspectoPatronum.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ConspectoPatronum.Models;

namespace ConspectoPatronum.Tests.Controllers
{
    [TestClass]
    public class SubjectsControllerTests
    {
        private SubjectsController _controller;
        private Mock<ISubjectsService> _subjectsMock;
        private Mock<IImagesService> _imagesMock;

        [TestInitialize]
        public void SubjectsControllerTestsInitialize()
        {
            InitializeSubjectsMock();
            InitializeImagesMock();
            _controller = new SubjectsController(
                _subjectsMock.Object,
                _imagesMock.Object);
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

        private void InitializeImagesMock()
        {
            var images = new List<Image>()
            {
                new Image()
                {
                    Id = 1,
                    Subject = new Subject()
                    {
                        Title = "Алгебра"
                    },
                    FileName = "file",
                }
            };
            _imagesMock = new Mock<IImagesService>();
            _imagesMock.Setup(m => m.Add(It.IsAny<Image>())).Callback<Image>(images.Add);
            _imagesMock.Setup(m => m.Count()).Returns(images.Count);
            _imagesMock.Setup(m => m.Delete(It.IsAny<int>())).Callback<int>(id =>
                {
                    var item = images.Find(image => image.Id == id);
                    images.Remove(item);
                });
            _imagesMock.Setup(m => m.GetAll()).Returns(images);
            _imagesMock.Setup(m => m.GetById(It.IsAny<int>()))
                .Returns<int>(id => images.Find(image => image.Id == id));
            _imagesMock.Setup(m => m.GetBySubject(It.IsAny<string>()))
                .Returns<string>(title => images
                    .Where(image => image.Subject.Title == title)
                    .ToList());
            _imagesMock.Setup(m => m.Update(It.IsAny<Image>()))
                .Callback<Image>(image =>
                {
                    var oldItem = images.FirstOrDefault(item => item.Id == image.Id);
                    images.Remove(oldItem);
                    images.Add(image);
                });
        }

        [TestMethod]
        public void SubjectsAllTest()
        {
            var result = _controller.All() as ViewResult;
            _subjectsMock.Verify(item => item.GetAll(), Times.Once());
            Assert.IsTrue(result.Model is IList<Subject>);
            var subjects = (List<Subject>)result.Model;
            Assert.AreEqual(2, subjects.Count);
        }

        [TestMethod]
        public void SemesterTest()
        {
            var result = _controller.Semester(3) as ViewResult;
            _subjectsMock.Verify(item => item.GetBySemester(Semester.Third), Times.Once());
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Model is SemesterViewModel);
            var model = (SemesterViewModel)result.Model;
            Assert.AreEqual(1, model.Subjects.Count);
            Assert.AreEqual("Матанализ", model.Subjects[0].Title);
        }

        [TestMethod]
        public void SubjectTest()
        {
            var algebraTitle = "Алгебра";
            var result = _controller.Subject(algebraTitle) as ViewResult;
            _subjectsMock.Verify(item => item.GetByTitle(algebraTitle), Times.Once());
            Assert.IsTrue(result.Model is SubjectViewModel);
            var model = (SubjectViewModel)result.Model;
            Assert.AreEqual(algebraTitle, model.Subject.Title);
            Assert.AreEqual(1, model.Images.Count);
        }

        // TODO: CRUID tests
    }
}
