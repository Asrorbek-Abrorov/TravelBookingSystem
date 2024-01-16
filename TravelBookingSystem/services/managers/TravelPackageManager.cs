namespace TravelBookingSystem.services;

public class TravelPackageManager
{
    private readonly TravelPackageRepository travelPackageRepository;

    public TravelPackageManager(TravelPackageRepository travelPackageRepository)
    {
        this.travelPackageRepository = travelPackageRepository;
    }

    public async Task<List<TravelPackage>> GetAllTravelPackagesAsync()
    {
        return await travelPackageRepository.GetAllAsync();
    }

    public async Task<TravelPackage> GetTravelPackageByIdAsync(int id)
    {
        return await travelPackageRepository.GetByIdAsync(id);
    }

    public async Task AddTravelPackageAsync(TravelPackage travelPackage)
    {
        await travelPackageRepository.AddAsync(travelPackage);
    }

    public async Task UpdateTravelPackageAsync(TravelPackage travelPackage)
    {
        await travelPackageRepository.UpdateAsync(travelPackage);
    }

    public async Task RemoveTravelPackageAsync(int id)
    {
        await travelPackageRepository.RemoveAsync(id);
    }
}


