using Ad_Backend.Application.DTOs.Vendors;

namespace Ad_Backend.Application.Interface.IService;

public interface IVendorService
{
    Task<VendorDto> CreateAsync(CreateVendorDto dto);
    Task<List<VendorDto>> GetAllAsync();
    Task<VendorDto?> GetByIdAsync(long id);
    Task<string> UpdateAsync(long id, CreateVendorDto dto);
    Task<string> DeleteAsync(long id);
}