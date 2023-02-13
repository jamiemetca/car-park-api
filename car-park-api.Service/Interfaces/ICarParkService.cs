using car_park_api.Service.DTOs;
using car_park_api.Service.DTOs.CarParks;

namespace car_park_api.Service.Interfaces
{
    public interface ICarParkService
    {
        List<CarParkDTO> GetAllCarParks();
        List<CarParkAvailabilityInfoDTO> GetAvilability(CarParkAvailabilityRequestDTO request);
        CarParkDTO UpdateCapacity(CarParkCapacityDTO request);
    }
}
