namespace TravelBookingSystem.services;

// TravelPackageRepository.cs in TravelBookingSystem.DataAccess
public class TravelPackageRepository
{
    private readonly string filePath;

    public TravelPackageRepository(string filePath)
    {
        this.filePath = filePath;
    }

    public async Task<List<TravelPackage>> GetAllAsync()
    {
        List<TravelPackage> travelPackages = new List<TravelPackage>();

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                TravelPackage travelPackage = ParseLine(line);
                travelPackages.Add(travelPackage);
            }
        }

        return travelPackages;
    }

    public async Task<TravelPackage> GetByIdAsync(int id)
    {
        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                TravelPackage travelPackage = ParseLine(line);
                if (travelPackage.Id == id)
                {
                    return travelPackage;
                }
            }
        }

        return null;
    }

    public async Task AddAsync(TravelPackage travelPackage)
    {
        using (StreamWriter writer = new StreamWriter(filePath, append: true))
        {
            await writer.WriteLineAsync(FormatLine(travelPackage));
        }
    }

    public async Task UpdateAsync(TravelPackage travelPackage)
    {
        List<string> lines = new List<string>();

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                TravelPackage existingTravelPackage = ParseLine(line);
                if (existingTravelPackage.Id != travelPackage.Id)
                {
                    lines.Add(line);
                }
                else
                {
                    lines.Add(FormatLine(travelPackage));
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
                TravelPackage existingTravelPackage = ParseLine(line);
                if (existingTravelPackage.Id != id)
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

    private TravelPackage ParseLine(string line)
    {
        string[] parts = line.Split(';');
        return new TravelPackage
        {
            Id = int.Parse(parts[0]),
            Name = parts[1],
            Destination = parts[2],
            Duration = int.Parse(parts[3]),
            Price = decimal.Parse(parts[4]),
            AvailableSpots = int.Parse(parts[5]),
            Itinerary = parts[6]
        };
    }

    private string FormatLine(TravelPackage travelPackage)
    {
        return $"{travelPackage.Id};{travelPackage.Name};{travelPackage.Destination};{travelPackage.Duration};{travelPackage.Price};{travelPackage.AvailableSpots};{travelPackage.Itinerary}";
    }
}