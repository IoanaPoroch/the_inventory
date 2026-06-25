using Domain.Data;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using Services.Models;

namespace Services
{
    public class SuppliersService : ISuppliersService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SuppliersService> _logger;

        public SuppliersService(ApplicationDbContext context, ILogger<SuppliersService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<(List<Supplier> Items, int TotalCount)> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken = default)
        {
            try
            {
                var query = _context.Suppliers.Where(s => !s.IsDeleted);

                var totalCount = await query.CountAsync(cancellationToken);

                var items = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(cancellationToken);

                _logger.LogInformation("{Count} suppliers retrieved (page {Page}, pageSize {PageSize}).", items.Count, page, pageSize);

                return (items, totalCount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all suppliers.");
                throw;
            }
        }

        public async Task<ServiceResult<Supplier>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var supplier = await _context.Suppliers
                    .FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted, cancellationToken);

                if (supplier is null)
                {
                    _logger.LogWarning("Supplier with id {Id} was not found.", id);

                    return ServiceResult<Supplier>.Fail(ServiceError.NotFound, $"Supplier with id {id} was not found.");
                }

                _logger.LogInformation("Supplier with id {Id} was found.", id);
                return ServiceResult<Supplier>.Ok(supplier);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving supplier with id {Id}.", id);
                throw;
            }
        }

        public async Task<ServiceResult<Supplier>> CreateAsync(Supplier supplier, CancellationToken cancellationToken = default)
        {
            try
            {
                _context.Suppliers.Add(supplier);
                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Supplier with id {Id} was saved.", supplier.Id);
                return ServiceResult<Supplier>.Ok(supplier);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a supplier.");
                throw;
            }
        }

        public async Task<ServiceResult<Supplier>> UpdateAsync(Guid id, Supplier supplier, CancellationToken cancellationToken = default)
        {
            try
            {
                var existing = await _context.Suppliers
                    .FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted, cancellationToken);

                if (existing is null)
                {
                    _logger.LogWarning("Supplier with id {Id} was not found for update.", id);
                    return ServiceResult<Supplier>.Fail(ServiceError.NotFound, $"Supplier with id {id} was not found.");
                }

                existing.Name = supplier.Name;
                existing.Address = supplier.Address;

                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Supplier with id {Id} was updated.", id);
                return ServiceResult<Supplier>.Ok(existing);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating supplier with id {Id}.", id);
                throw;
            }
        }

        public async Task<ServiceResult<Supplier>> PatchAsync(Guid id, string? name, string? address, CancellationToken cancellationToken = default)
        {
            try
            {
                var existing = await _context.Suppliers
                    .FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted, cancellationToken);

                if (existing is null)
                {
                    _logger.LogWarning("Supplier with id {Id} was not found for patch.", id);
                    return ServiceResult<Supplier>.Fail(ServiceError.NotFound, $"Supplier with id {id} was not found.");
                }

                if (name is not null) existing.Name = name;
                if (address is not null) existing.Address = address;

                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Supplier with id {Id} was patched.", id);
                return ServiceResult<Supplier>.Ok(existing);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while patching supplier with id {Id}.", id);
                throw;
            }
        }

        public async Task<ServiceResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var existing = await _context.Suppliers
                    .Include(s => s.Products)
                    .FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted, cancellationToken);

                if (existing is null)
                {
                    _logger.LogWarning("Supplier with id {Id} was not found for deletion.", id);
                    return ServiceResult.Fail(ServiceError.NotFound, $"Supplier with id {id} was not found.");
                }

                foreach (var product in existing.Products.Where(p => !p.IsDeleted))
                    product.IsDeleted = true;

                existing.IsDeleted = true;
                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Supplier with id {Id} and its products were soft-deleted.", id);
                return ServiceResult.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting supplier with id {Id}.", id);
                throw;
            }
        }
    }
}
