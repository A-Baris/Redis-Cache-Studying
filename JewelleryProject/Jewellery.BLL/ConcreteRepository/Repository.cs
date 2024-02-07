using Jewellery.BLL.AbstractRepository;
using Jewellery.Dal.JewelleryContext;
using Jewellery.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jewellery.BLL.ConcreteRepository
{
    public class Repository<T> :IRepository<T> where T : BaseEntity
    {
        private readonly ProjectContext _context;


        public Repository(ProjectContext context)
        {
            _context = context;


        }


        public async Task<string> Create(T entity)
        {
            try
            {
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                return await _context.Set<T>().ToListAsync();


            }
            catch
            {
                throw;
            }
        }

        public async Task<T> GetbyId(int id)
        {
            try
            {
                return await _context.Set<T>().FindAsync(id);

            }
            catch
            {
                throw;

            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var entity = await _context.Set<T>().FindAsync(id);
                if (entity == null)
                {
                    return false; 
                }
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> Update(T entity)
        {
            try
            {
                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();
                return null;
            }
            catch
            {
                throw;
            }
        }
    }
}

