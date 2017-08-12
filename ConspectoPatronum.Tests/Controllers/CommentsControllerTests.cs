using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConspectoPatronum.Core.Services;
using Moq;
using ConspectoPatronum.Domain;
using System.Collections.Generic;
using System.Linq;

namespace ConspectoPatronum.Tests.Controllers
{
    [TestClass]
    public class CommentsControllerTests
    {
        private Mock<ICommentsService> _commentsMock;

        [TestInitialize]
        public void CommentsControllerTestsInitialize()
        {
            var comments = new List<Comment>()
            {
                new Comment()
                {
                    Id = 1,
                    Author = "Автор",
                    PostedOn = DateTime.Now,
                    Text = "text",
                    Title = "Заголовок"
                }
            };
            _commentsMock = new Mock<ICommentsService>();
            _commentsMock.Setup(m => m.Add(It.IsAny<Comment>())).Callback<Comment>(comments.Add);
            _commentsMock.Setup(m => m.Count()).Returns(comments.Count);
            _commentsMock.Setup(m => m.Delete(It.IsAny<int>()))
                .Callback<int>(id =>
                {
                    var comment = comments.FirstOrDefault(item => item.Id == id);
                    comments.Remove(comment);
                });
            _commentsMock.Setup(m => m.GetAll()).Returns(comments);
            _commentsMock.Setup(m => m.GetById(It.IsAny<int>()))
                .Returns<int>(id => comments.FirstOrDefault(comment => comment.Id == id));
            _commentsMock.Setup(m => m.Update(It.IsAny<Comment>())).Callback<Comment>(comment =>
                {
                    var oldItem = comments.FirstOrDefault(item => item.Id == comment.Id);
                    comments.Remove(oldItem);
                    comments.Add(comment);
                });
        }
    }
}
