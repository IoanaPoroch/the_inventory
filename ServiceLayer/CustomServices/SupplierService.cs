using DomainLayer.Models;
using Microsoft.Extensions.Logging;
using RepositoryLayer.IRepository;
using ServiceLayer.ICustomServices;

namespace ServiceLayer.CustomServices
{
    public class SupplierService : ICustomService<Supplier>
    {
        private readonly IRepository<Supplier> _supplierRepository;
        private readonly ILogger<SupplierService> _logger;
        public SupplierService(IRepository<Supplier> supplierRepository, ILogger<SupplierService> logger)
        {
            _supplierRepository = supplierRepository;
            _logger = logger;
        }
        public void Delete(Supplier entity)
        {
            try
            {
                if (entity != null)
                {
                    _supplierRepository.Delete(entity);
                    _supplierRepository.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting supplier with Id {Id}", entity?.Id);
                throw;
            }
        }
        public Supplier Get(int Id)
        {
            try
            {
                var obj = _supplierRepository.Get(Id);
                if (obj != null)
                {
                    return obj;
                }
                else
                {
                    _logger.LogWarning("Supplier with Id {Id} not found", Id);
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving supplier with Id {Id}", Id);
                throw;
            }
        }
        public IEnumerable<Supplier> GetAll()
        {
            try
            {
                var obj = _supplierRepository.GetAll();
                if (obj != null)
                {
                    return obj;
                }
                else
                {
                    _logger.LogWarning("No suppliers found");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all suppliers");
                throw;
            }
        }
        public void Insert(Supplier entity)
        {
            try
            {
                if (entity != null)
                {
                    _supplierRepository.Insert(entity);
                    _supplierRepository.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while inserting supplier with Name {Name}", entity?.Name);
                throw;
            }
        }
        public void Remove(Supplier entity)
        {
            try
            {
                if (entity != null)
                {
                    _supplierRepository.Remove(entity);
                    _supplierRepository.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while removing supplier with Id {Id}", entity?.Id);
                throw;
            }
        }
        public void Update(Supplier entity)
        {
            try
            {
                if (entity != null)
                {
                    _supplierRepository.Update(entity);
                    _supplierRepository.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating supplier with Id {Id}", entity?.Id);
                throw;
            }
        }
    }
}