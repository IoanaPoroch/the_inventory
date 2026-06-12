using DomainLayer.Models;
using Microsoft.Extensions.Logging;
using RepositoryLayer.IRepository;
using ServiceLayer.ICustomServices;

namespace ServiceLayer.CustomServices
{
    public class InventoryMovementsService : ICustomService<InventoryMovements>
    {
        private readonly IRepository<InventoryMovements> _inventoryMovementsRepository;
        private readonly ILogger<InventoryMovementsService> _logger;
        public InventoryMovementsService(IRepository<InventoryMovements> inventoryMovementsRepository, ILogger<InventoryMovementsService> logger)
        {
            _inventoryMovementsRepository = inventoryMovementsRepository;
            _logger = logger;
        }
        public void Delete(InventoryMovements entity)
        {
            try
            {
                if (entity != null)
                {
                    _inventoryMovementsRepository.Delete(entity);
                    _inventoryMovementsRepository.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting InventoryMovements with ID {Id}", entity?.Id);
                throw;
            }
        }
        public InventoryMovements Get(int Id)
        {
            try
            {
                var obj = _inventoryMovementsRepository.Get(Id);
                if (obj != null)
                {
                    return obj;
                }
                else
                {
                    _logger.LogWarning("InventoryMovements with ID {Id} not found", Id);
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errooccured while retrieving InventoryMovements with ID {Id}", Id);
                throw;
            }
        }
        public IEnumerable<InventoryMovements> GetAll()
        {
            try
            {
                var obj = _inventoryMovementsRepository.GetAll();
                if (obj != null)
                {
                    return obj;
                }
                else
                {
                    _logger.LogWarning("No InventoryMovements found");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while retrieving all InventoryMovements");
                throw;
            }
        }
        public void Insert(InventoryMovements entity)
        {
            try
            {
                if (entity != null)
                {
                    _inventoryMovementsRepository.Insert(entity);
                    _inventoryMovementsRepository.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while inserting InventoryMovements");
                throw;
            }
        }
        public void Remove(InventoryMovements entity)
        {
            try
            {
                if (entity != null)
                {
                    _inventoryMovementsRepository.Remove(entity);
                    _inventoryMovementsRepository.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while removing InventoryMovements with ID {Id}", entity?.Id);
                throw;
            }
        }
        public void Update(InventoryMovements entity)
        {
            try
            {
                if (entity != null)
                {
                    _inventoryMovementsRepository.Update(entity);
                    _inventoryMovementsRepository.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating InventoryMovements with ID {Id}", entity?.Id);
                throw;
            }
        }
    }
}