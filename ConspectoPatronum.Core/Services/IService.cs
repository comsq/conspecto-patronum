using ConspectoPatronum.Domain;
using System.Collections.Generic;

namespace ConspectoPatronum.Core.Services
{
    public interface IService<T> where T : Entity
    {
        void Add(T item);

        void Delete(int id);

        void Update(T item);

        IList<T> GetAll();

        T GetById(int id);

        int Count();
    }
}
