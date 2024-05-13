using AutoMapper;
using OrderAPI.Data.DTOs;
using OrderAPI.Models;

namespace OrderAPI.Config
{
    public class Mappings
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<OrderDTO, Order>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
