using ConspectoPatronum.Domain;
using System;
using System.Data.Entity;
using System.Linq;

namespace ConspectoPatronum.Core.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly DataContext _context;
        private IDbSet<T> _entities;
        private IDbSet<T> Entities => _entities ?? (_entities = _context.Set<T>());

        public Repository(DataContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            if (entity == null)
            {
                throw new Exception("Can't add a null entity");
            }
            Entities.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = Entities.Find(id);
            if (entity == null)
            {
                throw new Exception(String.Format("No entity with id {0}", id));
            }
            Entities.Remove(entity);
            _context.SaveChanges();
        }

        public int Count()
        {
            return Entities.Count();
        }

        public IQueryable<T> GetAll() => Entities;

        public T GetById(int id)
        {
            var entity = Entities.Find(id);
            if (entity == null)
            {
                throw new Exception(String.Format("No entity with id {0}", id));
            }
            return entity;
        }

        public void SaveOrUpdate(T entity)
        {
            if (entity == null)
            {
                throw new Exception("Entity is null");
            }
            if (Entities.Find(entity.Id) == null)
            {
                Add(entity);
            }
            else
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
            _context.SaveChanges();
        }
    }
}
