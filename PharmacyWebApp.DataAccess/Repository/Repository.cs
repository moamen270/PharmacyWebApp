using Microsoft.EntityFrameworkCore;
using PharmacyWebApp.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyWebApp.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }
        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            _context.AddRange(entities);
            return entities;
        }
        
        public int Count()
        {
            return _context.Set<T>().Count();
        }

        public int Count(System.Linq.Expressions.Expression<Func<T, bool>> filter)
        {
            return _context.Set<T>().Count(filter);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public T Get(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public T GetFirstOrDefault(System.Linq.Expressions.Expression<Func<T, bool>>? filter=null)
        {   
            if (filter == null)
            {
                return _context.Set<T>().FirstOrDefault();
            }

            return _context.Set<T>().FirstOrDefault(filter);
        }

        public T Update(T entity)
        {
            _context.Update(entity);
            return entity;
        }

        public async Task<T> GetAsyce(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? filter = null)
        {

            if (filter == null)
            {
                return await _context.Set<T>().FirstOrDefaultAsync();
            }

            return await _context.Set<T>().FirstOrDefaultAsync(filter);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            return entities;
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> filter)
        {
            return await _context.Set<T>().CountAsync(filter);
        }
    }
}
