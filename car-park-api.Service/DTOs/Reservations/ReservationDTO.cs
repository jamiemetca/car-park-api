namespace car_park_api.Service.DTOs.Reservations
{
    public sealed class ReservationDTO
    {
        public int ReservationId { get; set; }
        public DateOnly DateFrom { get; set; }
        public DateOnly DateTo { get; set; }
        public int CarParkId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
