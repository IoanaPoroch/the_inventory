using AutoMapper;
using Domain.Models;
using Presentation.DTOs.Requests;
using Presentation.DTOs.Responses;

namespace Presentation.Profiles
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductResponseDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
        }
    }
}
