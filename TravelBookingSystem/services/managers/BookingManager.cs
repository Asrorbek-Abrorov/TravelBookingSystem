namespace TravelBookingSystem.services;

public class BookingManager
{
    private readonly BookingRepository bookingRepository;

    public BookingManager(BookingRepository bookingRepository)
    {
        this.bookingRepository = bookingRepository;
    }

    public async Task<List<Booking>> GetAllBookingsAsync()
    {
        return await bookingRepository.GetAllAsync();
    }

    public async Task<Booking> GetBookingByIdAsync(int id)
    {
        return await bookingRepository.GetByIdAsync(id);
    }

    public async Task AddBookingAsync(Booking booking)
    {
        await bookingRepository.AddAsync(booking);
    }

    public async Task UpdateBookingAsync(Booking booking)
    {
        await bookingRepository.UpdateAsync(booking);
    }

    public async Task RemoveBookingAsync(int id)
    {
        await bookingRepository.RemoveAsync(id);
    }
}