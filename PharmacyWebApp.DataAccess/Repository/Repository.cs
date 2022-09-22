using Microsoft.EntityFrameworkCore;
using PharmacyWebApp.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyWebApp.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private ApplicationDbContext _context;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }
        public T Add(T entity)
        {
            dbSet.Add(entity);
            return entity;
        }

        public IEnumerable<T> AddRange(IEnumerable<T> entities)
        {
            _context.AddRange(entities);
            return entities;
        }

        public int Count()
        {
            return dbSet.Count();
        }

        public int Count(System.Linq.Expressions.Expression<Func<T, bool>> filter)
        {
            return dbSet.Count(filter);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public T GetFirstOrDefault(System.Linq.Expressions.Expression<Func<T, bool>>? filter=null)
        {   
            if (filter == null)
            {
                return dbSet.FirstOrDefault();
            }

            return dbSet.FirstOrDefault(filter);
        }

        public T Update(T entity)
        {
            _context.Update(entity);
            return entity;
        }
    }
}
