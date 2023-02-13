namespace car_park_api.Service.DTOs
{
    public sealed class CarParkDTO
    {
        public int CarParkId { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public decimal DefaultPricing { get; set; }
    }
}
