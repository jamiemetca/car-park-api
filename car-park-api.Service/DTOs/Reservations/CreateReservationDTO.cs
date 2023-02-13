namespace car_park_api.Service.DTOs.Reservations
{
    public sealed class CreateReservationDTO
    {
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int CarParkId { get; set; }
    }
}
