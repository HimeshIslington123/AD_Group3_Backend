using Ad_Backend.Domain.Domain;

namespace Ad_Backend.Application.Interface.IRepository;

public interface IPartRepository 
{
    Task<Part> AddAsync(Part part);
    Task<List<Part>> GetAllAsync();
    Task<Part?> GetByIdAsync(long id);
    Task<Part?> UpdateAsync(long id, Part part);
    Task<bool> DeleteAsync(long id);
}
