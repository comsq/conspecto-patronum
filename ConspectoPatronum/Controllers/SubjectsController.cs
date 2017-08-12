using ConspectoPatronum.Core.Services;
using ConspectoPatronum.Domain;
using ConspectoPatronum.Models;
using System;
using System.Web.Mvc;

namespace ConspectoPatronum.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly ISubjectsService _subjectsService;
        private readonly IImagesService _imagesService;

        public SubjectsController(
            ISubjectsService subjectsService,
            IImagesService imagesService)
        {
            _subjectsService = subjectsService;
            _imagesService = imagesService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("All");
        }

        public ActionResult All()
        {
            return View(_subjectsService.GetAll());
        }

        public ActionResult Semester(int number = 1)
        {
            var semester = (Semester)number;
            var semesterViewModel = new SemesterViewModel()
            {
                Semester = semester,
                Subjects = _subjectsService.GetBySemester(semester)
            };
            return View(semesterViewModel);
        }

        public ActionResult Subject(string title)
        {
            var subjectViewModel = new SubjectViewModel()
            {
                Subject = _subjectsService.GetByTitle(title),
                Images = _imagesService.GetBySubject(title)
            };
            return View(subjectViewModel);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult AddSubject()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSubject(Subject subject)
        {
            if (!ModelState.IsValid)
            {
                return View(subject);
            }
            _subjectsService.Add(subject);
            return Redirect("/Subjects/All");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult UpdateSubject(string title)
        {
            var subject = _subjectsService.GetByTitle(title);
            return View(subject);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateSubject(Subject subject)
        {
            if (!ModelState.IsValid)
            {
                return View(subject);
            }
            _subjectsService.Update(subject);
            return Redirect(String.Format("/Subjects/Subject?title={}", subject.Title));
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteSubject(string title)
        {
            var subject = _subjectsService.GetByTitle(title);
            _subjectsService.Delete(subject.Id);
            return Redirect("/Subjects/All");
        }
    }
}