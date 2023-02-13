using car_park_api.Persistance.Models;
using Microsoft.EntityFrameworkCore;

namespace car_park_api.Persistance.Repositories
{
    public class ReservationsRepository : IReservationsRepository
    {
        private readonly CarParkApiContext _context;

        public ReservationsRepository(CarParkApiContext context)
        {
            _context = context;
        }

        public List<Reservation> GetAllReservations()
        {
            return _context.Reservations
                .AsNoTracking()
                .ToList();
        }

        public Reservation? GetReservationById(int id)
        {
            return _context.Reservations
                .Where(reservation => reservation.ReservationId == id)
                .AsNoTracking()
                .FirstOrDefault();
        }

        public int GetNumberOfBookingForDay(DateOnly date, int carParkId)
        {
            return _context.Reservations
                .Where(reservation =>
                    reservation.CarParkId == carParkId &&
                    reservation.DateFrom <= date &&
                    reservation.DateTo >= date)
                .AsNoTracking()
                .Count();
        }

        public Reservation CreateReservation(Reservation reservation)
        {
            var newReservation = _context.Reservations
                .Add(reservation);

            _context.SaveChanges();

            DetachAllEntries();

            return newReservation.Entity;
        }

        public Reservation UpdateReservation(Reservation reservation)
        {
            var newReservation = _context.Reservations.Update(reservation);

            _context.SaveChanges();

            DetachAllEntries();

            return newReservation.Entity;
        }

        public void DeleteReservation(Reservation reservation)
        {
            _context.Remove(reservation);
            _context.SaveChanges();
            DetachAllEntries();
        }

        private void DetachAllEntries()
        {
            _context.ChangeTracker
                .Entries()
                .ToList()
                .ForEach(entry => entry.State = EntityState.Detached);
        }
    }
}
