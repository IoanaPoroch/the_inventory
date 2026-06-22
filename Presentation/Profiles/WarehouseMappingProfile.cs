using AutoMapper;
using Domain.Models;
using Presentation.DTOs.Requests;
using Presentation.DTOs.Responses;

namespace Presentation.Profiles
{
    public class WarehouseMappingProfile : Profile
    {
        public WarehouseMappingProfile()
        {
            CreateMap<Warehouse, WarehouseResponseDto>();
            CreateMap<CreateWarehouseDto, Warehouse>();
            CreateMap<UpdateWarehouseDto, Warehouse>();
        }
    }
}
