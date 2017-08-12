using ConspectoPatronum.Core.Services;
using ConspectoPatronum.Domain;
using System;
using System.Web.Mvc;

namespace ConspectoPatronum.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentsService _commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            _commentsService = commentsService;
        }

        // GetComments: returns a partial view for each comment

        public ActionResult AddComment()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(Comment comment, string subjectTitle)
        {
            if (!ModelState.IsValid)
            {
                return View(comment);
            }
            comment.PostedOn = DateTime.Now;
            _commentsService.Add(comment);
            return Redirect(String.Format("/Subjects/Subject?title={}", subjectTitle));
        }

        public ActionResult EditComment(int id)
        {
            var comment = _commentsService.GetById(id);
            return View(comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditComment(Comment comment, string subjectTitle)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _commentsService.Update(comment);
            return Redirect(String.Format("/Subjects/Subject?title={}", subjectTitle));
        }

        public ActionResult DeleteComment(int commentId, string subjectTitle)
        {
            _commentsService.Delete(commentId);
            return Redirect(String.Format("/Subjects/Subject?title={}", subjectTitle));
        }
    }
}