using Domain.Models;
using Services.Models;

namespace Services.Interfaces
{
    public interface ISuppliersService
    {
        Task<(List<Supplier> Items, int TotalCount)> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken = default);
        Task<ServiceResult<Supplier>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ServiceResult<Supplier>> CreateAsync(Supplier supplier, CancellationToken cancellationToken = default);
        Task<ServiceResult<Supplier>> UpdateAsync(Guid id, Supplier supplier, CancellationToken cancellationToken = default);
        Task<ServiceResult<Supplier>> PatchAsync(Guid id, string? name, string? address, CancellationToken cancellationToken = default);
        Task<ServiceResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
