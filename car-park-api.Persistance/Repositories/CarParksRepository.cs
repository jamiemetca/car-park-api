using car_park_api.Persistance.Models;
using Microsoft.EntityFrameworkCore;

namespace car_park_api.Persistance.Repositories
{
    public class CarParksRepository : ICarParksRepository
    {
        private readonly CarParkApiContext _context;

        public CarParksRepository(CarParkApiContext context)
        {
            _context = context;
        }

        public List<CarPark> GetAllCarParks()
        {
            return _context.CarParks
                .AsNoTracking()
                .ToList();
        }

        public CarPark? GetCarParkById(int id)
        {
            return _context.CarParks
                .Where(carPark => carPark.CarParkId == id)
                .AsNoTracking()
                .FirstOrDefault();
        }

        public int GetCapacity(int carParkId)
        {
            return _context.CarParks
                .Where(carPark => carPark.CarParkId == carParkId)
                .AsNoTracking()
                .Select(carPark => carPark.Capacity)
                .FirstOrDefault();
        }

        public CarPark UpdateCarPark(CarPark carPark)
        {
            var updatedCarPark = _context.CarParks.Update(carPark);
            _context.SaveChanges();
            DetachAllEntries();

            return updatedCarPark.Entity;
        }

        private void DetachAllEntries()
        {
            _context.ChangeTracker
                .Entries()
                .ToList()
                .ForEach(entry => entry.State = EntityState.Detached);
        }
    }
}
