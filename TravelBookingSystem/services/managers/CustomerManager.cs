namespace TravelBookingSystem.services;

public class CustomerManager
{
    private readonly CustomerRepository customerRepository;

    public CustomerManager(CustomerRepository customerRepository)
    {
        this.customerRepository = customerRepository;
    }

    public async Task<List<Customer>> GetAllCustomersAsync()
    {
        return await customerRepository.GetAllAsync();
    }

    public async Task<Customer> GetCustomerByIdAsync(int id)
    {
        return await customerRepository.GetByIdAsync(id);
    }

    public async Task AddCustomerAsync(Customer customer)
    {
        await customerRepository.AddAsync(customer);
    }

    public async Task UpdateCustomerAsync(Customer customer)
    {
        await customerRepository.UpdateAsync(customer);
    }

    public async Task RemoveCustomerAsync(int id)
    {
        await customerRepository.RemoveAsync(id);
    }
}