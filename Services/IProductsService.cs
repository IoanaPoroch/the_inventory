using Domain.Models;

namespace Services
{
    public interface IProductsService
    {
        Task<(List<Product> Items, int TotalCount)> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken = default);
        Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Product?> CreateAsync(Product product, CancellationToken cancellationToken = default);
        Task<Product?> UpdateAsync(Guid id, Product product, CancellationToken cancellationToken = default);
        Task<Product?> PatchAsync(Guid id, string? name, string? color, string? madeIn, decimal? price, Guid? warehouseId, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
