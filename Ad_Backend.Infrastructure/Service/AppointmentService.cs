using Ad_Backend.Application.DTOs;
using Ad_Backend.Application.Interface.IRepository;
using Ad_Backend.Application.Interface.IService;
using Ad_Backend.Domain.Domain;

namespace Ad_Backend.Infrastructure.Service;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _repo;
    private readonly ICustomerRepository _customerRepo;
    private readonly EmailService _email;

    public AppointmentService(IAppointmentRepository repo, ICustomerRepository customerRepo,EmailService email)
    {
        _repo = repo;
        _customerRepo = customerRepo;
        _email=email;
    }
    public async Task<Appointment> CreateAppointmentAsync(CreateAppointmentDto dto, string userId)
    {

        var customer = await _customerRepo.GetByUserIdAsync(userId);

        if (customer == null)
            throw new Exception("Customer not found");

        var appointment = new Appointment
        {
            CustomerId = customer.Id, 
            VehicleId = dto.VehicleId,
            Date = dto.Date,
            ServiceType = dto.ServiceType,
            Description = dto.Description,
            SearchParts = dto.SearchParts,
            Status = "Pending"
        };

        return await _repo.CreateAsync(appointment);
    }

    public async Task<List<Appointment>> GetAllAsync()
    {
        return await _repo.GetAllAsync();
    }

    public async Task<List<Appointment>> GetByCustomerIdAsync(long customerId)
    {
        return await _repo.GetByCustomerIdAsync(customerId);
    }
    
    // 🔔 GET NEW
    public async Task<List<Appointment>> GetNewAppointmentsAsync()
    {
        return await _repo.GetNewAsync();
    }
    public async Task<List<Appointment>> GetByUserIdAsync(string userId)
    {
        return await _repo.GetByUserIdAsync(userId);
    }
    public async Task<bool> UpdateStatusAsync(long id, string status)
    {
        var appointment = await _repo.GetByIdAsync(id);

        if (appointment == null)
            return false;

        appointment.Status = status;

        await _repo.UpdateAsync(appointment);

        return true;
    }
    
    public async Task SendInvoiceAsync(long appointmentId)
{
    var appointment = await _repo.GetByIdWithDetailsAsync(appointmentId);

    if (appointment == null)
        throw new Exception("Appointment not found");

    var customer = appointment.Customer;
    var vehicle = appointment.Vehicle;

    var invoiceNo = $"INV-{appointment.Id}";
    var total = 2000; 

    var body = $@"
<div style='font-family: Arial, sans-serif; background:#f4f6f8; padding:5px;'>

    <div style='max-width:650px; margin:auto; background:white; border-radius:12px; overflow:hidden; box-shadow:0 4px 15px rgba(0,0,0,0.1);'>

        <!-- Header -->
        <div style='background:#1f2937; padding:20px; text-align:center; color:white;'>
            <img src='https://cdn-icons-png.flaticon.com/512/1995/1995470.png' width='50' />
            <h2 style='margin:10px 0 0;'>Auto Service Invoice</h2>
            <p style='margin:5px 0; font-size:13px;'>Thank you for choosing us</p>
        </div>

        <!-- Body -->
        <div style='padding:25px;'>

            <h3 style='color:#2c3e50;'>Hello {customer.FullName},</h3>
            <p style='color:#555;'>Your service invoice has been generated successfully.</p>

            <!-- Invoice Info -->
            <div style='background:#f9fafb; padding:15px; border-radius:8px; margin:15px 0;'>
                <p><strong>Invoice No:</strong> {invoiceNo}</p>
                <p><strong>Date:</strong> {appointment.Date}</p>
                <p><strong>Email:</strong> {customer.Email}</p>
            </div>

            <!-- Service Details -->
            <table style='width:100%; border-collapse:collapse; margin-top:10px;'>
                <thead>
                    <tr style='background:#e5e7eb;'>
                        <th style='padding:10px; text-align:left;'>Service</th>
                        <th style='padding:10px;'>Vehicle</th>
                        <th style='padding:10px;'>Status</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td style='padding:10px;'>{appointment.ServiceType}</td>
                        <td style='text-align:center;'>{vehicle.Brand} {vehicle.Model}</td>
                        <td style='text-align:center;'>{appointment.Status}</td>
                    </tr>
                </tbody>
            </table>

            <!-- Total -->
            <div style='text-align:right; margin-top:20px; font-size:16px;'>
                <p><strong>Total Amount:</strong> NPR {total}</p>
            </div>

            <!-- Payment Info -->
            <div style='background:#eef2ff; padding:15px; border-radius:8px; margin-top:20px;'>
                <p><strong>Payment Method:</strong> eSewa</p>
                <p><strong>eSewa ID:</strong> 98XXXXXXXX</p>
                <p style='color:#555;'>Please complete payment within 7 days.</p>
            </div>

            <!-- Button -->
            <div style='text-align:center; margin-top:25px;'>
                <a href='http://localhost:5173/invoice/{invoiceNo}'
                   style='background:#2563eb; color:white; padding:12px 25px; text-decoration:none; border-radius:6px; display:inline-block;'>
                   View Invoice
                </a>
            </div>

        </div>

        <!-- Footer -->
        <div style='background:#f3f4f6; text-align:center; padding:15px; font-size:12px; color:#666;'>
            © 2026 Auto Service System | Kathmandu, Nepal
        </div>

    </div>

</div>
";

    _email.SendEmail(customer.Email, $"Invoice #{invoiceNo}", body);
}
    public async Task MarkAppointmentsAsNotifiedAsync(List<Appointment> appointments)
    {
        await _repo.MarkAsNotifiedAsync(appointments);
    }
}
