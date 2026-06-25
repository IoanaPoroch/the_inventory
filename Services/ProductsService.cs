using Domain.Data;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using Services.Models;

namespace Services
{
    public class ProductsService : IProductsService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductsService> _logger;

        public ProductsService(ApplicationDbContext context, ILogger<ProductsService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<(List<Product> Items, int TotalCount)> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken = default)
        {
            try
            {
                var query = _context.Products.Where(p => !p.IsDeleted);

                var totalCount = await query.CountAsync(cancellationToken);

                var items = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(cancellationToken);

                _logger.LogInformation("{Count} products retrieved (page {Page}, pageSize {PageSize}).", items.Count, page, pageSize);

                return (items, totalCount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all products.");
                throw;
            }
        }

        public async Task<ServiceResult<Product>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var product = await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted, cancellationToken);

                if (product is null)
                {
                    _logger.LogWarning("Product with id {Id} was not found.", id);
                    return ServiceResult<Product>.Fail(ServiceError.NotFound, $"Product with id {id} was not found.");
                }

                _logger.LogInformation("Product with id {Id} was found.", id);
                return ServiceResult<Product>.Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving product with id {Id}.", id);
                throw;
            }
        }

        public async Task<ServiceResult<Product>> CreateAsync(Product product, CancellationToken cancellationToken = default)
        {
            try
            {
                var warehouseExists = await _context.Warehouses
                    .AnyAsync(w => w.Id == product.WarehouseId && !w.IsDeleted, cancellationToken);

                if (!warehouseExists)
                {
                    _logger.LogWarning("Warehouse with id {WarehouseId} was not found.", product.WarehouseId);
                    return ServiceResult<Product>.Fail(ServiceError.DependencyNotFound, $"Warehouse with id {product.WarehouseId} was not found.");
                }

                var supplierExists = await _context.Suppliers
                    .AnyAsync(s => s.Id == product.SupplierId && !s.IsDeleted, cancellationToken);

                if (!supplierExists)
                {
                    _logger.LogWarning("Supplier with id {SupplierId} was not found.", product.SupplierId);
                    return ServiceResult<Product>.Fail(ServiceError.DependencyNotFound, $"Supplier with id {product.SupplierId} was not found.");
                }

                _context.Products.Add(product);
                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Product with id {Id} was saved.", product.Id);
                return ServiceResult<Product>.Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a product.");
                throw;
            }
        }

        public async Task<ServiceResult<Product>> UpdateAsync(Guid id, Product product, CancellationToken cancellationToken = default)
        {
            try
            {
                var existing = await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted, cancellationToken);

                if (existing is null)
                {
                    _logger.LogWarning("Product with id {Id} was not found for update.", id);
                    return ServiceResult<Product>.Fail(ServiceError.NotFound, $"Product with id {id} was not found.");
                }

                var warehouseExists = await _context.Warehouses
                    .AnyAsync(w => w.Id == product.WarehouseId && !w.IsDeleted, cancellationToken);

                if (!warehouseExists)
                {
                    _logger.LogWarning("Warehouse with id {WarehouseId} was not found.", product.WarehouseId);
                    return ServiceResult<Product>.Fail(ServiceError.DependencyNotFound, $"Warehouse with id {product.WarehouseId} was not found.");
                }

                var supplierExists = await _context.Suppliers
                    .AnyAsync(s => s.Id == product.SupplierId && !s.IsDeleted, cancellationToken);

                if (!supplierExists)
                {
                    _logger.LogWarning("Supplier with id {SupplierId} was not found.", product.SupplierId);
                    return ServiceResult<Product>.Fail(ServiceError.DependencyNotFound, $"Supplier with id {product.SupplierId} was not found.");
                }

                existing.Name = product.Name;
                existing.Color = product.Color;
                existing.MadeIn = product.MadeIn;
                existing.Price = product.Price;
                existing.WarehouseId = product.WarehouseId;
                existing.SupplierId = product.SupplierId;

                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Product with id {Id} was updated.", id);
                return ServiceResult<Product>.Ok(existing);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating product with id {Id}.", id);
                throw;
            }
        }

        public async Task<ServiceResult<Product>> PatchAsync(Guid id, PatchProductModel model, CancellationToken cancellationToken = default)
        {
            try
            {
                var existing = await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted, cancellationToken);

                if (existing is null)
                {
                    _logger.LogWarning("Product with id {Id} was not found for patch.", id);
                    return ServiceResult<Product>.Fail(ServiceError.NotFound, $"Product with id {id} was not found.");
                }

                if (model.WarehouseId is not null)
                {
                    var warehouseExists = await _context.Warehouses
                        .AnyAsync(w => w.Id == model.WarehouseId && !w.IsDeleted, cancellationToken);

                    if (!warehouseExists)
                    {
                        _logger.LogWarning("Warehouse with id {WarehouseId} was not found.", model.WarehouseId);
                        return ServiceResult<Product>.Fail(ServiceError.DependencyNotFound, $"Warehouse with id {model.WarehouseId} was not found.");
                    }

                    existing.WarehouseId = model.WarehouseId.Value;
                }

                if (model.SupplierId is not null)
                {
                    var supplierExists = await _context.Suppliers
                        .AnyAsync(s => s.Id == model.SupplierId && !s.IsDeleted, cancellationToken);

                    if (!supplierExists)
                    {
                        _logger.LogWarning("Supplier with id {SupplierId} was not found.", model.SupplierId);
                        return ServiceResult<Product>.Fail(ServiceError.DependencyNotFound, $"Supplier with id {model.SupplierId} was not found.");
                    }

                    existing.SupplierId = model.SupplierId.Value;
                }

                if (model.Name is not null) existing.Name = model.Name;
                if (model.Color is not null) existing.Color = model.Color;
                if (model.MadeIn is not null) existing.MadeIn = model.MadeIn;
                if (model.Price is not null) existing.Price = model.Price.Value;

                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Product with id {Id} was patched.", id);
                return ServiceResult<Product>.Ok(existing);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while patching product with id {Id}.", id);
                throw;
            }
        }

        public async Task<ServiceResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var existing = await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted, cancellationToken);

                if (existing is null)
                {
                    _logger.LogWarning("Product with id {Id} was not found for deletion.", id);
                    return ServiceResult.Fail(ServiceError.NotFound, $"Product with id {id} was not found.");
                }

                existing.IsDeleted = true;
                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Product with id {Id} was deleted.", id);
                return ServiceResult.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting product with id {Id}.", id);
                throw;
            }
        }
    }
}
