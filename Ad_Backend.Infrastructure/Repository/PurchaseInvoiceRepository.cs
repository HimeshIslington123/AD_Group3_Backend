using Ad_Backend.Application.Interface.IRepository;
using Ad_Backend.Domain.Domain;
using Ad_Backend.Infrastructure.Presistance;
using Microsoft.EntityFrameworkCore;

namespace Ad_Backend.Infrastructure.Repository;

public class PurchaseInvoiceRepository : IPurchaseInvoiceRepository
{
    private readonly AppDbContext _context;

    public PurchaseInvoiceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PurchaseInvoice> CreateAsync(PurchaseInvoice invoice)
    {
        _context.PurchaseInvoices.Add(invoice);
        await _context.SaveChangesAsync();
        return invoice;
    }

    public async Task<List<PurchaseInvoice>> GetAllAsync()
    {
        return await _context.PurchaseInvoices
            .Include(i => i.Vendor)
            .Include(i => i.Items!)
            .ThenInclude(it => it.Part)
            .ToListAsync();
    }

    public async Task<PurchaseInvoice?> GetByIdAsync(long id)
    {
        return await _context.PurchaseInvoices
            .Include(i => i.Vendor)
            .Include(i => i.Items!)
            .ThenInclude(it => it.Part)
            .FirstOrDefaultAsync(i => i.Id == id);
    }
}
