using Domain.Models;

namespace Services
{
    public interface IWarehousesService
    {
        Task<(List<Warehouse> Items, int TotalCount)> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken = default);
        Task<Warehouse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Warehouse> CreateAsync(Warehouse warehouse, CancellationToken cancellationToken = default);
        Task<Warehouse?> UpdateAsync(Guid id, Warehouse warehouse, CancellationToken cancellationToken = default);
        Task<Warehouse?> PatchAsync(Guid id, string? name, string? address, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
