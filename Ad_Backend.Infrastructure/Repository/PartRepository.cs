using Ad_Backend.Application.Interface.IRepository;
using Ad_Backend.Domain.Domain;
using Ad_Backend.Infrastructure.Presistance;
using Microsoft.EntityFrameworkCore;

namespace Ad_Backend.Infrastructure.Repository;

public class PartRepository : IPartRepository
{
    private readonly AppDbContext _context;

    public PartRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Part> AddAsync(Part part)
    {
        _context.Parts.Add(part);
        await _context.SaveChangesAsync();
        return part;
    }

    public async Task<List<Part>> GetAllAsync()
    {
        return await _context.Parts.ToListAsync();
    }

    public async Task<Part?> GetByIdAsync(long id)
    {
        return await _context.Parts.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Part?> UpdateAsync(long id, Part part)
    {
        var existingPart = await _context.Parts.FindAsync(id);
        if (existingPart == null) return null;

        existingPart.Name = part.Name;
        existingPart.Description = part.Description;
        existingPart.Price = part.Price;
        existingPart.StockQuantity = part.StockQuantity;

        await _context.SaveChangesAsync();
        return existingPart;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var part = await _context.Parts.FindAsync(id);
        if (part == null) return false;

        _context.Parts.Remove(part);
        await _context.SaveChangesAsync();
        return true;
    }
}