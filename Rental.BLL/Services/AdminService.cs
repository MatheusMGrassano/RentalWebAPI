using Microsoft.EntityFrameworkCore;
using Rental.BLL.Services.Interfaces;
using Rental.BLL.Validators;
using Rental.DAL;
using Rental.DAL.DataContext;
using Rental.DAL.Entities;
using Rental.DAL.DTOs.AdminDTOs;

namespace Rental.BLL.Services
{
    public class AdminService : IAdminService
    {
        private readonly AppDbContext _context;
        private readonly TokenService _token;
        private readonly PasswordService _password;

        public AdminService(AppDbContext context, TokenService token, PasswordService password)
        {
            _context = context;
            _token = token;
            _password = password;
        }

        public async Task<Response<string>> AddAdminAsync(AdminAddDTO adminAddDTO)
        {
            try
            {
                adminAddDTO.Email = adminAddDTO.Email.ToLower();

                CredentialsValidator.ValidateEmail(adminAddDTO.Email);
                CredentialsValidator.ValidatePassword(adminAddDTO.Password);

                if (await _context.Admin.FirstOrDefaultAsync(x => x.Email == adminAddDTO.Email)is not null)
                    throw new Exception("Email already registered");

                var newAdmin = new Admin
                {
                    Email = adminAddDTO.Email,
                    Password = _password.HashPassword(adminAddDTO.Password)
                };

                await _context.Admin.AddAsync(newAdmin);
                await _context.SaveChangesAsync();

                return Response<string>.Success("Admin registered successfully");
            }
            catch(Exception ex)
            {
                return Response<string>.Error(ex.Message);
            }
        }

        public async Task<Response<string>> UpdatePasswordAdminAsync(AdminUpdatePasswordDTO adminUpdatePasswordDTO)
        {
            try
            {
                var admin = AdminLogin(adminUpdatePasswordDTO.Email, adminUpdatePasswordDTO.Password);

                CredentialsValidator.ValidatePassword(adminUpdatePasswordDTO.NewPassword);

                var newAdmin = new Admin
                {
                    Email = adminUpdatePasswordDTO.Email,
                    Password = _password.HashPassword(adminUpdatePasswordDTO.NewPassword)
                };

                _context.Admin.Update(admin);
                await _context.SaveChangesAsync();

                return Response<string>.Success("Password updated successfully");
            }
            catch (Exception ex)
            {
                return Response<string>.Error(ex.Message);
            }
        }

        public async Task<Response<string>> RemoveAdminAsync(AdminLoginDTO adminLoginDTO)
        {
            try
            {
                var admin = AdminLogin(adminLoginDTO.Email, adminLoginDTO.Password);

                _context.Admin.Remove(admin);
                await _context.SaveChangesAsync();

                return Response<string>.Success("Admin deleted successfully");
            }
            catch (Exception ex)
            {
                return Response<string>.Error(ex.Message);
            }
        }

        public async Task<Response<string>> LoginAdminAsync(AdminLoginDTO adminLoginDTO)
        {
            try
            {
                var admin = AdminLogin(adminLoginDTO.Email, adminLoginDTO.Password);

                return Response<string>.Success(_token.Generate(admin.Email));
            }
            catch (Exception ex)
            {
                return Response<string>.Error(ex.Message);
            }
        }

        private Admin AdminLogin(string email, string password)
        {
            var admin = _context.Admin.FirstOrDefault(x => x.Email == email);
            if (admin is null || !_password.AuthPassword(admin.Password, password))
                throw new Exception("Incorrect email or password");
            return admin;
        }
    }
}
