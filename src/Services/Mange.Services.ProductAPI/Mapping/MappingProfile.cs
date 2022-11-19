using AutoMapper;
using Mange.Services.ProductAPI.Entities;
using Mange.Services.ProductAPI.Models;

namespace Mange.Services.ProductAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
