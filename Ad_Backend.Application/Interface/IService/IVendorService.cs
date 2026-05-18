using Ad_Backend.Application.DTOs;
using Ad_Backend.Domain.Domain;

namespace Ad_Backend.Application.Interface.IService;

public interface IVendorService
{
    Task<Vendor> CreateAsync(CreateVendorDto dto);
    Task<List<Vendor>> GetAllAsync();
    Task<Vendor?> GetByIdAsync(long id);
    Task<Vendor?> UpdateAsync(long id, CreateVendorDto dto);
    Task<bool> DeleteAsync(long id);
}
