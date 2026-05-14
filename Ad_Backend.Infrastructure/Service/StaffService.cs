using Ad_Backend.Application.Interface.IRepository;
using Ad_Backend.Application.Interface.IService;
using Ad_Backend.Domain.Domain;

namespace Ad_Backend.Infrastructure.Service;

public class StaffService : IStaffService
{
    private readonly IStaffRepository _staffRepository;

    public StaffService(IStaffRepository staffRepository)
    {
        _staffRepository = staffRepository;
    }

    public async Task<List<Staff>> GetAllStaffAsync()
    {
        return await _staffRepository.GetAllAsync();
    }

    public async Task<Staff?> GetStaffByIdAsync(int id)
    {
        return await _staffRepository.GetByIdAsync(id);
    }

    public async Task<Staff> CreateStaffAsync(Staff staff)
    {
        return await _staffRepository.AddAsync(staff);
    }

    public async Task<Staff?> UpdateStaffAsync(int id, Staff staff)
    {
        return await _staffRepository.UpdateAsync(id, staff);
    }

    public async Task<bool> DeleteStaffAsync(long id)
    {
        return await _staffRepository.DeleteAsync(id);
    }
}