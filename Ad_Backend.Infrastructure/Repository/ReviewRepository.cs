using Ad_Backend.Application.Interface.IRepository;
using Ad_Backend.Domain.Domain;
using Ad_Backend.Infrastructure.Presistance;
using Microsoft.EntityFrameworkCore;

namespace Ad_Backend.Infrastructure.Repository;

public class ReviewRepository : IReviewRepository
{
    private readonly AppDbContext _context;

    public ReviewRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Review> CreateAsync(
        Review review
    )
    {
        _context.Reviews.Add(review);

        await _context.SaveChangesAsync();

        return review;
    }

    public async Task<List<Review>> GetAllAsync()
    {
        return await _context.Reviews
            .Include(x => x.Customer)
            .Include(x => x.Appointment)
            .ToListAsync();
    }
}
