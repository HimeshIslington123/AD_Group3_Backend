using Ad_Backend.Application.DTOs;
using Ad_Backend.Application.DTOs.Vendors;
using Ad_Backend.Application.Interface.IRepository;
using Ad_Backend.Application.Interface.IService;
using Ad_Backend.Domain.Domain;

public class VendorService : IVendorService
{
    private readonly IVendorRepository _repository;

    public VendorService(IVendorRepository repository)
    {
        _repository = repository;
    }

    public async Task<VendorDto> CreateAsync(CreateVendorDto dto)
    {
        var vendor = new Vendor
        {
            Name = dto.Name,
            Phone = dto.Phone,
            Address = dto.Address,
            Email = dto.Email
        };

        var result = await _repository.CreateAsync(vendor);

        return new VendorDto
        {
            Id = result.Id,
            Name = result.Name,
            Phone = result.Phone,
            Address = result.Address,
            Email = result.Email
        };
    }

    public async Task<List<VendorDto>> GetAllAsync()
    {
        var vendors = await _repository.GetAllAsync();

        return vendors.Select(v => new VendorDto
        {
            Id = v.Id,
            Name = v.Name,
            Phone = v.Phone,
            Address = v.Address,
            Email = v.Email
        }).ToList();
    }

    public async Task<VendorDto?> GetByIdAsync(long id)
    {
        var vendor = await _repository.GetByIdAsync(id);

        if (vendor == null) return null;

        return new VendorDto
        {
            Id = vendor.Id,
            Name = vendor.Name,
            Phone = vendor.Phone,
            Address = vendor.Address,
            Email = vendor.Email
        };
    }

    public async Task<string> UpdateAsync(long id, CreateVendorDto dto)
    {
        var vendor = await _repository.GetByIdAsync(id);

        if (vendor == null)
            return "Vendor not found";

        vendor.Name = dto.Name;
        vendor.Phone = dto.Phone;
        vendor.Address = dto.Address;
        vendor.Email = dto.Email;

        await _repository.UpdateAsync(vendor);

        return "Vendor updated successfully";
    }

    public async Task<string> DeleteAsync(long id)
    {
        var vendor = await _repository.GetByIdAsync(id);

        if (vendor == null)
            return "Vendor not found";

        await _repository.DeleteAsync(vendor);

        return "Vendor deleted successfully";
    }
}