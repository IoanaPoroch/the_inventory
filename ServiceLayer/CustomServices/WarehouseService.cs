using DomainLayer.Models;
using Microsoft.Extensions.Logging;
using RepositoryLayer.IRepository;
using ServiceLayer.ICustomServices;

namespace ServiceLayer.CustomServices
{
    public class WarehouseService : ICustomService<Warehouse>
    {
        private readonly IRepository<Warehouse> _warehouseRepository;
        private readonly ILogger<WarehouseService> _logger;
        public WarehouseService(IRepository<Warehouse> warehouseRepository, ILogger<WarehouseService> logger)
        {
            _warehouseRepository = warehouseRepository;
            _logger = logger;
        }
        public void Delete(Warehouse entity)
        {
            try
            {
                if (entity != null)
                {
                    _warehouseRepository.Delete(entity);
                    _warehouseRepository.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the warehouse.");
                throw;
            }
        }
        public Warehouse Get(int Id)
        {
            try
            {
                var obj = _warehouseRepository.Get(Id);
                if (obj != null)
                {
                    return obj;
                }
                else
                {
                    _logger.LogWarning("Warehouse with Id {Id} not found.", Id);
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the warehouse with Id {Id}.", Id);
                throw;
            }
        }
        public IEnumerable<Warehouse> GetAll()
        {
            try
            {
                var obj = _warehouseRepository.GetAll();
                if (obj != null)
                {
                    return obj;
                }
                else
                {
                    _logger.LogWarning("No warehouses found.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all warehouses.");
                throw;
            }

        }
        public void Insert(Warehouse entity)
        {
            try
            {
                if (entity != null)
                {
                    _warehouseRepository.Insert(entity);
                    _warehouseRepository.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while inserting a new warehouse.");
                throw;
            }
        }

        public void Remove(Warehouse entity)
        {
            try
            {
                if (entity != null)
                {
                    _warehouseRepository.Remove(entity);
                    _warehouseRepository.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while removing the warehouse.");
                throw;
            }
        }
        public void Update(Warehouse entity)
        {
            try
            {
                if (entity != null)
                {
                    _warehouseRepository.Update(entity);
                    _warehouseRepository.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the warehouse.");
                throw;
            }
        }

    }
}
