using AutoMapper;
using Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Presentation.DTOs.Requests;
using Presentation.DTOs.Responses;
using Services.Interfaces;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    public class WarehousesController(IWarehousesService warehousesService, IMapper mapper, IValidator<PaginationQuery> paginationValidator) : ApiController
    {
        private readonly IWarehousesService _warehousesService = warehousesService;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<PaginationQuery> _paginationValidator = paginationValidator;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery query, CancellationToken cancellationToken)
        {
            var validation = await _paginationValidator.ValidateAsync(query, cancellationToken);
            if (!validation.IsValid)
                return BadRequest(validation.Errors.Select(e => e.ErrorMessage));

            var (items, totalCount) = await _warehousesService.GetAllAsync(query.Page, query.PageSize, cancellationToken);

            var response = new PagedResponse<WarehouseResponseDto>
            {
                Data = _mapper.Map<List<WarehouseResponseDto>>(items),
                Page = query.Page,
                PageSize = query.PageSize,
                TotalCount = totalCount
            };

            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _warehousesService.GetByIdAsync(id, cancellationToken);
            return ToActionResult(result, w => Ok(_mapper.Map<WarehouseResponseDto>(w)));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateWarehouseDto dto, CancellationToken cancellationToken)
        {
            var warehouse = _mapper.Map<Warehouse>(dto);
            var result = await _warehousesService.CreateAsync(warehouse, cancellationToken);
            return ToActionResult(result, w => CreatedAtAction(nameof(GetById), new { id = w.Id }, _mapper.Map<WarehouseResponseDto>(w)));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateWarehouseDto dto, CancellationToken cancellationToken)
        {
            var warehouse = _mapper.Map<Warehouse>(dto);
            var result = await _warehousesService.UpdateAsync(id, warehouse, cancellationToken);
            return ToActionResult(result, w => Ok(_mapper.Map<WarehouseResponseDto>(w)));
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> Patch(Guid id, PatchWarehouseDto dto, CancellationToken cancellationToken)
        {
            var result = await _warehousesService.PatchAsync(id, dto.Name, dto.Address, cancellationToken);
            return ToActionResult(result, w => Ok(_mapper.Map<WarehouseResponseDto>(w)));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _warehousesService.DeleteAsync(id, cancellationToken);
            return ToActionResult(result);
        }
    }
}
