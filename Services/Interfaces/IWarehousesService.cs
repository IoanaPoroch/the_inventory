using Domain.Models;
using Services.Models;

namespace Services.Interfaces
{
    public interface IWarehousesService
    {
        Task<(List<Warehouse> Items, int TotalCount)> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken = default);
        Task<ServiceResult<Warehouse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ServiceResult<Warehouse>> CreateAsync(Warehouse warehouse, CancellationToken cancellationToken = default);
        Task<ServiceResult<Warehouse>> UpdateAsync(Guid id, Warehouse warehouse, CancellationToken cancellationToken = default);
        Task<ServiceResult<Warehouse>> PatchAsync(Guid id, string? name, string? address, CancellationToken cancellationToken = default);
        Task<ServiceResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
