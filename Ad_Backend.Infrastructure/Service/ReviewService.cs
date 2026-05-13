using Ad_Backend.Application.DTOs;
using Ad_Backend.Application.Interface.IRepository;
using Ad_Backend.Application.Interface.IService;
using Ad_Backend.Domain.Domain;

namespace Ad_Backend.Infrastructure.Service;


public class ReviewService : IReviewService
{
    private readonly IReviewRepository _repo;

    private readonly ICustomerRepository _customerRepo;

    public ReviewService(
        IReviewRepository repo,
        ICustomerRepository customerRepo
    )
    {
        _repo = repo;
        _customerRepo = customerRepo;
    }

    public async Task<Review> CreateAsync(
        CreateReviewDto dto,
        string userId
    )
    {
        var customer = await _customerRepo
            .GetByUserIdAsync(userId);

        if (customer == null)
            throw new Exception("Customer not found");

        var review = new Review
        {
            AppointmentId = dto.AppointmentId,
            CustomerId = customer.Id,
            Rating = dto.Rating,
            Comment = dto.Comment,
            Date = DateTime.UtcNow
        };

        return await _repo.CreateAsync(review);
    }

    public async Task<List<Review>> GetAllAsync()
    {
        return await _repo.GetAllAsync();
    }
}
