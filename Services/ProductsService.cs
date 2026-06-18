using Domain.Data;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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

        public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var product = await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted, cancellationToken);

                if (product is null)
                {
                    _logger.LogWarning("Product with id {Id} was not found.", id);
                    return null;
                }

                _logger.LogInformation("Product with id {Id} was found.", id);

                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving product with id {Id}.", id);
                throw;
            }
        }

        public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
        {
            try
            {
                product.Id = Guid.NewGuid();

                _context.Products.Add(product);
                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Product with id {Id} was saved.", product.Id);

                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a product.");
                throw;
            }
        }

        public async Task<Product?> UpdateAsync(Guid id, Product product, CancellationToken cancellationToken = default)
        {
            try
            {
                var existing = await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted, cancellationToken);

                if (existing is null)
                {
                    _logger.LogWarning("Product with id {Id} was not found for update.", id);
                    return null;
                }

                existing.Name = product.Name;
                existing.Color = product.Color;
                existing.MadeIn = product.MadeIn;
                existing.Price = product.Price;

                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Product with id {Id} was updated.", id);

                return existing;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating product with id {Id}.", id);
                throw;
            }
        }

        public async Task<Product?> PatchAsync(Guid id, string? name, string? color, string? madeIn, decimal? price, CancellationToken cancellationToken = default)
        {
            try
            {
                var existing = await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted, cancellationToken);

                if (existing is null)
                {
                    _logger.LogWarning("Product with id {Id} was not found for patch.", id);
                    return null;
                }

                if (name is not null) existing.Name = name;
                if (color is not null) existing.Color = color;
                if (madeIn is not null) existing.MadeIn = madeIn;
                if (price is not null) existing.Price = price.Value;

                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Product with id {Id} was patched.", id);

                return existing;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while patching product with id {Id}.", id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var existing = await _context.Products
                    .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted, cancellationToken);

                if (existing is null)
                {
                    _logger.LogWarning("Product with id {Id} was not found for deletion.", id);
                    return false;
                }

                existing.IsDeleted = true;

                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Product with id {Id} was deleted.", id);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting product with id {Id}.", id);
                throw;
            }
        }
    }
}
