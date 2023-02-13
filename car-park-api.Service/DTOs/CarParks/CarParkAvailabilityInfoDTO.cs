namespace car_park_api.Service.DTOs.CarParks
{
    public sealed class CarParkAvailabilityInfoDTO
    {
        public decimal Price { get; set; }
        public DateOnly Date { get; set; }
        public int SpacesAvailable { get; set; }
    }
}
