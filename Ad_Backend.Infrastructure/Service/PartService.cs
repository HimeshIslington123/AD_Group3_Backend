using Ad_Backend.Application.Interface.IRepository;
using Ad_Backend.Application.Interface.IService;
using Ad_Backend.Domain.Domain;

namespace Ad_Backend.Infrastructure.Service;

public class PartService : IPartService
{
    private readonly IPartRepository _partRepository;

    public PartService(IPartRepository partRepository)
    {
        _partRepository = partRepository;
    }

    public async Task<Part> CreatePartAsync(Part part)
    {
        return await _partRepository.AddAsync(part);
    }

    public async Task<List<Part>> GetAllPartsAsync()
    {
        return await _partRepository.GetAllAsync();
    }

    public async Task<Part?> GetPartByIdAsync(long id)
    {
        return await _partRepository.GetByIdAsync(id);
    }

    public async Task<Part?> UpdatePartAsync(long id, Part part)
    {
        return await _partRepository.UpdateAsync(id, part);
    }

    public async Task<bool> DeletePartAsync(long id)
    {
        return await _partRepository.DeleteAsync(id);
    }
}