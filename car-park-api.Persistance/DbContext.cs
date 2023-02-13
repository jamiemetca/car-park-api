using car_park_api.Persistance.Models;
using Microsoft.EntityFrameworkCore;

namespace car_park_api.Persistance;
public class CarParkApiContext: DbContext
{
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseInMemoryDatabase(databaseName: "CarParkDb");
	}

	public DbSet<PricingSchedule> PricingSchedules { get; set;}
	public DbSet<Reservation> Reservations { get; set; }
	public DbSet<CarPark> CarParks { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<CarPark>().HasData(
			new CarPark { 
				CarParkId = 1, 
				Name = "Airport Carpark", 
				Capacity = 10, 
				DefaultPricing = 10 });

        modelBuilder.Entity<PricingSchedule>().HasData(
            new PricingSchedule
            {
                PricingScheduleId = 1,
				CarParkId = 1,
				AppliesFrom = new DateOnly(2023, 02, 13),
                AppliesTo = new DateOnly(2023, 02, 17),
				Price = 5
            });

		modelBuilder.Entity<Reservation>().HasData(
			new Reservation
			{
				ReservationId = 1,
				DateFrom = new DateOnly(2023, 02, 13),
				DateTo = new DateOnly(2023, 02, 14),
				CarParkId = 1,
				TotalPrice = 20
			},
            new Reservation
            {
                ReservationId = 2,
                DateFrom = new DateOnly(2023, 02, 13),
                DateTo = new DateOnly(2023, 02, 14),
                CarParkId = 1,
                TotalPrice = 20
            },
            new Reservation
            {
                ReservationId = 3,
                DateFrom = new DateOnly(2023, 02, 13),
                DateTo = new DateOnly(2023, 02, 14),
                CarParkId = 1,
                TotalPrice = 20
            },
            new Reservation
            {
                ReservationId = 4,
                DateFrom = new DateOnly(2023, 02, 13),
                DateTo = new DateOnly(2023, 02, 14),
                CarParkId = 1,
                TotalPrice = 20
            },
            new Reservation
            {
                ReservationId = 5,
                DateFrom = new DateOnly(2023, 02, 13),
                DateTo = new DateOnly(2023, 02, 14),
                CarParkId = 1,
                TotalPrice = 20
            },
            new Reservation
            {
                ReservationId = 6,
                DateFrom = new DateOnly(2023, 02, 13),
                DateTo = new DateOnly(2023, 02, 14),
                CarParkId = 1,
                TotalPrice = 20
            },
            new Reservation
            {
                ReservationId = 7,
                DateFrom = new DateOnly(2023, 02, 13),
                DateTo = new DateOnly(2023, 02, 14),
                CarParkId = 1,
                TotalPrice = 20
            },
            new Reservation
            {
                ReservationId = 8,
                DateFrom = new DateOnly(2023, 02, 13),
                DateTo = new DateOnly(2023, 02, 14),
                CarParkId = 1,
                TotalPrice = 20
            },
            new Reservation
            {
                ReservationId = 9,
                DateFrom = new DateOnly(2023, 02, 13),
                DateTo = new DateOnly(2023, 02, 14),
                CarParkId = 1,
                TotalPrice = 20
            },
            new Reservation
            {
                ReservationId = 10,
                DateFrom = new DateOnly(2023, 02, 13),
                DateTo = new DateOnly(2023, 02, 14),
                CarParkId = 1,
                TotalPrice = 20
            });
    }
}
