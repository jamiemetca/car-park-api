using car_park_api.Service.DTOs.Reservations;

namespace car_park_api.Service.Interfaces
{
    public interface IReservationService
    {
        List<ReservationDTO> GetAllRerservations();
        ReservationDTO CreateReservation(CreateReservationDTO reservationRequest);
        // List<CarParkAvailabilityInfoDTO> GetAvilability(CarParkAvailabilityRequestDTO request);
        ReservationDTO UpdateReservation(UpdateReservationDTO request);
        void DeleteReservation(int reservationId);
    }
}
