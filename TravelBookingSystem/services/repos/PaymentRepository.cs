namespace TravelBookingSystem.services;

public class PaymentRepository
{
    private readonly string filePath;

    public PaymentRepository(string filePath)
    {
        this.filePath = filePath;
    }

    public async Task<List<Payment>> GetAllAsync()
    {
        List<Payment> payments = new List<Payment>();

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                Payment payment = ParseLine(line);
                payments.Add(payment);
            }
        }

        return payments;
    }

    public async Task<Payment> GetByIdAsync(int id)
    {
        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                Payment payment = ParseLine(line);
                if (payment.Id == id)
                {
                    return payment;
                }
            }
        }

        return null;
    }

    public async Task AddAsync(Payment payment)
    {
        using (StreamWriter writer = new StreamWriter(filePath, append: true))
        {
            await writer.WriteLineAsync(FormatLine(payment));
        }
    }

    public async Task UpdateAsync(Payment payment)
    {
        List<string> lines = new List<string>();

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                Payment existingPayment = ParseLine(line);
                if (existingPayment.Id != payment.Id)
                {
                    lines.Add(line);
                }
                else
                {
                    lines.Add(FormatLine(payment));
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
                Payment existingPayment = ParseLine(line);
                if (existingPayment.Id != id)
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

    private Payment ParseLine(string line)
    {
        string[] parts = line.Split(';');
        return new Payment
        {
            Id = int.Parse(parts[0]),
            BookingId = int.Parse(parts[1]),
            Amount = decimal.Parse(parts[2]),
            Date = DateTime.Parse(parts[3])
        };
    }

    private string FormatLine(Payment payment)
    {
        return $"{payment.Id};{payment.BookingId};{payment.Amount};{payment.Date.ToString("yyyy-MM-dd")}";
    }
}
