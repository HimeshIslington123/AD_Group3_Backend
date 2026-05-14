using Ad_Backend.Application.DTOs;
using Ad_Backend.Application.Interface.IRepository;
using Ad_Backend.Application.Interface.IService;
using Ad_Backend.Domain.Domain;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repo;

    public CustomerService(ICustomerRepository repo)
    {
        _repo = repo;
    }

    public async Task<Customer?> GetByUserIdAsync(string userId)
    {
        return await _repo.GetByUserIdAsync(userId);
    }
    public async Task<List<CustomerDto>> GetAllAsync()
    {
        var customers = await _repo.GetAllAsync();

        var result = new List<CustomerDto>();

        foreach (var c in customers)
        {
            var dto = new CustomerDto
            {
                Id = c.Id,
                FullName = c.FullName,
                Email = c.Email,
                Phone = c.Phone,
                Address = c.Address,
                Vehicles = new List<VehicleDto>(),
                SalesInvoices = new List<SalesInvoiceDto>(),
                Appointments = new List<AppointmentDto>()
            };

            // Vehicles
            foreach (var v in c.Vehicles)
            {
                dto.Vehicles.Add(new VehicleDto
                {
                    Id = v.Id,
                    VehicleNumber = v.VehicleNumber,
                    Brand = v.Brand,
                    Model = v.Model,
                    Year = v.Year,
                    Type = v.Type
                });
            }

            // Sales
            foreach (var s in c.SalesInvoices)
            {
                dto.SalesInvoices.Add(new SalesInvoiceDto
                {
                    Id = s.Id,
                    Amount = s.TotalAmount,
                    Date = s.Date
                });
            }

            // Appointments
            foreach (var a in c.Appointments)
            {
                dto.Appointments.Add(new AppointmentDto
                {
                    Id = a.Id,
                    Date = a.Date,
                    Status = a.Status
                });
            }

            result.Add(dto);
        }

        return result;
    }

    // ✅ GET BY ID (same idea)
    public async Task<CustomerDto?> GetByIdAsync(long id)
    {
        var c = await _repo.GetByIdAsync(id);
        if (c == null) return null;

        var dto = new CustomerDto
        {
            Id = c.Id,
            FullName = c.FullName,
            Email = c.Email,
            Phone = c.Phone,
            Address = c.Address,
            Vehicles = new List<VehicleDto>(),
            SalesInvoices = new List<SalesInvoiceDto>(),
            Appointments = new List<AppointmentDto>()
        };

        foreach (var v in c.Vehicles)
        {
            dto.Vehicles.Add(new VehicleDto
            {
                Id = v.Id,
                VehicleNumber = v.VehicleNumber,
                Brand = v.Brand,
                Model = v.Model,
                Year = v.Year,
                Type = v.Type
            });
        }

        foreach (var s in c.SalesInvoices)
        {
            dto.SalesInvoices.Add(new SalesInvoiceDto
            {
                Id = s.Id,
                Amount = s.TotalAmount,
                Date = s.Date
            });
        }

        foreach (var a in c.Appointments)
        {
            dto.Appointments.Add(new AppointmentDto
            {
                Id = a.Id,
                Date = a.Date,
                Status = a.Status
            });
        }

        return dto;
    }

    public async Task<CustomerDto?> UpdateAsync(long id, UpdateCustomerDto dto)
    {
        var updated = await _repo.UpdateAsync(id, dto);
        if (updated == null) return null;

        // reuse GetById to avoid rewriting mapping
        return await GetByIdAsync(updated.Id);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        return await _repo.DeleteAsync(id);
    }
}
