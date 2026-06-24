using AutoMapper;
using Domain.Models;
using Presentation.DTOs.Requests;
using Presentation.DTOs.Responses;

namespace Presentation.Profiles
{
    public class SupplierMappingProfile : Profile
    {
        public SupplierMappingProfile()
        {
            CreateMap<Supplier, SupplierResponseDto>();
            CreateMap<CreateSupplierDto, Supplier>();
            CreateMap<UpdateSupplierDto, Supplier>();
        }
    }
}
