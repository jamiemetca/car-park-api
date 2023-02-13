namespace car_park_api.Service.DTOs.Reservations
{
    public sealed class UpdateReservationDTO
    {
        public int ReservationId { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int CarParkId { get; set; }
    }
}
