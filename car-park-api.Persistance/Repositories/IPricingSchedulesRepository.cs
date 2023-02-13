using car_park_api.Persistance.Models;

namespace car_park_api.Persistance.Repositories
{
    public interface IPricingSchedulesRepository
    {
        PricingSchedule GetPricingScheduleByCarParkIdAndDate(int carParkId, DateOnly date);
    }
}
