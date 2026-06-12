using DomainLayer.Models;
using Microsoft.Extensions.Logging;
using RepositoryLayer.IRepository;
using ServiceLayer.ICustomServices;

namespace ServiceLayer.CustomServices
{
    public class ProductService : ICustomService<Product>
    {
        private readonly IRepository<Product> _productRepository;
        private readonly ILogger<ProductService> _logger;
        public ProductService(IRepository<Product> productRepository, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }
        public void Delete(Product entity)
        {
            try
            {
                if (entity != null)
                {
                    _productRepository.Delete(entity);
                    _productRepository.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while deleting product with id {Id}", entity?.Id);
                throw;
            }
        }
        public Product Get(int Id)
        {
            try
            {
                var obj = _productRepository.Get(Id);
                if (obj != null)
                {
                    return obj;
                }
                else
                {
                    _logger.LogWarning("Product with id {Id} not found", Id);
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while retrieving product with id {Id}", Id);
                throw;
            }
        }
        public IEnumerable<Product> GetAll()
        {
            try
            {
                var obj = _productRepository.GetAll();
                if (obj != null)
                {
                    return obj;
                }
                else
                {
                    _logger.LogWarning("No products found");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while retrieving all products");
                throw;
            }
        }
        public void Insert(Product entity)
        {
            try
            {
                if (entity != null)
                {
                    _productRepository.Insert(entity);
                    _productRepository.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inserting product with name {Name}", entity?.Name);
                throw;
            }
        }
        public void Remove(Product entity)
        {
            try
            {
                if (entity != null)
                {
                    _productRepository.Remove(entity);
                    _productRepository.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Update(Product entity)
        {
            try
            {
                if (entity != null)
                {
                    _productRepository.Update(entity);
                    _productRepository.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating product with id {Id}", entity?.Id);
                throw;
            }
        }
    }
}