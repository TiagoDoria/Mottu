using AutoMapper;
using MotorcycleAPI.Data.DTOs;
using MotorcycleAPI.Models;
using MotorcycleAPI.Repository.interfaces;
using MotorcycleAPI.Services.interfaces;

namespace MotorcycleAPI.Services
{
    public class MotorcycleService : IMotorcycleService
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IMapper _mapper;

        public MotorcycleService(IMotorcycleRepository motorcycleRepository, IMapper mapper)
        {
            _motorcycleRepository = motorcycleRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(MotorcycleDTO entity)
        {
            await _motorcycleRepository.AddAsync(_mapper.Map<Motorcycle>(entity));
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _motorcycleRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<MotorcycleDTO>> FindAllAsync()
        {
            return _mapper.Map<IEnumerable<MotorcycleDTO>>(await _motorcycleRepository.FindAllAsync());
        }

        public async Task<MotorcycleDTO> FindByIdAsync(Guid id)
        {
            return _mapper.Map<MotorcycleDTO>(await _motorcycleRepository.FindByIdAsync(id));
        }

        public async Task UpdateAsync(MotorcycleDTO entity)
        {
            await _motorcycleRepository.UpdateAsync(_mapper.Map<Motorcycle>(entity));
        }
    }
}
