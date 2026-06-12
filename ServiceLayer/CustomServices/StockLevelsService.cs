using DomainLayer.Models;
using Microsoft.Extensions.Logging;
using RepositoryLayer.IRepository;
using ServiceLayer.ICustomServices;

namespace ServiceLayer.CustomServices
{
    public class StockLevelsService : ICustomService<StockLevels>
    {
        private readonly IRepository<StockLevels> _stockLevelsRepository;
        private readonly ILogger<StockLevelsService> _logger;
        public StockLevelsService(IRepository<StockLevels> stockLevelsRepository, ILogger<StockLevelsService> logger)
        {
            _stockLevelsRepository = stockLevelsRepository;
            _logger = logger;
        }
        public void Delete(StockLevels entity)
        {
            try
            {
                if (entity != null)
                {
                    _stockLevelsRepository.Delete(entity);
                    _stockLevelsRepository.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting StockLevels with Id {Id}", entity.Id);
                throw;
            }
        }
        public StockLevels Get(int Id)
        {
            try
            {
                var obj = _stockLevelsRepository.Get(Id);
                if (obj != null)
                {
                    return obj;
                }
                else
                {
                    _logger.LogWarning("StockLevels with Id {Id} not found", Id);
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving StockLevels with Id {Id}", Id);
                throw;
            }
        }
        public IEnumerable<StockLevels> GetAll()
        {
            try
            {
                var obj = _stockLevelsRepository.GetAll();
                if (obj != null)
                {
                    return obj;
                }
                else
                {
                    _logger.LogWarning("No StockLevels found");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all StockLevels");
                throw;
            }
        }
        public void Insert(StockLevels entity)
        {
            try
            {
                if (entity != null)
                {
                    _stockLevelsRepository.Insert(entity);
                    _stockLevelsRepository.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while inserting StockLevels");
                throw;
            }
        }
        public void Remove(StockLevels entity)
        {
            try
            {
                if (entity != null)
                {
                    _stockLevelsRepository.Remove(entity);
                    _stockLevelsRepository.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while removing StockLevels with Id {Id}", entity.Id);
                throw;
            }
        }
        public void Update(StockLevels entity)
        {
            try
            {
                if (entity != null)
                {
                    _stockLevelsRepository.Update(entity);
                    _stockLevelsRepository.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating StockLevels with Id {Id}", entity.Id);
                throw;
            }
        }
    }
}