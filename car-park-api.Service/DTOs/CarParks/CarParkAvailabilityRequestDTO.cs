namespace car_park_api.Service.DTOs.CarParks
{
    public sealed class CarParkAvailabilityRequestDTO
    {
        public int CarParkId { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
    }
}
