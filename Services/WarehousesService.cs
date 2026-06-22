using Domain.Data;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Services
{
    public class WarehousesService : IWarehousesService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<WarehousesService> _logger;

        public WarehousesService(ApplicationDbContext context, ILogger<WarehousesService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<(List<Warehouse> Items, int TotalCount)> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken = default)
        {
            try
            {
                var query = _context.Warehouses.Where(w => !w.IsDeleted);

                var totalCount = await query.CountAsync(cancellationToken);

                var items = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(cancellationToken);

                _logger.LogInformation("{Count} warehouses retrieved (page {Page}, pageSize {PageSize}).", items.Count, page, pageSize);

                return (items, totalCount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all warehouses.");
                throw;
            }
        }

        public async Task<Warehouse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var warehouse = await _context.Warehouses
                    .FirstOrDefaultAsync(w => w.Id == id && !w.IsDeleted, cancellationToken);

                if (warehouse is null)
                {
                    _logger.LogWarning("Warehouse with id {Id} was not found.", id);
                    return null;
                }

                _logger.LogInformation("Warehouse with id {Id} was found.", id);

                return warehouse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving warehouse with id {Id}.", id);
                throw;
            }
        }

        public async Task<Warehouse> CreateAsync(Warehouse warehouse, CancellationToken cancellationToken = default)
        {
            try
            {
                warehouse.Id = Guid.NewGuid();

                _context.Warehouses.Add(warehouse);
                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Warehouse with id {Id} was saved.", warehouse.Id);

                return warehouse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a warehouse.");
                throw;
            }
        }

        public async Task<Warehouse?> UpdateAsync(Guid id, Warehouse warehouse, CancellationToken cancellationToken = default)
        {
            try
            {
                var existing = await _context.Warehouses
                    .FirstOrDefaultAsync(w => w.Id == id && !w.IsDeleted, cancellationToken);

                if (existing is null)
                {
                    _logger.LogWarning("Warehouse with id {Id} was not found for update.", id);
                    return null;
                }

                existing.Name = warehouse.Name;
                existing.Address = warehouse.Address;

                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Warehouse with id {Id} was updated.", id);

                return existing;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating warehouse with id {Id}.", id);
                throw;
            }
        }

        public async Task<Warehouse?> PatchAsync(Guid id, string? name, string? address, CancellationToken cancellationToken = default)
        {
            try
            {
                var existing = await _context.Warehouses
                    .FirstOrDefaultAsync(w => w.Id == id && !w.IsDeleted, cancellationToken);

                if (existing is null)
                {
                    _logger.LogWarning("Warehouse with id {Id} was not found for patch.", id);
                    return null;
                }

                if (name is not null) existing.Name = name;
                if (address is not null) existing.Address = address;

                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Warehouse with id {Id} was patched.", id);

                return existing;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while patching warehouse with id {Id}.", id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var existing = await _context.Warehouses
                    .FirstOrDefaultAsync(w => w.Id == id && !w.IsDeleted, cancellationToken);

                if (existing is null)
                {
                    _logger.LogWarning("Warehouse with id {Id} was not found for deletion.", id);
                    return false;
                }

                existing.IsDeleted = true;

                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Warehouse with id {Id} was deleted.", id);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting warehouse with id {Id}.", id);
                throw;
            }
        }
    }
}
