using Ad_Backend.Application.DTOs;
using Ad_Backend.Domain.Domain;

namespace Ad_Backend.Application.Interface.IRepository;


public interface IRequestPartRepository
{
    // CREATE
    Task<PartRequest> CreateAsync(
        PartRequest request
    );

    // GET ALL
    Task<List<PartRequest>> GetAllAsync();

    // GET BY CUSTOMER
    Task<List<PartRequest>>
        GetByCustomerIdAsync(long customerId);

    // GET BY USER
    Task<List<PartRequest>>
        GetByUserIdAsync(string userId);

    // GET BY ID
    Task<PartRequest?> GetByIdAsync(long id);

    // UPDATE
    Task UpdateAsync(
        PartRequest request
    );
}
