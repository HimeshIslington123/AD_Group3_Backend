using Ad_Backend.Domain.Domain;

namespace Ad_Backend.Application.Interface.IRepository;

public interface IStaffRepository
{
    Task<List<Staff>> GetAllAsync();
    Task<Staff?> GetByIdAsync(int id);
    Task<Staff> AddAsync(Staff staff);
    Task<Staff?> UpdateAsync(int id, Staff staff);
    Task<bool> DeleteAsync(long id);
}