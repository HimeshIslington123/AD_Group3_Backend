using Ad_Backend.Domain.Domain;

namespace Ad_Backend.Application.Interface.IRepository;

public interface IReviewRepository
{
    Task<Review> CreateAsync(Review review);

    Task<List<Review>> GetAllAsync();
}
