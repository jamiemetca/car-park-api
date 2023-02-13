using car_park_api.Persistance.Models;

namespace car_park_api.Persistance.Repositories
{
    public class PricingSchedulesRepository : IPricingSchedulesRepository
    {
        private readonly CarParkApiContext _context;

        public PricingSchedulesRepository(CarParkApiContext context)
        {
            _context = context;
        }

        public PricingSchedule GetPricingScheduleByCarParkIdAndDate(int carParkId, DateOnly date)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return _context.PricingSchedules
                .Where(pricingSchedule =>
                    pricingSchedule.CarParkId == carParkId &&
                    pricingSchedule.AppliesFrom >= date &&
                    pricingSchedule.AppliesTo <= date)
                .FirstOrDefault();
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
