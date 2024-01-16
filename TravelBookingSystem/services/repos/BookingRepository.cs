namespace TravelBookingSystem.services;

public class BookingRepository
{
    private readonly string filePath;

    public BookingRepository(string filePath)
    {
        this.filePath = filePath;
    }

    public async Task<List<Booking>> GetAllAsync()
    {
        List<Booking> bookings = new List<Booking>();

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                Booking booking = ParseLine(line);
                bookings.Add(booking);
            }
        }

        return bookings;
    }

    public async Task<Booking> GetByIdAsync(int id)
    {
        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                Booking booking = ParseLine(line);
                if (booking.Id == id)
                {
                    return booking;
                }
            }
        }

        return null;
    }

    public async Task AddAsync(Booking booking)
    {
        using (StreamWriter writer = new StreamWriter(filePath, append: true))
        {
            await writer.WriteLineAsync(FormatLine(booking));
        }
    }

    public async Task UpdateAsync(Booking booking)
    {
        List<string> lines = new List<string>();

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                Booking existingBooking = ParseLine(line);
                if (existingBooking.Id != booking.Id)
                {
                    lines.Add(line);
                }
                else
                {
                    lines.Add(FormatLine(booking));
                }
            }
        }

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (string line in lines)
            {
                await writer.WriteLineAsync(line);
            }
        }
    }

    public async Task RemoveAsync(int id)
    {
        List<string> lines = new List<string>();

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                Booking existingBooking = ParseLine(line);
                if (existingBooking.Id != id)
                {
                    lines.Add(line);
                }
            }
        }

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (string line in lines)
            {
                await writer.WriteLineAsync(line);
            }
        }
    }

    private Booking ParseLine(string line)
    {
        string[] parts = line.Split(';');
        return new Booking
        {
            Id = int.Parse(parts[0]),
            CustomerId = int.Parse(parts[1]),
            Date = DateTime.Parse(parts[2]),
            Amount = decimal.Parse(parts[3])
        };
    }

    private string FormatLine(Booking booking)
    {
        return $"{booking.Id};{booking.CustomerId};{booking.Date.ToString("yyyy-MM-dd")};{booking.Amount}";
    }
}
