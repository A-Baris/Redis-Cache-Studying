using Jewellery.BLL.Redis_Cache.Abstract;
using Jewellery.Common.IpFinder;
using Jewellery.Entity.Entities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Jewellery.BLL.Redis_Cache.Concrete
{
    public class RedisCacheService<T> : IRedisCacheService<T> where T : BaseEntity
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;
        private readonly string _entityKey;
        public RedisCacheService(int dbNo, string entityKey, string url)
        {
            _redis = ConnectionMultiplexer.Connect(url);
            _database = _redis.GetDatabase(dbNo);
            _entityKey = entityKey;
        }
        public async Task<bool> Delete(int id)
        {
            return await _database.HashDeleteAsync(_entityKey, id);
             
        }

        public async Task<IEnumerable<T>> GetAllEntities()
        {
            
                var cacheEntity = await _database.HashGetAllAsync(_entityKey);
                var entityList = cacheEntity.Select(item => JsonSerializer.Deserialize<T>(item.Value)).ToList();
                return entityList;
            
           
        }

        public async Task<T> GetEntityById(int id)
        {
            if (_database.KeyExists(_entityKey))
            {
                var entity = await _database.HashGetAsync(_entityKey, id);
                return entity.HasValue ? JsonSerializer.Deserialize<T>(entity) : null;
            }
            return null;
        }

        public async Task<T> SetEntity(T entity)
        {
            await _database.HashSetAsync(_entityKey, entity.Id.ToString(), JsonSerializer.Serialize(entity));
            return entity;
        }

        public async Task<T> Update(int updatedId,T newEntity)
        {
            var updatedEntity = await _database.HashGetAsync(_entityKey, updatedId);
            newEntity.UpdatedDate = DateTime.UtcNow;
            newEntity.UpdatedByComputer = Environment.MachineName;
            newEntity.UpdatedByIp = IpFinder.GetIpAddress();
            await SetEntity(newEntity);
            return newEntity;
        }
    }
}
