using Ad_Backend.Application.DTOs;
using Ad_Backend.Application.Interface.IRepository;
using Ad_Backend.Application.Interface.IService;
using Ad_Backend.Domain.Domain;

namespace Ad_Backend.Infrastructure.Service;

public class RequestPartsService : IRequestPartService
{
    private readonly IRequestPartRepository _repo;

    private readonly ICustomerRepository _customerRepo;

    public RequestPartsService(
        IRequestPartRepository repo,
        ICustomerRepository customerRepo
    )
    {
        _repo = repo;
        _customerRepo = customerRepo;
    }

    // CREATE
    public async Task<PartRequest> CreateAsync(
        CreatePartRequestDto dto,
        string userId
    )
    {
        var customer = await _customerRepo
            .GetByUserIdAsync(userId);

        if (customer == null)
            throw new Exception("Customer not found");

        var request = new PartRequest
        {
            CustomerId = customer.Id,
            VehicleId = dto.VehicleId,
            PartName = dto.PartName,
            Quantity = dto.Quantity,
            Urgency = dto.Urgency,
            Notes = dto.Notes,
            Status = "Pending",
            Date = DateTime.UtcNow
        };

        return await _repo.CreateAsync(request);
    }

    // GET ALL
    public async Task<List<PartRequest>> GetAllAsync()
    {
        return await _repo.GetAllAsync();
    }

    // GET BY ID
    public async Task<PartRequest?> GetByIdAsync(long id)
    {
        return await _repo.GetByIdAsync(id);
    }

    // GET BY CUSTOMER
    public async Task<List<PartRequest>> GetByCustomerIdAsync(long customerId)
    {
        return await _repo.GetByCustomerIdAsync(customerId);
    }

    // GET BY USER
    public async Task<List<PartRequest>> GetByUserIdAsync(string userId)
    {
        return await _repo.GetByUserIdAsync(userId);
    }

    // UPDATE STATUS
    public async Task<bool> UpdateStatusAsync(
        long id,
        string status
    )
    {
        var request = await _repo.GetByIdAsync(id);

        if (request == null)
            return false;

        request.Status = status;

        await _repo.UpdateAsync(request);

        return true;
    }
}
