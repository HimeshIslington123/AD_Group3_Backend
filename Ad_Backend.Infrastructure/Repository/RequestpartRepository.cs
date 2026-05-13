using Ad_Backend.Application.Interface.IRepository;
using Ad_Backend.Domain.Domain;
using Ad_Backend.Infrastructure.Presistance;
using Microsoft.EntityFrameworkCore;

namespace Ad_Backend.Infrastructure.Repository;





public class RequestpartRepository : IRequestPartRepository
{
    private readonly AppDbContext _context;

    public RequestpartRepository(
        AppDbContext context
    )
    {
        _context = context;
    }

    // CREATE
    public async Task<PartRequest> CreateAsync(
        PartRequest request
    )
    {
        _context.PartRequests.Add(request);

        await _context.SaveChangesAsync();

        return request;
    }

    // GET ALL
    public async Task<List<PartRequest>>
        GetAllAsync()
    {
        return await _context.PartRequests
            .Include(x => x.Customer)
            .Include(x => x.Vehicle)
            .OrderByDescending(x => x.Date)
            .ToListAsync();
    }

    // GET BY CUSTOMER
    public async Task<List<PartRequest>>
        GetByCustomerIdAsync(long customerId)
    {
        return await _context.PartRequests
            .Include(x => x.Vehicle)
            .Where(x => x.CustomerId == customerId)
            .OrderByDescending(x => x.Date)
            .ToListAsync();
    }

    // GET BY USER
    public async Task<List<PartRequest>>
        GetByUserIdAsync(string userId)
    {
        return await _context.PartRequests
            .Include(x => x.Customer)
            .Include(x => x.Vehicle)
            .Where(x => x.Customer!.UserId == userId)
            .OrderByDescending(x => x.Date)
            .ToListAsync();
    }

    // GET BY ID
    public async Task<PartRequest?>
        GetByIdAsync(long id)
    {
        return await _context.PartRequests
            .Include(x => x.Vehicle)
            .Include(x => x.Customer)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    // UPDATE
    public async Task UpdateAsync(
        PartRequest request
    )
    {
        _context.PartRequests.Update(request);

        await _context.SaveChangesAsync();
    }
}
