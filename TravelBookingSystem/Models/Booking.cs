namespace TravelBookingSystem;

public class Booking
{
    private static int id = 0;
    public int Id = ++id;
    public int TravelPackageId { get; set; }
    public int CustomerId { get; set; }
    public int NumberOfTravelers { get; set; }
    public DateTime TravelDate { get; set; }
    public BookingStatus Status { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
}