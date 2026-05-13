using Ad_Backend.Application.Interface.IRepository;
using Ad_Backend.Domain.Domain;
using Ad_Backend.Infrastructure.Presistance;
using Microsoft.EntityFrameworkCore;

namespace Ad_Backend.Infrastructure.Repository;


public class AppointmentRepository : IAppointmentRepository
{
    private readonly AppDbContext _context;
    public AppointmentRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<List<Appointment>> GetNewAsync()
    {
        return await _context.Appointments
            .Where(a => !a.IsNotified)
            .Include(a => a.Vehicle)
            .ToListAsync();
    }

    // 🔔 MARK AS NOTIFIED
    public async Task MarkAsNotifiedAsync(List<Appointment> appointments)
    {
        foreach (var a in appointments)
        {
            a.IsNotified = true;
        }

        await _context.SaveChangesAsync();
    }
    public async Task<Appointment> CreateAsync(Appointment appointment)
    {
        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();
        return appointment;
    }

    public async Task<List<Appointment>> GetAllAsync()
    {
        return await _context.Appointments
            .Include(a => a.Customer)
            .Include(a => a.Vehicle)
            .Include(a => a.Review)   
            .ToListAsync();
    }
    public async Task<List<Appointment>> GetByUserIdAsync(string userId)
    {
        return await _context.Appointments
            .Include(a => a.Vehicle)
            .Include(a => a.Customer)
            .Include(a => a.Review)  
            .Where(a => a.Customer.UserId == userId)
            .OrderByDescending(a => a.Date)
            .ToListAsync();
    }
    public async Task<List<Appointment>> GetByCustomerIdAsync(long customerId)
    {
        return await _context.Appointments
            .Where(a => a.CustomerId == customerId)
            .Include(a => a.Vehicle)
            .Include(a => a.Review)   
            .ToListAsync();
    }
    
    public async Task<Appointment?> GetByIdAsync(long id)
    {
        return await _context.Appointments.FindAsync(id);
    }
    public async Task<Appointment?> GetByIdWithDetailsAsync(long id)
    {
        return await _context.Appointments
            .Include(a => a.Customer)
            .Include(a => a.Vehicle)
            .Include(a => a.Review) 
            .FirstOrDefaultAsync(a => a.Id == id);
    }
    public async Task UpdateAsync(Appointment appointment)
    {
        _context.Appointments.Update(appointment);
        await _context.SaveChangesAsync();
    }
}
