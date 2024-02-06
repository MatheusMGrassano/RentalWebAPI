using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rental.BLL.Services.Interfaces;
using Rental.DAL;
using Rental.DAL.DTOs.AdminDTOs;

namespace Rental_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<Response<string>>> LoginAdmin(AdminLoginDTO adminLoginDTO)
        {
            try
            {
                return Ok(await _adminService.LoginAdminAsync(adminLoginDTO));
            }
            catch(Exception ex) 
            {
                return BadRequest(Response<string>.Error(ex.Message));
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<Response<string>>> CreateAdmin(AdminAddDTO adminAddDTO)
        {
            try
            {
                return Ok(await _adminService.AddAdminAsync(adminAddDTO));
            }
            catch(Exception ex) 
            {
                return BadRequest(Response<string>.Error(ex.Message));
            }
        }
        [Authorize]
        [HttpPut("passwordupdate")]
        public async Task<ActionResult<Response<string>>> UpdatePasswordAdmin(AdminUpdatePasswordDTO adminUpdatePasswordDTO)
        {
            try
            {
                return Ok(await _adminService.UpdatePasswordAdminAsync(adminUpdatePasswordDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(Response<string>.Error(ex.Message));
            }
        }

        [Authorize]
        [HttpDelete("delete")]
        public async Task<ActionResult<Response<string>>> RemoveAdmin(AdminLoginDTO adminLoginDTO)
        {
            try
            {
                return Ok(await _adminService.RemoveAdminAsync(adminLoginDTO));
            }
            catch (Exception ex)
            {
                return BadRequest(Response<string>.Error(ex.Message));
            }
        }
    }
}
