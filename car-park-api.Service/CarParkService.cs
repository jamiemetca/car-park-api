using AutoMapper;
using car_park_api.Persistance.Repositories;
using car_park_api.Service.DTOs;
using car_park_api.Service.DTOs.CarParks;
using car_park_api.Service.Interfaces;

namespace car_park_api.Service
{
    public sealed class CarParkService : ICarParkService
    {
        private readonly ICarParksRepository _carParksRepository;
        private readonly IReservationsRepository _reservationsRepository;
        private readonly IPricingSchedulesRepository _pricingSchedulesRepository;
        private readonly IMapper _mapper;

        public CarParkService(
            ICarParksRepository carParksRepository,
            IReservationsRepository reservationRepository,
            IPricingSchedulesRepository pricingSchedulesRepository,
            IMapper mapper)
        {
            _carParksRepository = carParksRepository;
            _reservationsRepository = reservationRepository;
            _pricingSchedulesRepository = pricingSchedulesRepository;
            _mapper = mapper;
        }

        public List<CarParkDTO> GetAllCarParks()
        {
            var results = _carParksRepository.GetAllCarParks();
            
            return _mapper.Map<List<CarParkDTO>>(results);
        }

        public List<CarParkAvailabilityInfoDTO> GetAvilability(CarParkAvailabilityRequestDTO request)
        {
            var isCarParkProvided = request.CarParkId != default(int);
            var isDateFromProvided = !string.IsNullOrWhiteSpace(request.DateFrom);
            var isDateToProvided = !string.IsNullOrWhiteSpace(request.DateTo);

            if (!isCarParkProvided || !isDateFromProvided || !isDateToProvided)
            {
                throw new ArgumentNullException("Required fields: carParkId, dateFrom and dateTo");
            }

            DateTime fromDate;
            var canParseFromDate = DateTime.TryParse(request.DateFrom, out fromDate);
            DateTime toDate;
            var canParseToDate = DateTime.TryParse(request.DateTo, out toDate);

            if (!canParseFromDate || !canParseToDate)
            {
                throw new ArgumentException("Date format must be yyyy/mm/dd");
            }

            if (fromDate > toDate)
            {
                throw new ArgumentException("fromDate must be before toDate");
            }

            var availability = new List<CarParkAvailabilityInfoDTO>();

            var carParkInfo = _carParksRepository.GetCarParkById(request.CarParkId);

            if (carParkInfo == null)
            {
                throw new ArgumentNullException("Car park not found");
            }

            DateTime day;
            for (day = fromDate.Date; day.Date <= toDate.Date; day = day.AddDays(1))
            {
                var dateOnly = DateOnly.FromDateTime(day);
                var spacesTaken = _reservationsRepository.GetNumberOfBookingForDay(dateOnly, request.CarParkId);
                var spacesRemaining = Math.Max(carParkInfo.Capacity - spacesTaken, 0);

                var availabilityInfo = new CarParkAvailabilityInfoDTO
                {
                    SpacesAvailable = spacesRemaining,
                    Date = dateOnly,
                    Price = carParkInfo?.DefaultPricing ?? 0
                };

                var pricingInfoForDay = _pricingSchedulesRepository.GetPricingScheduleByCarParkIdAndDate(request.CarParkId, dateOnly);
                if (pricingInfoForDay != null)
                {
                    availabilityInfo.Price = pricingInfoForDay.Price;
                }

                availability.Add(availabilityInfo);
            }

            return availability;
        }

        public CarParkDTO UpdateCapacity(CarParkCapacityDTO request)
        {
            var isCarParkProvided = request.CarParkId != default(int);
            var isCapacityProvided = request.Capacity != default(int) && request.Capacity > -1;

            if (!isCarParkProvided || !isCapacityProvided)
            {
                throw new ArgumentException("Required fields: carParkId and Capacity");
            }

            var carPark = _carParksRepository.GetCarParkById(request.CarParkId);

            if (carPark == null)
            {
                throw new ArgumentNullException("Car park not found");
            }

            carPark.Capacity= request.Capacity;

            var updatedCarPark = _carParksRepository.UpdateCarPark(carPark);

            return _mapper.Map<CarParkDTO>(updatedCarPark);
        }

    }
}
