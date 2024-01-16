namespace TravelBookingSystem;

public class Payment
{
    private static int id = 0;
    public int Id = ++id;
    public int BookingId { get; set; }
    public decimal AmountPaid { get; set; }
    public PaymentStatus Status { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}