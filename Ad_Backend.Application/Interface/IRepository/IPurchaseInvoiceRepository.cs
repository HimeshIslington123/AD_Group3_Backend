using Ad_Backend.Domain.Domain;

namespace Ad_Backend.Application.Interface.IRepository;

public interface IPurchaseInvoiceRepository
{
    Task<PurchaseInvoice> CreateAsync(PurchaseInvoice invoice);
    Task<List<PurchaseInvoice>> GetAllAsync();
    Task<PurchaseInvoice?> GetByIdAsync(long id);
}
