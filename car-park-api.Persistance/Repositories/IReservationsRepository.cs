using car_park_api.Persistance.Models;

namespace car_park_api.Persistance.Repositories
{
    public interface IReservationsRepository
    {
        Reservation? GetReservationById(int id);
        List<Reservation> GetAllReservations();
        int GetNumberOfBookingForDay(DateOnly date, int carParkId);
        Reservation CreateReservation(Reservation reservation);
        Reservation UpdateReservation(Reservation reservation);
        void DeleteReservation(Reservation reservation);
    }
}
