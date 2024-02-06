using Rental.DAL;
using Rental.DAL.DTOs.AdminDTOs;

namespace Rental.BLL.Services.Interfaces
{
    public interface IAdminService
    {
        public Task<Response<string>> AddAdminAsync(AdminAddDTO adminAddDTO);
       // public Task<Response<string>> UpdateEmailAdminAsync(AdminDTO adminDTO);
        public Task<Response<string>> UpdatePasswordAdminAsync(AdminUpdatePasswordDTO adminUpdatePasswordDTO);
        public Task<Response<string>> RemoveAdminAsync(AdminLoginDTO adminLoginDTO);
        public Task<Response<string>> LoginAdminAsync(AdminLoginDTO adminLoginDTO);
    }
}
