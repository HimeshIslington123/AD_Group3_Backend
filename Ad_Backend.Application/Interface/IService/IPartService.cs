using Ad_Backend.Domain.Domain;

namespace Ad_Backend.Application.Interface.IService;

public interface IPartService
{
    Task<Part> CreatePartAsync(Part part);
    Task<List<Part>> GetAllPartsAsync();
    Task<Part?> GetPartByIdAsync(long id);
    Task<Part?> UpdatePartAsync(long id, Part part);
    Task<bool> DeletePartAsync(long id);
}