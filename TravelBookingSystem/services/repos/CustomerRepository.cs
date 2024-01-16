namespace TravelBookingSystem.services;

public class CustomerRepository
{
    private readonly string filePath;

    public CustomerRepository(string filePath)
    {
        this.filePath = filePath;
    }

    public async Task<List<Customer>> GetAllAsync()
    {
        List<Customer> customers = new List<Customer>();

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                Customer customer = ParseLine(line);
                customers.Add(customer);
            }
        }

        return customers;
    }

    public async Task<Customer> GetByIdAsync(int id)
    {
        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                Customer customer = ParseLine(line);
                if (customer.Id == id)
                {
                    return customer;
                }
            }
        }

        return null;
    }

    public async Task AddAsync(Customer customer)
    {
        using (StreamWriter writer = new StreamWriter(filePath, append: true))
        {
            await writer.WriteLineAsync(FormatLine(customer));
        }
    }

    public async Task UpdateAsync(Customer customer)
    {
        List<string> lines = new List<string>();

        using (StreamReader reader = new StreamReader(filePath))
        {
            string line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                Customer existingCustomer = ParseLine(line);
                if (existingCustomer.Id != customer.Id)
                {
                    lines.Add(line);
                }
                else
                {
                    lines.Add(FormatLine(customer));
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
                Customer existingCustomer = ParseLine(line);
                if (existingCustomer.Id != id)
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

    private Customer ParseLine(string line)
    {
        string[] parts = line.Split(';');
        return new Customer
        {
            Id = int.Parse(parts[0]),
            Name = parts[1],
            ContactDetails = parts[2],
            PaymentInformation = parts[3]
        };
    }

    private string FormatLine(Customer customer)
    {
        return $"{customer.Id};{customer.Name};{customer.ContactDetails};{customer.PaymentInformation}";
    }
}
