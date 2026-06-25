using AutoMapper;
using Domain.Models;
using Presentation.DTOs.Requests;
using Presentation.DTOs.Responses;
using Services.Models;

namespace Presentation.Profiles
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductResponseDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
            CreateMap<PatchProductDto, PatchProductModel>();
        }
    }
}
