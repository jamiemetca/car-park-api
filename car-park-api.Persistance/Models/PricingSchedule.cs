namespace car_park_api.Persistance.Models
{
    public sealed class PricingSchedule
    {
        public int PricingScheduleId { get; set; }
        public int CarParkId { get; set; }
        public DateOnly AppliesFrom { get; set; }
        public DateOnly AppliesTo { get; set; }
        public decimal Price { get; set; }

        public CarPark CarPark { get; set; }
    }
}
