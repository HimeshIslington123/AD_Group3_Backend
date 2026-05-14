using Ad_Backend.Domain.Domain;
using Ad_Backend.Application.DTOs;

public interface ICustomerRepository
{
    Task<List<Customer>> GetAllAsync();
    Task<Customer?> GetByIdAsync(long id);
    Task<Customer?> UpdateAsync(long id, UpdateCustomerDto dto);
    Task<bool> DeleteAsync(long id);
    Task<Customer?> GetByUserIdAsync(string userId);
}
