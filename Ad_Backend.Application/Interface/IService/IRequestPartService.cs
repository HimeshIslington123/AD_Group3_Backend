using Ad_Backend.Application.DTOs;
using Ad_Backend.Domain.Domain;

namespace Ad_Backend.Application.Interface.IService;


public interface IRequestPartService
{
    // CREATE
    Task<PartRequest> CreateAsync(
        CreatePartRequestDto dto,
        string userId
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

    // UPDATE STATUS
    Task<bool> UpdateStatusAsync(
        long id,
        string status
    );
}
