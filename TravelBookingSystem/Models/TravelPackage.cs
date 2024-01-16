namespace TravelBookingSystem;

public class TravelPackage
{
    private static int id = 0;
    public int Id = ++id;
    public string Name { get; set; }
    public string Destination { get; set; }
    public int Duration { get; set; }
    public decimal Price { get; set; }
    public int AvailableSpots { get; set; }
    public string Itinerary { get; set; }
}