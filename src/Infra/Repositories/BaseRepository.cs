
using Domain.Entities;
using Infra.Context;
using Infra.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Base
    {
        private readonly ManagerContext _context;
        public BaseRepository(ManagerContext context)
        {
            _context = context;
        }
        public virtual async Task<T> Create(T obj)
        {
            _context.Add(obj); 

            await _context.SaveChangesAsync(); 

            return obj; 
        }

        public virtual async Task<T> Updade(T obj)
        {
            _context.Entry(obj).State = EntityState.Modified; 
            
            await _context.SaveChangesAsync();

            return obj;
        }

        public virtual async Task Remove(long id)
        {
            var obj = await GetById(id); 
            
            if(obj != null) 
            {
                _context.Remove(obj);
                await _context.SaveChangesAsync();             
            }
        }

        public virtual async Task<T> GetById(long id)
        {
            var obj = await _context.Set<T>()
                                    .AsNoTracking() 
                                    .Where(x => x.Id == id)
                                    .ToListAsync();

            return obj.FirstOrDefault();
        }

        public virtual async Task<List<T>> GetAll()
        {
            return await _context.Set<T>()
                                 .AsNoTracking()
                                 .ToListAsync();
        }
    }
}