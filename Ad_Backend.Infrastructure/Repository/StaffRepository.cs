using Ad_Backend.Application.Interface.IRepository;
using Ad_Backend.Domain.Domain;
using Ad_Backend.Infrastructure.Presistance;
using Microsoft.EntityFrameworkCore;

namespace Ad_Backend.Infrastructure.Repository;

public class StaffRepository : IStaffRepository
{
    private readonly AppDbContext _context;

    public StaffRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Staff>> GetAllAsync()
    {
        return await _context.Staffs
            .Include(s => s.User)   
            .ToListAsync();
    }

    public async Task<Staff?> GetByIdAsync(int id)
    {
        return await _context.Staffs.FindAsync(id);
    }

    public async Task<Staff> AddAsync(Staff staff)
    {
        _context.Staffs.Add(staff);
        await _context.SaveChangesAsync();
        return staff;
    }

    public async Task<Staff?> UpdateAsync(int id, Staff staff)
    {
        var existing = await _context.Staffs.FindAsync(id);
        if (existing == null) return null;

        existing.FullName = staff.FullName;
        existing.Email = staff.Email;
        existing.Position = staff.Position;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var staff = await _context.Staffs.FindAsync(id);
        if (staff == null) return false;

        _context.Staffs.Remove(staff);
        await _context.SaveChangesAsync();
        return true;
    }
}