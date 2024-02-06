using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rental.BLL.Services.Interfaces;
using Rental.DAL;
using Rental.DAL.DTOs.MotorcycleDTOs;
using System.Net;

namespace Rental_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotorcycleController : ControllerBase
    {
        private readonly IMotorcycleService _motorcycleService;

        public MotorcycleController(IMotorcycleService motorcycleService)
        {
            _motorcycleService = motorcycleService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<Response<MotorcycleDTO>>> GetMotorcycleList([FromQuery] int offset, int limit = 10)
        {
            return Ok(await _motorcycleService.GetMotorcycleListAsync(offset, limit));
        }

        [Authorize]
        [HttpGet("id/{id}")]
        public async Task<ActionResult<Response<MotorcycleDTO>>> GetById(int id)
        {
            try
            {
                var response = await _motorcycleService.GetMotorcycleByIdAsync(id);
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return Ok(response);

                    case HttpStatusCode.NotFound:
                        return NotFound(response);

                    default:
                        return BadRequest(response);
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpGet("plate/{plate}")]
        public async Task<ActionResult<Response<MotorcycleDTO>>> GetByPlate(string plate)
        {
            try
            {
                var response = await _motorcycleService.GetMotorcycleByPlateAsync(plate);
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return Ok(response);

                    case HttpStatusCode.NotFound:
                        return NotFound(response);

                    default:
                        return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(Response<string>.Error(ex.Message));
            }
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<ActionResult<Response<MotorcycleDTO>>> CreateMotorcycle(MotorcycleAddDTO motorcycleAddDTO)
        {
            try
            {
                return Ok(await _motorcycleService.AddMotorcycleAsync(motorcycleAddDTO));
            }
            catch(Exception ex)
            {
                return BadRequest(Response<string>.Error(ex.Message));
            }
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<ActionResult<Response<MotorcycleDTO>>> UpdateMotorcycle(int id, string newPlate)
        {
            try
            {
                var response = await _motorcycleService.UpdateMotorcyclePlateAsync(id, newPlate);
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return Ok(response);

                    case HttpStatusCode.NotFound:
                        return NotFound(response);

                    default:
                        return BadRequest(response);
                }
            }
            catch(Exception ex)
            {
                return BadRequest(Response<string>.Error(ex.Message));
            }
        }

        [Authorize]
        [HttpDelete("delete")]
        public async Task<ActionResult<Response<MotorcycleDTO>>> DeleteMotorcycle(int id)
        {
            try
            {
                var response = await _motorcycleService.RemoveMotorcycleAsync(id);
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return Ok(response);

                    case HttpStatusCode.NotFound:
                        return NotFound(response);

                    default:
                        return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(Response<string>.Error(ex.Message));
            }
        }
    }
}
