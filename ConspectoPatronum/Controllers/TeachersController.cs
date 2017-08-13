using ConspectoPatronum.Core.Services;
using ConspectoPatronum.Domain;
using ConspectoPatronum.Models;
using System;
using System.Web.Mvc;

namespace ConspectoPatronum.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ISubjectsService _subjectsService;
        private readonly ITeachersService _teachersService;

        public TeachersController(
            ISubjectsService subjectsService,
            ITeachersService teachersService)
        {
            _subjectsService = subjectsService;
            _teachersService = teachersService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("All");
        }

        public ActionResult All()
        {
            return View(_teachersService.GetAll());
        }

        public ActionResult Teacher(string name)
        {
            var teacher = _teachersService.GetByName(name);
            var teachersSubjectsViewModel = new TeachersSubjectsViewModel()
            {
                Teacher = teacher,
                Subjects = _subjectsService.GetByTeacher(teacher)
            };
            return View(teachersSubjectsViewModel);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult AddTeacher()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTeacher(Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return View(teacher);
            }
            _teachersService.Add(teacher);
            return Redirect("/Teachers/All");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult UpdateTeacher(string name)
        {
            var teacher = _teachersService.GetByName(name);
            return View(teacher);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateTeacher(Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return View(teacher);
            }
            _teachersService.Update(teacher);
            return Redirect(String.Format("/Teachers/Teacher?name={}", teacher.Name));
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteTeacher(string name)
        {
            var teacher = _teachersService.GetByName(name);
            _teachersService.Delete(teacher.Id);
            return Redirect("/Teachers/All");
        }
    }
}