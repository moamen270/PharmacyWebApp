using Microsoft.EntityFrameworkCore;
using DSCWebApp.DataAccess.Repository.IRepository;
using System.Linq.Expressions;

namespace DSCWebApp.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            //_db.ShoppingCarts.Include(u => u.Product).Include(u=>u.CoverType);
            this.dbSet = _context.Set<T>();
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

        public IEnumerable<T> GetAllByDeafult(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }

        public IEnumerable<T> GetAll(string[]? include = null)
        {
            IQueryable<T> result = _context.Set<T>();
            if (include != null)
                foreach (var item in include)
                {
                    result = result.Include(item);
                }
            return result.ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync(string[]? include = null)
        {
            IQueryable<T> result = _context.Set<T>().AsNoTracking();
            if (include != null)
                foreach (var item in include)
                {
                    result = result.Include(item);
                }
            return await result.ToListAsync();
        }

        public T GetFirstOrDefault(System.Linq.Expressions.Expression<Func<T, bool>>? filter = null, string[]? include = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (filter != null)
                query = query.Where(filter);
            else
                query.FirstOrDefault();
            if (include != null)
            {
                foreach (var item in include)
                {
                    query = query.Include(item);
                }
            }
            return query.FirstOrDefault();
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

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? filter = null, string[]? include = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (filter != null)
                query = query.Where(filter);
            if (include != null)
            {
                foreach (var item in include)
                {
                    query = query.Include(item);
                }
            }
            return await query.FirstOrDefaultAsync();
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

        public async Task<IEnumerable<T>> GetAllByFilterAsync(Expression<Func<T, bool>> filter, string[]? include = null)
        {
            var query = _context.Set<T>().Where(filter);
            if (include != null)
            {
                foreach (var item in include)
                {
                    query = query.Include(item);
                }
            }

            return await query.ToListAsync();
        }

        public T GetFirstOrDefaultForShopping(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = true)
        {
            if (tracked)
            {
                IQueryable<T> query = dbSet;

                query = query.Where(filter);
                if (includeProperties != null)
                {
                    foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProp);
                    }
                }
                return query.FirstOrDefault();
            }
            else
            {
                IQueryable<T> query = dbSet.AsNoTracking();

                query = query.Where(filter);
                if (includeProperties != null)
                {
                    foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProp);
                    }
                }
                return query.FirstOrDefault();
            }
        }
    }
}