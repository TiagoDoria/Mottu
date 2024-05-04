﻿using MotorcycleAPI.Data.DTOs;
using MotorcycleAPI.Models;

namespace MotorcycleAPI.Services.interfaces
{
    public interface IMotorcycleService
    {
        Task AddAsync(MotorcycleDTO entity);
        Task UpdateAsync(MotorcycleDTO entity);
        Task<IEnumerable<MotorcycleDTO>> FindAllAsync();
        Task<MotorcycleDTO> FindByIdAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);
    }
}
