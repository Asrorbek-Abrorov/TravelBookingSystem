namespace TravelBookingSystem;

public class Customer
{
    private static int id = 0;
    public int Id = ++id;
    public string Name { get; set; }
    public string ContactDetails { get; set; }
    public string PaymentInformation { get; set; }
}