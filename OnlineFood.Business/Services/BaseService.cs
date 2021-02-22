using OnlineFood.Business.Contracts;
using OnlineFood.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnlineFood.Business.Services
{
    public class BaseService<TModel> : IBaseService<TModel> where TModel : class
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private ApplicationDbContext _dbContext;
        private IQueryable<TModel> query;
        #endregion

        #region Ctor
        public BaseService(IUnitOfWork unitOfWork , ApplicationDbContext dbContext)
        {
            _unitOfWork = unitOfWork;
            query = _unitOfWork.Set<TModel>();
            _dbContext = dbContext;

        }
        #endregion

        #region GetAll
        public IQueryable<TModel> GetAll(Expression<Func<TModel, bool>> predicate = null, bool asNoTracking = true)
        {
            if (asNoTracking)
            {
                return predicate != null ? query.AsNoTracking().Where(predicate) : query.AsNoTracking();
            }
            else
            {
                return predicate != null ? query.Where(predicate) : query;
            }
        }
        #endregion

        #region Get
        public TModel Get(Expression<Func<TModel, bool>> predicate, bool asNoTracking = false)
        {
            return asNoTracking ? query.AsNoTracking().FirstOrDefault(predicate) : query.FirstOrDefault(predicate);
        }
        #endregion

        #region GetAsync
        public async Task<TModel> GetAsync(Expression<Func<TModel, bool>> predicate, bool asNoTracking = false)
        {
            return await (asNoTracking ? query.AsNoTracking().FirstOrDefaultAsync(predicate) : query.FirstOrDefaultAsync(predicate));
        }
        #endregion

        #region AllInclude
        public IBaseService<TModel> AllInclude(params Expression<Func<TModel, object>>[] includes)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return this;
        }
        #endregion

        #region AllInclude
        public IQueryable<TModel> GetQuery()
        {
            return query;
        }
        #endregion

        #region Add
        public void Add(TModel entity) => _unitOfWork.Set<TModel>().Add(entity);
        #endregion

        #region Delete Batch
        public virtual int Delete(Expression<Func<TModel, bool>> where) => _unitOfWork.Set<TModel>().AsNoTracking().Where(where).DeleteFromQuery();
        public virtual async Task<int> DeleteAsync(Expression<Func<TModel, bool>> where) => await _unitOfWork.Set<TModel>().AsNoTracking().Where(where).DeleteFromQueryAsync();
        #endregion

        #region Delete
        public void Delete(TModel entity)
        {
            _unitOfWork.Set<TModel>().Remove(entity);
        }
        #endregion

        #region Attach
        public void Attach(TModel entity)
        {
            _unitOfWork.Set<TModel>().Attach(entity);
        }
        #endregion

        //#region BulkUpdates
        //public void BulkUpdate(System.Collections.Generic.List<TModel> entities)
        //{
        //    _dbContext.BulkUpdate(entities);
        //}
        //public void BulkUpdateAsync(System.Collections.Generic.List<TModel> entities)
        //{
        //    _dbContext.BulkUpdateAsync(entities);
        //}
        //#endregion

        //#region BulkInsert
        //public void BulkInsert(System.Collections.Generic.List<TModel> entities)
        //{
        //    _dbContext.BulkInsert(entities);
        //}
        //public void BulkInsertAsync(System.Collections.Generic.List<TModel> entities)
        //{
        //    _dbContext.BulkInsertAsync(entities);
        //}
        //#endregion
    }
}
