using Ad_Backend.Domain.Domain;

namespace Ad_Backend.Application.Interface.IService;

public interface IStaffService
{
    Task<List<Staff>> GetAllStaffAsync();
    Task<Staff?> GetStaffByIdAsync(int id);
    Task<Staff> CreateStaffAsync(Staff staff);
    Task<Staff?> UpdateStaffAsync(int id, Staff staff);
    Task<bool> DeleteStaffAsync(long id);
}