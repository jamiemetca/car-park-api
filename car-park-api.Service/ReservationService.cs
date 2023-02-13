using AutoMapper;
using car_park_api.Persistance.Models;
using car_park_api.Persistance.Repositories;
using car_park_api.Service.DTOs.CarParks;
using car_park_api.Service.DTOs.Reservations;
using car_park_api.Service.Interfaces;

namespace car_park_api.Service
{
    public sealed class ReservationService : IReservationService
    {
        private readonly IReservationsRepository _reservationsRepository;
        private readonly IMapper _mapper;
        private readonly ICarParksRepository _carParksRepository;
        private readonly ICarParkService _carParkService;
        private readonly IPricingSchedulesRepository _pricingSchedulesRepository;

        public ReservationService(
            IReservationsRepository reservationsRepository,
            IMapper mapper,
            ICarParksRepository carParksRepository,
            ICarParkService carParkService,
            IPricingSchedulesRepository pricingSchedulesRepository)
        {
            _reservationsRepository = reservationsRepository;
            _mapper = mapper;
            _carParksRepository = carParksRepository;
            _carParkService = carParkService;
            _pricingSchedulesRepository = pricingSchedulesRepository;
        }
        public List<ReservationDTO> GetAllRerservations()
        {
            var reservations = _reservationsRepository.GetAllReservations();

            return _mapper.Map<List<ReservationDTO>>(reservations);
        }

        public ReservationDTO CreateReservation(CreateReservationDTO request)
        {
            var availabilityRequest = _mapper.Map<CarParkAvailabilityRequestDTO>(request);
            var carParkAvailability = _carParkService.GetAvilability(availabilityRequest);
            var carParkIsAvailable = carParkAvailability.All(info => info.SpacesAvailable > 0);

            if(!carParkIsAvailable)
            {
                throw new ArgumentException("Car park is not available for requested dates");
            }

            // check can parse
            DateTime fromDate;
            var canParseFromDate = DateTime.TryParse(request.DateFrom, out fromDate);
            DateTime toDate;
            var canParseToDate = DateTime.TryParse(request.DateTo, out toDate);

            if (!canParseFromDate || !canParseToDate)
            {
                throw new ArgumentException("Date format must be yyyy/mm/dd");
            }

            var totalPrice = carParkAvailability.Select(info => info.Price).Sum();

            var reservation = new Reservation
            {
                DateFrom = DateOnly.FromDateTime(fromDate),
                DateTo = DateOnly.FromDateTime(toDate),
                CarParkId = request.CarParkId,
                TotalPrice = totalPrice
            };

            var newReservation = _reservationsRepository.CreateReservation(reservation);

            return _mapper.Map<ReservationDTO>(newReservation);
        }
    
        public ReservationDTO UpdateReservation(UpdateReservationDTO request)
        {
            var availabilityRequest = _mapper.Map<CarParkAvailabilityRequestDTO>(request);
            var carParkAvailability = _carParkService.GetAvilability(availabilityRequest);
            var carParkIsAvailable = carParkAvailability.All(info => info.SpacesAvailable > 0);

            if (!carParkIsAvailable)
            {
                throw new ArgumentException("Car park is not available for requested dates");
            }

            // check can parse
            DateTime fromDate;
            var canParseFromDate = DateTime.TryParse(request.DateFrom, out fromDate);
            DateTime toDate;
            var canParseToDate = DateTime.TryParse(request.DateTo, out toDate);

            if (!canParseFromDate || !canParseToDate)
            {
                throw new ArgumentException("Date format must be yyyy/mm/dd");
            }

            var updatedReservation = _mapper.Map<Reservation>(request);
            updatedReservation.TotalPrice = carParkAvailability.Select(info => info.Price).Sum();

            var newReservation = _reservationsRepository.UpdateReservation(updatedReservation);

            return _mapper.Map<ReservationDTO>(newReservation);
        }

        public void DeleteReservation(int reservationId)
        {
            var reservation = _reservationsRepository.GetReservationById(reservationId);
            if(reservation == null)
            {
                throw new ArgumentNullException("Reservation not found");
            }

            _reservationsRepository.DeleteReservation(reservation);
        }
    }
}
