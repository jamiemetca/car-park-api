using car_park_api.Persistance.Models;

namespace car_park_api.Persistance.Repositories
{
    public interface ICarParksRepository
    {
        List<CarPark> GetAllCarParks();
        int GetCapacity(int carParkId);
        CarPark? GetCarParkById(int id);
        CarPark UpdateCarPark(CarPark carPark);
    }
}
