using ConspectoPatronum.Core.Services;
using System.Collections.Generic;
using System.Linq;
using ConspectoPatronum.Domain;
using ConspectoPatronum.Core.Repositories;

namespace ConspectoPatronum.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly IRepository<Comment> _repository;

        public CommentsService(IRepository<Comment> repository)
        {
            _repository = repository;
        }

        public void Add(Comment comment)
        {
            _repository.Add(comment);
        }

        public int Count()
        {
            return _repository.Count();
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public IList<Comment> GetAll()
        {
            return _repository.GetAll()
                .OrderBy(comment => comment.PostedOn)
                .ToList();
        }

        public Comment GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Update(Comment comment)
        {
            _repository.SaveOrUpdate(comment);
        }
    }
}
