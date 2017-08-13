using ConspectoPatronum.Core.Repositories;
using ConspectoPatronum.Core.Services;
using ConspectoPatronum.Domain;
using System.Collections.Generic;
using System.Linq;

namespace ConspectoPatronum.Services
{
    public class SubjectsService : ISubjectsService
    {
        private readonly IRepository<Subject> _repository;

        public SubjectsService(IRepository<Subject> repository)
        {
            _repository = repository;
        }

        public void Add(Subject subject)
        {
            _repository.Add(subject);
        }

        public int Count()
        {
            return _repository.Count();
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public IList<Subject> GetAll()
        {
            return _repository.GetAll()
                .OrderBy(subject => subject.Title)
                .ToList();
        }

        public Subject GetById(int id)
        {
            return _repository.GetById(id);
        }

        public IList<Subject> GetByTeacher(Teacher teacher)
        {
            return _repository.GetAll()
                .Where(subject => subject.Teacher.Name == teacher.Name)
                .OrderBy(subject => subject.Title)
                .ToList();
        }

        public IList<Subject> GetBySemester(Semester semester)
        {
            return _repository.GetAll()
                .Where(subject => subject.FromSemester <= semester
                    && semester <= subject.ToSemester)
                .OrderBy(subject => subject.Title)
                .ToList();
        }

        public Subject GetByTitle(string title)
        {
            return _repository.GetAll()
                .FirstOrDefault(subject => subject.Title == title);
        }

        public void Update(Subject subject)
        {
            _repository.SaveOrUpdate(subject);
        }
    }
}
