using Microsoft.AspNetCore.Mvc;
using Rental.BLL.Services.Interfaces;
using Rental.DAL;
using Rental.DAL.DTOs.DriverDTOs;
using Rental.DAL.DTOs.RentDTOs;

namespace Rental_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _driverService;
        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<Response<string>>> LoginDriverAsync(DriverLoginDTO driverLoginDTO)
        {
            try
            {
                return Ok(await _driverService.LoginDriverAsync(driverLoginDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(Response<string>.Error(ex.Message));
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<Response<string>>> CreateDriver(DriverAddDTO driverAddDTO)
        {
            try
            {
                return Ok(await _driverService.AddDriverAsync(driverAddDTO));
            }
            catch(Exception ex) 
            {
                return BadRequest(Response<string>.Error(ex.Message));
            }
        }

        [HttpPost("rent")]
        public async Task<ActionResult<Response<string>>> CreateRent(RentAddDTO rentAddDTO)
        {
            try
            {
                return Ok(await _driverService.RentMotorcycleAsync(rentAddDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(Response<string>.Error(ex.Message));
            }
        }

        [HttpPost("return")]
        public async Task<ActionResult<Response<string>>> ReturnRent(RentEndDateDTO rentEndDateDTO)
        {
            try
            {
                return Ok(await _driverService.InformEndDate(rentEndDateDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(Response<string>.Error(ex.Message));
            }
        }
    }
}
