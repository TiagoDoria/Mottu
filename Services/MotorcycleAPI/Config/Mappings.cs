using AutoMapper;
using MotorcycleAPI.Data.DTOs;
using MotorcycleAPI.Models;

namespace MotorcycleAPI.Config
{
    public class Mappings
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<MotorcycleDTO, Motorcycle>().ReverseMap();
                config.CreateMap<MotorcycleUpdateDTO, Motorcycle>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
