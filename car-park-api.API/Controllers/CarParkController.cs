using car_park_api.Service.DTOs;
using car_park_api.Service.DTOs.CarParks;
using car_park_api.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace car_park_api.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CarParkController : ControllerBase
{
    public readonly ICarParkService _carParkService;

    public CarParkController(
        ICarParkService carParkService)
    {
        _carParkService = carParkService;
    }

    [HttpGet(Name="GetCarParks")]
    public ActionResult<CarParkDTO> GetCarParks()
    {
        var carParks = _carParkService.GetAllCarParks();

        return Ok(carParks);
    }

    [HttpPost(Name = "CheckAvailability")]
    public ActionResult<List<CarParkAvailabilityInfoDTO>> GetAvailability(CarParkAvailabilityRequestDTO request)
    {
        List<CarParkAvailabilityInfoDTO> result = new List<CarParkAvailabilityInfoDTO>();

        try
        {
            result = _carParkService.GetAvilability(request);
        }
        catch (ArgumentNullException exp)
        {
            Response.StatusCode = 404;
            return Content(exp.Message);
        }
        catch(ArgumentException exp)
        {
            Response.StatusCode = 400;
            return Content(exp.Message);
        }

        return Ok(result);
    }

    [HttpPost(Name = "UpdateCapacity")]
    public ActionResult<CarParkDTO> UpdateCapacity(CarParkCapacityDTO request)
    {
        CarParkDTO result;
        try
        {
            result = _carParkService.UpdateCapacity(request);
        }
        catch(ArgumentNullException exp)
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
}
