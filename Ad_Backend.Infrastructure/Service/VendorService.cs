using Ad_Backend.Application.DTOs;
using Ad_Backend.Application.Interface.IRepository;
using Ad_Backend.Application.Interface.IService;
using Ad_Backend.Domain.Domain;

namespace Ad_Backend.Infrastructure.Service;

public class VendorService : IVendorService
{
    private readonly IVendorRepository _repository;

    public VendorService(IVendorRepository repository)
    {
        _repository = repository;
    }

    public async Task<Vendor> CreateAsync(CreateVendorDto dto)
    {
        var vendor = new Vendor
        {
            Name = dto.Name,
            Phone = dto.Phone,
            Address = dto.Address,
            Email = dto.Email
        };
        return await _repository.CreateAsync(vendor);
    }

    public async Task<List<Vendor>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Vendor?> GetByIdAsync(long id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Vendor?> UpdateAsync(long id, CreateVendorDto dto)
    {
        var vendor = await _repository.GetByIdAsync(id);
        if (vendor == null) return null;

        vendor.Name = dto.Name;
        vendor.Phone = dto.Phone;
        vendor.Address = dto.Address;
        vendor.Email = dto.Email;

        return await _repository.UpdateAsync(vendor);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        return await _repository.DeleteAsync(id);
    }
}
