using Rental.DAL;
using Rental.DAL.DTOs.DriverDTOs;
using Rental.DAL.DTOs.RentDTOs;

namespace Rental.BLL.Services.Interfaces
{
    public interface IDriverService
    {
        public Task<Response<string>> LoginDriverAsync(DriverLoginDTO driverLoginDTO);
        public Task<Response<string>> AddDriverAsync(DriverAddDTO driverAddDTO);
        public Task<Response<RentDTO>> RentMotorcycleAsync(RentAddDTO rentMotorcycleDTO);
        public Task<Response<RentDTO>> InformEndDate(RentEndDateDTO rentEndDateDTO);
    }
}
