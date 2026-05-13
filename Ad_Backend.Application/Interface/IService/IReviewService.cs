using Ad_Backend.Application.DTOs;
using Ad_Backend.Domain.Domain;

namespace Ad_Backend.Application.Interface.IService;


public interface IReviewService
{
    Task<Review> CreateAsync(
        CreateReviewDto dto,
        string userId
    );

    Task<List<Review>> GetAllAsync();
}
