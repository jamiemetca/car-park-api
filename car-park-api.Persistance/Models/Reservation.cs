namespace car_park_api.Persistance.Models
{
    public sealed class Reservation
    {
        public int ReservationId { get; set; }
        public DateOnly DateFrom { get; set; }
        public DateOnly DateTo { get; set; }
        public int CarParkId { get; set; }
        public decimal TotalPrice { get; set; }

        public CarPark CarPark { get; set; }
    }
}
