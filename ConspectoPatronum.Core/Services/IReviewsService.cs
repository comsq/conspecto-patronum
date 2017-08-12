using ConspectoPatronum.Domain;

namespace ConspectoPatronum.Core.Services
{
    public interface IReviewsService : IService<Review>
    {
        int GetTodayReviews();

        int GetVisits();

        int GetTodayVisits();

        Review GetLastVisit();
    }
}
