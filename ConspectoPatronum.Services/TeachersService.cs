using ConspectoPatronum.Core.Services;
using System.Collections.Generic;
using System.Linq;
using ConspectoPatronum.Domain;
using ConspectoPatronum.Core.Repositories;

namespace ConspectoPatronum.Services
{
    public class TeachersService : ITeachersService
    {
        private readonly IRepository<Teacher> _repository;

        public TeachersService(IRepository<Teacher> repository)
        {
            _repository = repository;
        }

        public void Add(Teacher teacher)
        {
            _repository.Add(teacher);
        }

        public int Count()
        {
            return _repository.Count();
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public IList<Teacher> GetAll()
        {
            return _repository.GetAll()
                .OrderBy(teacher => teacher.Name)
                .ToList();
        }

        public Teacher GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Teacher GetByName(string teacherName)
        {
            return _repository.GetAll()
                .FirstOrDefault(teacher => teacher.Name == teacherName);
        }

        public void Update(Teacher teacher)
        {
            _repository.SaveOrUpdate(teacher);
        }
    }
}
