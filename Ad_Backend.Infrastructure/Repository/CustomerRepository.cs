using Ad_Backend.Application.DTOs;
using Ad_Backend.Application.Interface.IRepository;
using Ad_Backend.Domain.Domain;
using Ad_Backend.Infrastructure.Presistance;
using Microsoft.EntityFrameworkCore;

public class CustomerRepository : ICustomerRepository
{
    private readonly AppDbContext _context;

    public CustomerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Customer>> GetAllAsync()
    {
        return await _context.Customers
            .Include(c => c.Vehicles)
            .Include(c => c.SalesInvoices)
            .Include(c => c.Appointments)
            .ToListAsync();
    }

    public async Task<Customer?> GetByIdAsync(long id)
    {
        return await _context.Customers
            .Include(c => c.Vehicles)
            .Include(c => c.SalesInvoices)
            .Include(c => c.Appointments)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
    public async Task<Customer?> GetByUserIdAsync(string userId)
    {
        return await _context.Customers
            .Include(c => c.Vehicles)
            .Include(c => c.Appointments)
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }
    public async Task<Customer?> UpdateAsync(long id, UpdateCustomerDto dto)
    {
        var customer = await _context.Customers
            .Include(c => c.Vehicles)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (customer == null) return null;

        customer.FullName = dto.FullName;
        customer.Phone = dto.Phone;
        customer.Address = dto.Address;

        foreach (var v in dto.Vehicles)
        {
            if (v.Id != 0)
            {
                var existing = customer.Vehicles.FirstOrDefault(x => x.Id == v.Id);
                if (existing != null)
                {
                    existing.VehicleNumber = v.VehicleNumber;
                    existing.Brand = v.Brand;
                    existing.Model = v.Model;
                    existing.Year = v.Year;
                    existing.Type = v.Type;
                }
            }
            else
            {
                customer.Vehicles.Add(new Vehicle
                {
                    VehicleNumber = v.VehicleNumber,
                    Brand = v.Brand,
                    Model = v.Model,
                    Year = v.Year,
                    Type = v.Type
                });
            }
        }

        var dtoIds = dto.Vehicles.Select(v => v.Id).ToList();
        var toRemove = customer.Vehicles.Where(v => !dtoIds.Contains(v.Id)).ToList();
        _context.Vehicles.RemoveRange(toRemove);

        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null) return false;

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
        return true;
    }
}
