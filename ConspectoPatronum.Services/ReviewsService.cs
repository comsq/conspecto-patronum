using ConspectoPatronum.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using ConspectoPatronum.Domain;
using ConspectoPatronum.Core.Repositories;

namespace ConspectoPatronum.Services
{
    public class ReviewsService : IReviewsService
    {
        private readonly IRepository<Review> _repository;

        public ReviewsService(IRepository<Review> repository)
        {
            _repository = repository;
        }

        public void Add(Review item)
        {
            _repository.Add(item);
        }

        public int Count()
        {
            return _repository.Count();
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public IList<Review> GetAll()
        {
            return _repository.GetAll()
                .OrderByDescending(review => review.DateTime)
                .ToList();
        }

        public Review GetById(int id)
        {
            return _repository.GetById(id);
        }

        public Review GetLastVisit()
        {
            return _repository.GetAll()
                .Where(review => review.IsVisit)
                .OrderByDescending(review => review.DateTime)
                .FirstOrDefault();
        }

        public int GetTodayReviews()
        {
            throw new NotImplementedException("Problem with difference between datetime values.");
        }

        public int GetTodayVisits()
        {
            throw new NotImplementedException("Problem with difference between datetime values.");
        }

        public int GetVisits()
        {
            return _repository.GetAll()
                .Where(review => review.IsVisit)
                .OrderByDescending(review => review.DateTime)
                .Count();
        }

        public void Update(Review item)
        {
            _repository.SaveOrUpdate(item);
        }
    }
}
