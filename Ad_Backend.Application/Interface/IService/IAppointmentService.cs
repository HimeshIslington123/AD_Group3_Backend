using Ad_Backend.Application.DTOs;
using Ad_Backend.Domain.Domain;

namespace Ad_Backend.Application.Interface.IService;

public interface IAppointmentService
{
    Task<Appointment> CreateAppointmentAsync(CreateAppointmentDto dto, string userId);
    Task<List<Appointment>> GetAllAsync();
    Task<List<Appointment>> GetByCustomerIdAsync(long customerId);
    
    Task<List<Appointment>> GetNewAppointmentsAsync();
    Task SendInvoiceAsync(long appointmentId);
    Task<bool> UpdateStatusAsync(long id, string status);
    Task<List<Appointment>> GetByUserIdAsync(string userId);
    Task MarkAppointmentsAsNotifiedAsync(List<Appointment> appointments);
}
