using Ad_Backend.Application.DTOs;
using Ad_Backend.Domain.Domain;

public interface ICustomerService
{
    Task<List<CustomerDto>> GetAllAsync();
    Task<CustomerDto?> GetByIdAsync(long id);
    Task<CustomerDto?> UpdateAsync(long id, UpdateCustomerDto dto);
    Task<bool> DeleteAsync(long id);
    
    Task<Customer?> GetByUserIdAsync(string userId);
}
