using AutoMapper;
using Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Presentation.DTOs.Requests;
using Presentation.DTOs.Responses;
using Services;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IProductsService productsService, IMapper mapper, IValidator<PaginationQuery> paginationValidator) : ControllerBase
    {
        private readonly IProductsService _productsService = productsService;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<PaginationQuery> _paginationValidator = paginationValidator;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery query, CancellationToken cancellationToken)
        {
            var validation = await _paginationValidator.ValidateAsync(query, cancellationToken);
            if (!validation.IsValid)
                return BadRequest(validation.Errors.Select(e => e.ErrorMessage));

            var (items, totalCount) = await _productsService.GetAllAsync(query.Page, query.PageSize, cancellationToken);

            var response = new PagedResponse<ProductResponseDto>
            {
                Data = _mapper.Map<List<ProductResponseDto>>(items),
                Page = query.Page,
                PageSize = query.PageSize,
                TotalCount = totalCount
            };

            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var product = await _productsService.GetByIdAsync(id, cancellationToken);

            if (product is null)
                return NotFound();

            return Ok(_mapper.Map<ProductResponseDto>(product));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto dto, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(dto);
            var created = await _productsService.CreateAsync(product, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, _mapper.Map<ProductResponseDto>(created));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateProductDto dto, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(dto);
            var updated = await _productsService.UpdateAsync(id, product, cancellationToken);

            if (updated is null)
                return NotFound();

            return Ok(_mapper.Map<ProductResponseDto>(updated));
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> Patch(Guid id, PatchProductDto dto, CancellationToken cancellationToken)
        {
            var patched = await _productsService.PatchAsync(id, dto.Name, dto.Color, dto.MadeIn, dto.Price, cancellationToken);

            if (patched is null)
                return NotFound();

            return Ok(_mapper.Map<ProductResponseDto>(patched));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var deleted = await _productsService.DeleteAsync(id, cancellationToken);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
