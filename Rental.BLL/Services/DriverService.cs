using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Rental.BLL.Services.Interfaces;
using Rental.BLL.Validators;
using Rental.DAL;
using Rental.DAL.DataContext;
using Rental.DAL.DTOs.DriverDTOs;
using Rental.DAL.DTOs.RentDTOs;
using Rental.DAL.Entities;
using Rental.DAL.Entities.Records;

namespace Rental.BLL.Services
{
    public class DriverService : IDriverService
    {
        private readonly AppDbContext _context;
        private readonly PasswordService _password;
        private readonly TokenService _token;

        public DriverService(AppDbContext context, PasswordService password, TokenService token)
        {
            _context = context;
            _password = password;
            _token = token;
        }

        public async Task<Response<string>> AddDriverAsync(DriverAddDTO driverAddDTO)
        {
            try
            {
                driverAddDTO.Email = driverAddDTO.Email.ToLower();
                CredentialsValidator.ValidateEmail(driverAddDTO.Email);
                CredentialsValidator.ValidatePassword(driverAddDTO.Password);

                if (await _context.Driver.FirstOrDefaultAsync(x => x.Email == driverAddDTO.Email) is not null)
                    throw new Exception("Email already registered");
                if (await _context.Driver.FirstOrDefaultAsync(x => x.Cnpj == driverAddDTO.Cnpj || x.Licence == driverAddDTO.Licence) is not null)
                    throw new Exception("Driver already registered");
                if (driverAddDTO.Name.IsNullOrEmpty() || driverAddDTO.Cnpj.IsNullOrEmpty() || driverAddDTO.BirthDate.Equals(DateTime.MinValue))
                    throw new Exception("Please, enter driver's data");

                var newDriver = new Driver
                {
                    Email = driverAddDTO.Email,
                    Password = _password.HashPassword(driverAddDTO.Password),
                    Name = driverAddDTO.Name,
                    Cnpj = driverAddDTO.Cnpj,
                    BirthDate = driverAddDTO.BirthDate,
                    Licence = driverAddDTO.Licence,
                    LicenceType = driverAddDTO.LicenceType
                };

                await _context.Driver.AddAsync(newDriver);
                await _context.SaveChangesAsync();

                return Response<string>.Success("Driver registered successfully");
            }
            catch (Exception ex)
            {
                return Response<string>.Error(ex.Message);
            }
        }

        public async Task<Response<string>> LoginDriverAsync(DriverLoginDTO driverLoginDTO)
        {
            try
            {
                var driver = DriverLogin(driverLoginDTO.Email, driverLoginDTO.Password);

                return Response<string>.Success(_token.Generate(driver.Email));
            }
            catch (Exception ex)
            {
                return Response<string>.Error(ex.Message);
            }
        }

        public async Task<Response<RentDTO>> RentMotorcycleAsync(RentAddDTO rentAddDTO)
        {
            try
            {
                var driver = DriverLogin(rentAddDTO.DriverEmail, rentAddDTO.DriverPassword);

                var motorcycle = await _context.Motorcycle.FirstOrDefaultAsync(x => x.Available == true);
                if (motorcycle is null)
                    throw new Exception("Sorry, no motorcycles available at the moment. Please, try again later.");
                motorcycle.Available = false;

                var plan = Plan.GetPlan(rentAddDTO.RentDays);

                var newRent = CreateRent(driver, motorcycle, plan);

                await _context.Rent.AddAsync(newRent);
                _context.Motorcycle.Update(motorcycle);
                await _context.SaveChangesAsync();

                return Response<RentDTO>.Success(CreateRentDTO(newRent));
            }
            catch (Exception ex)
            {
                return Response<RentDTO>.Error(ex.Message);
            }
        }

        public async Task<Response<RentDTO>> InformEndDate(RentEndDateDTO rentEndDateDTO)
        {
            try
            {
                var driver = DriverLogin(rentEndDateDTO.DriverEmail, rentEndDateDTO.DriverPassword);

                var rent = await _context.Rent.FirstOrDefaultAsync(x => x.RentDriver.Email == rentEndDateDTO.DriverEmail && x.RentMotorcycle.Plate == rentEndDateDTO.Plate);
                if (rent is null)
                    return Response<RentDTO>.NotFound();

                rent.EndDate = rentEndDateDTO.EndDate;
                rent.TotalPrice = CalculateTotalPrice(rent.RentPlan, rent.ExpectedEndDate, rentEndDateDTO.EndDate);

                _context.Rent.Update(rent);
                await _context.SaveChangesAsync();

                var rentDTO = CreateRentDTO(rent);

                return Response<RentDTO>.Success(rentDTO);
            }
            catch (Exception ex)
            {
                return Response<RentDTO>.Error(ex.Message);
            }
        }

        public Rent CreateRent(Driver driver, Motorcycle motorcycle, Plan plan)
        {
            return new Rent
            {
                RentDriver = driver,
                RentMotorcycle = motorcycle,
                RentPlan = plan,
                StartDate = DateTime.Now.AddDays(1),
                ExpectedEndDate = DateTime.Now.AddDays(plan.Days + 1)
            };
        }

        public RentDTO CreateRentDTO(Rent rent)
        {
            return new RentDTO
            {
                DriverName = rent.RentDriver.Name,
                MotorcyclePlate = rent.RentMotorcycle.Plate,
                StartDate = rent.StartDate,
                EndDate = rent.EndDate,
                TotalPrice = rent.TotalPrice
            };
        }

        public decimal CalculateTotalPrice(Plan plan, DateTime expectedEndDate, DateTime endDate)
        {
            const decimal extraDaysFee = 50;

            decimal totalPrice = plan.Price * plan.Days;

            if (endDate < expectedEndDate)
                totalPrice += totalPrice * (decimal)plan.Fee;

            if (endDate > expectedEndDate)
            {
                int extraDays = (endDate - expectedEndDate).Days;
                totalPrice += extraDays * extraDaysFee;
            }
            return totalPrice;
        }

        public Driver DriverLogin(string email, string password)
        {
            var driver = _context.Driver.FirstOrDefault(x => x.Email == email);
            if (driver is null || !_password.AuthPassword(driver.Password, password))
                throw new Exception("Incorrect email or password");
            return driver;
        }
    }
}
