using AutoMapper;
using Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Presentation.DTOs.Requests;
using Presentation.DTOs.Responses;
using Services.Interfaces;
using Services.Models;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController(IProductsService productsService, IMapper mapper, IValidator<PaginationQuery> paginationValidator) : ApiController
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
            var result = await _productsService.GetByIdAsync(id, cancellationToken);
            return ToActionResult(result, p => Ok(_mapper.Map<ProductResponseDto>(p)));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDto dto, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(dto);
            var result = await _productsService.CreateAsync(product, cancellationToken);
            return ToActionResult(result, p => CreatedAtAction(nameof(GetById), new { id = p.Id }, _mapper.Map<ProductResponseDto>(p)));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateProductDto dto, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(dto);
            var result = await _productsService.UpdateAsync(id, product, cancellationToken);
            return ToActionResult(result, p => Ok(_mapper.Map<ProductResponseDto>(p)));
        }

        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> Patch(Guid id, PatchProductDto dto, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<PatchProductModel>(dto);
            var result = await _productsService.PatchAsync(id, model, cancellationToken);
            return ToActionResult(result, p => Ok(_mapper.Map<ProductResponseDto>(p)));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _productsService.DeleteAsync(id, cancellationToken);
            return ToActionResult(result);
        }
    }
}
