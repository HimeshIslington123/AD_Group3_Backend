using Ad_Backend.Application.DTOs.PurchaseInvoices;

namespace Ad_Backend.Application.Interface.IService;

public interface IPurchaseInvoiceService
{
    Task<PurchaseInvoiceDto> CreateInvoiceAsync(CreatePurchaseInvoiceDto dto);
    Task<List<PurchaseInvoiceDto>> GetAllInvoicesAsync();
    Task<PurchaseInvoiceDto?> GetInvoiceByIdAsync(long id);
}
