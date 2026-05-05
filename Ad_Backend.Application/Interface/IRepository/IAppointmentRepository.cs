using Ad_Backend.Domain.Domain;

namespace Ad_Backend.Application.Interface.IRepository;

public interface IAppointmentRepository
{
    Task<Appointment> CreateAsync(Appointment appointment);
    Task<List<Appointment>> GetAllAsync();
    Task<List<Appointment>> GetByCustomerIdAsync(long customerId);
    Task<List<Appointment>> GetNewAsync();
    Task<Appointment?> GetByIdAsync(long id);
    Task UpdateAsync(Appointment appointment);
    
    
 
    Task<List<Appointment>> GetByUserIdAsync(string userId);

    Task MarkAsNotifiedAsync(List<Appointment> appointments);
    Task<Appointment?> GetByIdWithDetailsAsync(long id);
}
