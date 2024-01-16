namespace TravelBookingSystem.services;

public class PaymentManager
{
    private readonly PaymentRepository paymentRepository;

    public PaymentManager(PaymentRepository paymentRepository)
    {
        this.paymentRepository = paymentRepository;
    }

    public async Task<List<Payment>> GetAllPaymentsAsync()
    {
        return await paymentRepository.GetAllAsync();
    }

    public async Task<Payment> GetPaymentByIdAsync(int id)
    {
        return await paymentRepository.GetByIdAsync(id);
    }

    public async Task AddPaymentAsync(Payment payment)
    {
        await paymentRepository.AddAsync(payment);
    }

    public async Task UpdatePaymentAsync(Payment payment)
    {
        await paymentRepository.UpdateAsync(payment);
    }

    public async Task RemovePaymentAsync(int id)
    {
        await paymentRepository.RemoveAsync(id);
    }
}