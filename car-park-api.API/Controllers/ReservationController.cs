using car_park_api.Service.DTOs.Reservations;
using car_park_api.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace car_park_api.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ReservationController: ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public ActionResult<List<ReservationDTO>> GetAllReservations()
        {
            return Ok(_reservationService.GetAllRerservations());
        }

        [HttpPost(Name = "CreateReservation")]
        public ActionResult<ReservationDTO> CreateReservation(CreateReservationDTO reservationRequest)
        {
            ReservationDTO reservation = new ReservationDTO();
            try
            {
                reservation = _reservationService.CreateReservation(reservationRequest);
            }
            catch(ArgumentException exp)
            {
                Response.StatusCode = 400;
                return Content(exp.Message);
            }

            return Ok(reservation);
        }

        [HttpPost(Name = "UpdateReservation")]
        public ActionResult<ReservationDTO> UpdateReservation(UpdateReservationDTO request)
        {
            ReservationDTO result = new ReservationDTO();

            try
            {
                result = _reservationService.UpdateReservation(request);
            }
            catch (ArgumentNullException exp)
            {
                Response.StatusCode = 404;
                return Content(exp.Message);
            }
            catch (ArgumentException exp)
            {
                Response.StatusCode = 400;
                return Content(exp.Message);
            }

            return Ok(result);
        }

        [HttpDelete(Name = "DeleteReservation")]
        public ActionResult<ReservationDTO> DeleteReservation(int reservationId)
        {
            try
            {
                _reservationService.DeleteReservation(reservationId);
            }
            catch (ArgumentNullException exp)
            {
                Response.StatusCode = 404;
                return Content(exp.Message);
            }

            return NoContent();
        }
    }
}
