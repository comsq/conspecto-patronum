using ConspectoPatronum.Core.Services;
using ConspectoPatronum.Domain;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ConspectoPatronum.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly ISubjectsService _subjectsService;
        private readonly IImagesService _imagesService;

        public AdminController(
            ISubjectsService subjectsService,
            IImagesService imagesService)
        {
            _subjectsService = subjectsService;
            _imagesService = imagesService;
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddImage(Image image, int? previousId, int? nextId, string subjectTitle)
        {
            // TODO
            return Redirect(String.Format("/Subjects/Subject?title={}", subjectTitle));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddImages(IList<Image> images, int? previousId, int? nextId, string subjectTitle)
        {
            // TODO
            return Redirect(String.Format("/Subjects/Subject?title={}", subjectTitle));
        }
        
        public ActionResult DeleteImage(string fileName, string subjectTitle)
        {
            var image = _imagesService.GetByFileName(fileName);
            _imagesService.Delete(image.Id);
            return Redirect(String.Format("/Subjects/Subject?title={}", subjectTitle));
        }

        // TODO: Statistics
    }
}