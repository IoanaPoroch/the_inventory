using Domain.Models;
using Services.Models;

namespace Services.Interfaces
{
    public interface IProductsService
    {
        Task<(List<Product> Items, int TotalCount)> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken = default);
        Task<ServiceResult<Product>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ServiceResult<Product>> CreateAsync(Product product, CancellationToken cancellationToken = default);
        Task<ServiceResult<Product>> UpdateAsync(Guid id, Product product, CancellationToken cancellationToken = default);
        Task<ServiceResult<Product>> PatchAsync(Guid id, PatchProductModel model, CancellationToken cancellationToken = default);
        Task<ServiceResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
