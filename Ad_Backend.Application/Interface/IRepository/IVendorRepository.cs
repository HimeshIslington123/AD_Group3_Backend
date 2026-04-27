using Ad_Backend.Domain.Domain;

namespace Ad_Backend.Application.Interface.IRepository;

public interface IVendorRepository
{
    Task<Vendor> CreateAsync(Vendor vendor);
    Task<List<Vendor>> GetAllAsync();
    Task<Vendor?> GetByIdAsync(long id);
    Task UpdateAsync(Vendor vendor);
    Task DeleteAsync(Vendor vendor);
}