namespace Ad_Backend.Domain.Domain;

public class Payment
{
    public long Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
}
