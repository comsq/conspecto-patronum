using ConspectoPatronum.Core.Services;
using ConspectoPatronum.Domain;
using System.Collections.Generic;
using System.Linq;
using ConspectoPatronum.Core.Repositories;

namespace ConspectoPatronum.Services
{
    public class ImagesService : IImagesService
    {
        private readonly IRepository<Image> _repository;

        public ImagesService(IRepository<Image> repository)
        {
            _repository = repository;
        }

        public void Add(Image image)
        {
            _repository.Add(image);
        }

        public int Count()
        {
            return _repository.Count();
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public IList<Image> GetAll()
        {
            return _repository.GetAll()
                .GroupBy(image => image.Subject.Title)
                .SelectMany(group => group.OrderBy(image => image.Number))
                .ToList();
        }

        public Image GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Image GetByFileName(string fileName)
        {
            return _repository.GetAll()
                .FirstOrDefault(image => image.FileName == fileName);
        }

        public IList<Image> GetBySubject(string title)
        {
            return _repository.GetAll()
                .Where(image => image.Subject.Title == title)
                .OrderBy(image => image.Number)
                .ToList();
        }

        public void Update(Image image)
        {
            _repository.SaveOrUpdate(image);
        }
    }
}
