using AutoMapper;
using LocationAPI.Data.DTOs;
using LocationAPI.Models;

namespace LocationAPI.Config
{
    public class Mappings
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<LocationDTO, Location>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
