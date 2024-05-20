using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.CORE.DataAccess
{
    public interface IEntityRepository<T> where T :class, new()
    {
        Task<T> AddAsync(T entity);
        Task<List<T>> AddAllAsync(List<T> entityList);
        Task<T> UpdateAsync(T entity);
        Task<List<T>> UpdateAllAsync(List<T> entityList);
        Task DeleteAsync(T entity);
        Task DeleteAllAsync(List<T> entityList);
        Task<T> GetAsync(Expression<Func<T, bool>> fiter);
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> filter = null);
        T Add(T entity);
        T Update(T entity);
        void Delete(T entity);
        T Get(Expression<Func<T, bool>> filter);
        IList<T> GetList(Expression<Func<T, bool>> filter = null);
        IQueryable<T> GetAll_I(Expression<Func<T, bool>> filter = null);
        IQueryable<T> GetAll_I_Params(Expression<Func<T, bool>> filter = null, string[] include = null);
        bool Exist(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
        IEnumerable<T> GetMany(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
        IQueryable<T> FindByCodition(Expression<Func<T, bool>> expression);


    }
}
