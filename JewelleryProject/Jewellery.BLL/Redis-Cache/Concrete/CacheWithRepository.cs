using Jewellery.BLL.AbstractRepository;
using Jewellery.Dal.JewelleryContext;
using Jewellery.Dal.Redis_Cache.Entities;
using Jewellery.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Jewellery.Dal.Redis_Cache.Concrete
{
    public class CacheWithRepository<T> : ICacheWithRepository<T> where T : BaseEntity
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;
        private readonly string _entityKey;
        private readonly ProjectContext _context;

        public CacheWithRepository(int dbNo, string entityKey, string url, ProjectContext context)
        {
            _redis = ConnectionMultiplexer.Connect(url);
            _database = _redis.GetDatabase(dbNo);
            _entityKey = entityKey;
            _context = context;
        }



        public async Task<bool> Delete(int id)
        {
            if (_database.KeyExists(_entityKey))
            {
                await _database.HashDeleteAsync(_entityKey,id);

            }
            
            var entity = await _context.Set<T>().FirstOrDefaultAsync(x=>x.Id==id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            else { return false; }

        }

        public async Task<IEnumerable<T>> GetAll()
        {
            if (_database.KeyExists(_entityKey))
            {
                var cacheEntity = await _database.HashGetAllAsync(_entityKey);
                var entityList = cacheEntity.Select(item => JsonSerializer.Deserialize<T>(item.Value)).ToList();
                return entityList;
            }
            else
            {
                var entities = await _context.Set<T>().ToListAsync();
                foreach (var item in entities)
                {
                    await _database.HashSetAsync(_entityKey, item.Id, JsonSerializer.Serialize(item));

                }
                return entities;
            }

        }

        public async Task<T> GetbyId(int id)
        {
            if (_database.KeyExists(_entityKey))
            {
                var entity = await _database.HashGetAsync(_entityKey, id);
                return entity.HasValue ? JsonSerializer.Deserialize<T>(entity) : null;
            }
            else
            {
                return await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public async Task<string> Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            var newEntity = await _context.Set<T>().FirstOrDefaultAsync(x=>x.Id ==entity.Id);
            await _database.HashSetAsync(_entityKey, newEntity.Id.ToString(), JsonSerializer.Serialize(newEntity));
            await _context.SaveChangesAsync();
            return "Successful";

        }
        public async Task<string> Update(T entity)
        {
            

            if (_database.KeyExists(_entityKey))
            {
                var oldEntity = await _database.HashGetAsync(_entityKey, entity.Id);
                if (oldEntity.HasValue)
                {
                    await _database.HashSetAsync(_entityKey, entity.Id, JsonSerializer.Serialize(entity));
                }




            }
            var preEntity = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (preEntity != null)
            {
                _context.Entry(preEntity).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
                return "successful";
            }
            else
            {
                return "Entity does not exist in database";
            }

        }

    }

}



