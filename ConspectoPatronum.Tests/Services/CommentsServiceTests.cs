using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConspectoPatronum.Services;
using ConspectoPatronum.Domain;
using Moq;
using ConspectoPatronum.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace ConspectoPatronum.Tests.Services
{
    [TestClass]
    public class CommentsServiceTests
    {
        private CommentsService _commentsService;
        private Mock<IRepository<Comment>> _mock;

        [TestInitialize]
        public void CommentsServiceTestsInitialize()
        {
            var comments = new List<Comment>()
            {
                new Comment()
                {
                    Id = 1,
                    Author = "Автор1",
                    PostedOn = DateTime.Now,
                    Text = "text",
                    Title = "Коммент1"
                }
            };
            _mock = new Mock<IRepository<Comment>>();
            _mock.Setup(m => m.Add(It.IsAny<Comment>())).Callback<Comment>(comments.Add);
            _mock.Setup(m => m.Count()).Returns(comments.Count);
            _mock.Setup(m => m.Delete(It.IsAny<int>()))
                .Callback<int>(id =>
                {
                    var comment = comments.Find(item => item.Id == id);
                    comments.Remove(comment);
                });
            _mock.Setup(m => m.GetAll()).Returns(comments.AsQueryable<Comment>());
            _mock.Setup(m => m.GetById(It.IsAny<int>()))
                .Returns<int>(id => comments.Find(item => item.Id == id));
            _mock.Setup(m => m.SaveOrUpdate(It.IsAny<Comment>()))
                .Callback<Comment>(comment =>
                {
                    var oldComment = comments.Find(item => item.Id == comment.Id);
                    if (oldComment != null)
                    {
                        comments.Remove(oldComment);
                    }
                    comments.Add(comment);
                });
            _commentsService = new CommentsService(_mock.Object);
        }

        [TestMethod]
        public void AddCommentTest()
        {
            var comment = new Comment();
            _commentsService.Add(comment);
            _mock.Verify(item => item.Add(comment), Times.Once());
        }

        [TestMethod]
        public void CountCommentsTest()
        {
            var count = _commentsService.Count();
            Assert.AreEqual(1, count);
            _mock.Verify(item => item.Count(), Times.Once());
        }

        [TestMethod]
        public void DeleteCommentTest()
        {
            _commentsService.Delete(1);
            _mock.Verify(item => item.Delete(1), Times.Once());
        }

        [TestMethod]
        public void GetAllCommentsTest()
        {
            var comments = _commentsService.GetAll().ToList();
            Assert.AreEqual(1, comments.Count);
            Assert.AreEqual("Автор1", comments[0].Author);
            _mock.Verify(item => item.GetAll(), Times.Once());
        }

        [TestMethod]
        public void GetCommentByIdTest()
        {
            var comment = _commentsService.GetById(1);
            _mock.Verify(m => m.GetById(1), Times.Once());
            Assert.AreEqual("Автор1", comment.Author);
            Assert.AreEqual("Коммент1", comment.Title);
            Assert.AreEqual("text", comment.Text);
        }

        [TestMethod]
        public void UpdateCommentTest()
        {
            var newComment = new Comment() { Id = 1 };
            _commentsService.Update(newComment);
            _mock.Verify(item => item.SaveOrUpdate(newComment), Times.Once());
        }
    }
}
