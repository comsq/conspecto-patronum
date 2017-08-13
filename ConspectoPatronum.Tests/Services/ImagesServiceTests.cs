using ConspectoPatronum.Core.Extensions;
using ConspectoPatronum.Core.Repositories;
using ConspectoPatronum.Domain;
using ConspectoPatronum.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace ConspectoPatronum.Tests.Services
{
    [TestClass]
    public class ImagesServiceTests
    {
        private ImagesService _imagesService;
        private Mock<IRepository<Image>> _mock;

        [TestInitialize]
        public void ImagesServiceTestsInitialise()
        {
            var image1 = new Image()
            {
                Id = 1,
                FileName = "file1",
                Number = 1,
                Subject = new Subject() { Title = "Курс1" }
            };
            var image2 = new Image()
            {
                Id = 2,
                FileName = "file2",
                Number = 1,
                Subject = new Subject() { Title = "Курс2" }
            };
            var images = new List<Image>() { image1, image2 };

            _mock = new Mock<IRepository<Image>>();
            _mock.Setup(m => m.Add(It.IsAny<Image>())).Callback<Image>(images.Add);
            _mock.Setup(m => m.Count()).Returns(images.Count);
            _mock.Setup(m => m.Delete(It.IsAny<int>()))
                .Callback<int>(id =>
                {
                    var image = images.Find(item => item.Id == id);
                    images.Remove(image);
                });
            _mock.Setup(m => m.GetAll()).Returns(images.AsQueryable<Image>());
            _mock.Setup(m => m.GetById(It.IsAny<int>()))
                .Returns<int>(id => images.Find(item => item.Id == id));
            _mock.Setup(m => m.SaveOrUpdate(It.IsAny<Image>()))
                .Callback<Image>(image =>
                {
                    var oldImage = images.Find(item => item.Id == image.Id);
                    if (oldImage != null)
                    {
                        images.Remove(oldImage);
                    }
                    images.Add(image);
                });
            _imagesService = new ImagesService(_mock.Object);
        }

        [TestMethod]
        public void AddImageTest()
        {
            var image = new Image();
            _imagesService.Add(image);
            _mock.Verify(item => item.Add(image), Times.Once());
        }

        [TestMethod]
        public void CountImagesTest()
        {
            var count = _imagesService.Count();
            Assert.AreEqual(2, count);
            _mock.Verify(item => item.Count(), Times.Once());
        }

        [TestMethod]
        public void DeleteImageTest()
        {
            _imagesService.Delete(1);
            _mock.Verify(item => item.Delete(1), Times.Once());
        }

        [TestMethod]
        public void GetAllImagesTest()
        {
            var images = _imagesService.GetAll().ToList();
            Assert.AreEqual("file1", images
                .FirstOrDefault(image => image.Id == 1)
                .FileName);
            Assert.AreEqual("file2", images
                .FirstOrDefault(image => image.Id == 2)
                .FileName);
            _mock.Verify(item => item.GetAll(), Times.Once());
        }

        [TestMethod]
        public void GetImageByIdTest()
        {
            var image = _imagesService.GetById(1);
            _mock.Verify(m => m.GetById(1), Times.Once());
            Assert.AreEqual("file1", image.FileName);
        }

        [TestMethod]
        public void GetImageByFileNameTest()
        {
            var image = _imagesService.GetByFileName("file1");
            _mock.Verify(item => item.GetAll(), Times.Once());
            Assert.AreEqual(1, image.Id);
            Assert.AreEqual("Курс1", image.Subject.Title);
        }

        [TestMethod]
        public void GetImageBySubjectTest()
        {
            var images = _imagesService.GetBySubject("Курс1");
            Assert.AreEqual(1, images.Count);
            Assert.AreEqual("file1", images[0].FileName);
            _mock.Verify(item => item.GetAll(), Times.Once());
        }

        [TestMethod]
        public void UpdateImageTest()
        {
            var newImage = new Image()
            {
                Id = 1,
                FileName = "new file name",
                Subject = new Subject() { Title = "title" }
            };
            _imagesService.Update(newImage);
            _mock.Verify(item => item.SaveOrUpdate(newImage), Times.Once());
        }
    }
}
