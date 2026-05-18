using Ad_Backend.Application.Interface.IRepository;
using Ad_Backend.Domain.Domain;
using Ad_Backend.Infrastructure.Presistance;
using Microsoft.EntityFrameworkCore;

namespace Ad_Backend.Infrastructure.Repository;

public class VendorRepository : IVendorRepository
{
    private readonly AppDbContext _context;

    public VendorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Vendor> CreateAsync(Vendor vendor)
    {
        _context.Vendors.Add(vendor);
        await _context.SaveChangesAsync();
        return vendor;
    }

    public async Task<List<Vendor>> GetAllAsync()
    {
        return await _context.Vendors.ToListAsync();
    }

    public async Task<Vendor?> GetByIdAsync(long id)
    {
        return await _context.Vendors.FindAsync(id);
    }

    public async Task<Vendor?> UpdateAsync(Vendor vendor)
    {
        _context.Vendors.Update(vendor);
        await _context.SaveChangesAsync();
        return vendor;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var vendor = await _context.Vendors.FindAsync(id);
        if (vendor == null) return false;

        _context.Vendors.Remove(vendor);
        await _context.SaveChangesAsync();
        return true;
    }
}
