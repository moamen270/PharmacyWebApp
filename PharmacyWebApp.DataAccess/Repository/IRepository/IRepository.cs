using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyWebApp.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
        Task<T> GetAsyce(int id);
        IEnumerable<T> GetAll( string[]? include = null);
        Task<IEnumerable<T>> GetAllAsync(string[]? include = null);
        T GetFirstOrDefault(Expression<Func<T,bool>>? filter = null, string[]? include = null);
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? filter=null, string[]? include = null);
        T Add(T entity);
        Task<T> AddAsync(T entity);
        IEnumerable<T> AddRange(IEnumerable<T> entities);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        T Update(T entity);      
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        int Count();
        int Count(Expression<Func<T, bool>> filter);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> filter);


    }
}
