
using Rental.DAL;
using Rental.DAL.DTOs.MotorcycleDTOs;

namespace Rental.BLL.Services.Interfaces
{
    public interface IMotorcycleService
    {
        public Task<Response<List<MotorcycleDTO>>> GetMotorcycleListAsync(int offset, int limit);
        public Task<Response<MotorcycleDTO>> GetMotorcycleByIdAsync(int id);
        public Task<Response<MotorcycleDTO>> GetMotorcycleByPlateAsync(string plate);
        public Task<Response<MotorcycleDTO>> AddMotorcycleAsync(MotorcycleAddDTO motorcycleAddDTO);
        public Task<Response<MotorcycleDTO>> UpdateMotorcyclePlateAsync(int id, string newPlate);
        public Task<Response<MotorcycleDTO>> RemoveMotorcycleAsync(int id);
    }
}
