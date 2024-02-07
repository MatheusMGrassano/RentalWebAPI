using Rental.DAL;
using Rental.DAL.Entities;
using Rental.DAL.DataContext;
using Rental.BLL.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Rental.BLL.Validators;
using Rental.DAL.DTOs.MotorcycleDTOs;

namespace Rental.BLL.Services
{
    public class MotorcycleService : IMotorcycleService
    {
        private readonly AppDbContext _context;

        public MotorcycleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Response<List<MotorcycleDTO>>> GetMotorcycleListAsync(int offset, int limit)
        {
            try
            {
                var motorcycleList = new List<MotorcycleDTO>();

                foreach(Motorcycle motorcycle in await _context.Motorcycle.Skip(offset).Take(limit).ToListAsync())
                {
                    motorcycleList.Add(CreateMotorcycleDTO(motorcycle));
                }

                return Response<List<MotorcycleDTO>>.Success(motorcycleList);
            }
            catch(Exception ex) 
            {
                return Response<List<MotorcycleDTO>>.Error(ex.Message);
            }
        }
        public async Task<Response<MotorcycleDTO>> GetMotorcycleByIdAsync(int id)
        {
            try
            {
                var motorcycle = await _context.Motorcycle.FirstOrDefaultAsync(x => x.Id == id);

                if (motorcycle is null)
                    return Response<MotorcycleDTO>.NotFound();

                var motorcycleDTO = CreateMotorcycleDTO(motorcycle);

                return Response<MotorcycleDTO>.Success(motorcycleDTO);
            }
            catch(Exception ex)
            {
                return Response<MotorcycleDTO>.Error(ex.Message);
            }
        }

        public async Task<Response<MotorcycleDTO>> GetMotorcycleByPlateAsync(string plate)
        {
            try
            {
                var motorcycle = await _context.Motorcycle.FirstOrDefaultAsync(x => x.Plate == plate.ToUpper());

                if (motorcycle is null)
                    return Response<MotorcycleDTO>.NotFound();

                var motorcycleDTO = CreateMotorcycleDTO(motorcycle);

                return Response<MotorcycleDTO>.Success(motorcycleDTO);
            }
            catch (Exception ex)
            {
                return Response<MotorcycleDTO>.Error(ex.Message);
            }
        }
        public async Task<Response<MotorcycleDTO>> AddMotorcycleAsync(MotorcycleAddDTO motorcycleAddDTO)
        {
            try
            {
                motorcycleAddDTO.Plate = motorcycleAddDTO.Plate.ToUpper();

                MotorcycleValidator.ValidatePlate(motorcycleAddDTO.Plate);
                MotorcycleValidator.ValidateManufactureYear(motorcycleAddDTO.ManufactureYear);

                if (await _context.Motorcycle.FirstOrDefaultAsync(x => x.Plate == motorcycleAddDTO.Plate) is not null)
                    throw new Exception("Motorcycle already registered.");

                var newMotorcycle = CreateMotorcycle(motorcycleAddDTO);

                await _context.Motorcycle.AddAsync(newMotorcycle);
                await _context.SaveChangesAsync();

                var motorcycle = FindMotorcycleAsync(newMotorcycle.Id).Result;

                return Response<MotorcycleDTO>.Success(CreateMotorcycleDTO(motorcycle));
            }
            catch(Exception ex)
            {
                return Response<MotorcycleDTO>.Error(ex.Message);
            }
        }

        public async Task<Response<MotorcycleDTO>> UpdateMotorcyclePlateAsync(int id, string newPlate)
        {
            try
            {
                MotorcycleValidator.ValidatePlate(newPlate);

                var motorcycle = FindMotorcycleAsync(id).Result;

                if (motorcycle is null)
                    return Response<MotorcycleDTO>.NotFound();

                motorcycle.Plate = newPlate;

                _context.Motorcycle.Update(motorcycle);
                await _context.SaveChangesAsync();

                return Response<MotorcycleDTO>.Success(CreateMotorcycleDTO(motorcycle));
            }
            catch (Exception ex)
            {
                return Response<MotorcycleDTO>.Error(ex.Message);
            }
        }

        public async Task<Response<MotorcycleDTO>> RemoveMotorcycleAsync(int id)
        {
            try
            {
                var motorcycle = FindMotorcycleAsync(id).Result;
                
                if (motorcycle is null)
                    return Response<MotorcycleDTO>.NotFound();

                if (motorcycle.Available == false)
                    throw new Exception("Unable to remove. The motorcycle is currently rented.");

                _context.Motorcycle.Remove(motorcycle);
                await _context.SaveChangesAsync();

                return Response<MotorcycleDTO>.Success(CreateMotorcycleDTO(motorcycle));
            }
            catch (Exception ex)
            {
                return Response<MotorcycleDTO>.Error(ex.Message);
            }
        }
        public async Task<Motorcycle> FindMotorcycleAsync(int id)
        {
            return await _context.Motorcycle.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception();
        }

        public Motorcycle CreateMotorcycle(MotorcycleAddDTO motorcycleAddDTO)
        {
            return new Motorcycle
            {
                ManufactureYear = motorcycleAddDTO.ManufactureYear,
                Model = motorcycleAddDTO.Model,
                Plate = motorcycleAddDTO.Plate,
                Available = true
            };
        }

        public MotorcycleDTO CreateMotorcycleDTO(Motorcycle motorcycle)
        {
            return new MotorcycleDTO
            {
                Id = motorcycle.Id,
                ManufactureYear = motorcycle.ManufactureYear,
                Model = motorcycle.Model,
                Plate = motorcycle.Plate,
                Available = motorcycle.Available
            };
        }
    }
}

