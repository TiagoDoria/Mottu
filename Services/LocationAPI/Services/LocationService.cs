using AutoMapper;
using LocationAPI.Data.DTOs;
using LocationAPI.Models;
using LocationAPI.Repository.Interfaces;
using LocationAPI.Services.Interfaces;
using LocationAPI.Utils;

namespace LocationAPI.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        public LocationService(ILocationRepository locationRepository, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(LocationDTO entity)
        {
            entity.ExpectedEndDate = entity.StartDate.AddDays(entity.PlanDays);
            await _locationRepository.AddAsync(_mapper.Map<Location>(entity));
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _locationRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<LocationDTO>> FindAllAsync()
        {
            return _mapper.Map<IEnumerable<LocationDTO>>(await _locationRepository.FindAllAsync());
        }

        public async Task<LocationDTO> FindByIdAsync(Guid id)
        {
            return _mapper.Map<LocationDTO>(await _locationRepository.FindByIdAsync(id));
        }

        public async Task UpdateAsync(LocationDTO entity)
        {
            await _locationRepository.UpdateAsync(_mapper.Map<Location>(entity));
        }

        public async Task CalculatePrice(LocationDTO entity)
        {
            switch (entity.PlanDays)
            {
                case 7:
                    var sevenDaysPlan = new LeasingCalculator(new SevenDaysLeasingPlan());
                    entity.TotalPrice = sevenDaysPlan.CalculateLeasingValue(entity.StartDate, entity.EndDate); break;
                case 15:
                    var fifteenDaysPlan = new LeasingCalculator(new FifteenDaysLeasingPlan());
                    entity.TotalPrice = fifteenDaysPlan.CalculateLeasingValue(entity.StartDate, entity.EndDate); break;
                case 30:
                    var thirtyDaysPlan = new LeasingCalculator(new ThirtyDaysLeasingPlan());
                    entity.TotalPrice = thirtyDaysPlan.CalculateLeasingValue(entity.StartDate, entity.EndDate); break;
            }
                
            await _locationRepository.UpdateAsync(_mapper.Map<Location>(entity));
        }
    }
}
