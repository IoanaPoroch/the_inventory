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
    public class SuppliersController(ISuppliersService suppliersService, IMapper mapper, IValidator<PaginationQuery> paginationValidator) : ApiController
    {
        private readonly ISuppliersService _suppliersService = suppliersService;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<PaginationQuery> _paginationValidator = paginationValidator;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery query, CancellationToken cancellationToken)
        {
            var validation = await _paginationValidator.ValidateAsync(query, cancellationToken);
            if (!validation.IsValid)
                return BadRequest(validation.Errors.Select(e => e.ErrorMessage));

            var (items, totalCount) = await _suppliersService.GetAllAsync(query.Page, query.PageSize, cancellationToken);

            var response = new PagedResponse<SupplierResponseDto>
            {
                Data = _mapper.Map<List<SupplierResponseDto>>(items),
                Page = query.Page,
                PageSize = query.PageSize,
                TotalCount = totalCount
            };

            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _suppliersService.GetByIdAsync(id, cancellationToken);
            return ToActionResult(result, s => Ok(_mapper.Map<SupplierResponseDto>(s)));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSupplierDto dto, CancellationToken cancellationToken)
        {
            var supplier = _mapper.Map<Supplier>(dto);
            var result = await _suppliersService.CreateAsync(supplier, cancellationToken);
            return ToActionResult(result, s => CreatedAtAction(nameof(GetById), new { id = s.Id }, _mapper.Map<SupplierResponseDto>(s)));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateSupplierDto dto, CancellationToken cancellationToken)
        {
            var supplier = _mapper.Map<Supplier>(dto);
            var result = await _suppliersService.UpdateAsync(id, supplier, cancellationToken);
            return ToActionResult(result, s => Ok(_mapper.Map<SupplierResponseDto>(s)));
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> Patch(Guid id, PatchSupplierDto dto, CancellationToken cancellationToken)
        {
            var result = await _suppliersService.PatchAsync(id, dto.Name, dto.Address, cancellationToken);
            return ToActionResult(result, s => Ok(_mapper.Map<SupplierResponseDto>(s)));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _suppliersService.DeleteAsync(id, cancellationToken);
            return ToActionResult(result);
        }
    }
}
