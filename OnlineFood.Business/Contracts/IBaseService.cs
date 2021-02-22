using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineFood.Business.Contracts
{
    public interface IBaseService<TModel> where TModel : class
    {
        IQueryable<TModel> GetAll(Expression<Func<TModel, bool>> predicate = null, bool asNoTracking = true);
        IQueryable<TModel> GetQuery();
        TModel Get(Expression<Func<TModel, bool>> predicate, bool asNoTracking = false);
        Task<TModel> GetAsync(Expression<Func<TModel, bool>> predicate, bool asNoTracking = false);
        IBaseService<TModel> AllInclude(params Expression<Func<TModel, object>>[] includes);
        void Add(TModel entity);
        int Delete(Expression<Func<TModel, bool>> where);
        Task<int> DeleteAsync(Expression<Func<TModel, bool>> where);
        void Delete(TModel entity);
        void Attach(TModel entity);
        //void BulkUpdate(List<TModel> entities);
        //void BulkInsert(List<TModel> entities);
        //void BulkUpdateAsync(List<TModel> entities);
        //void BulkInsertAsync(List<TModel> entities);
    }
}
