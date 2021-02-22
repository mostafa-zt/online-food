using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineFood.Infrastructure.DataAccess
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// متدی برای استفاده از الگوی مخزن توکار 
        /// EF
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیت</typeparam>
        /// <returns>IDbSet از موجودیت</returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        /// <summary>
        /// متد ذخیره سازی
        /// </summary>
        /// <returns></returns>
        int SaveAllChanges(bool updateCommonFields = true);
        /// <summary>
        /// متد ذخیره سازی به صورت ناهمزمان
        /// </summary>
        /// <returns></returns>
        Task<int> SaveAllChangesAsync(bool updateCommonFields = true);
        /// <summary>
        /// برای نشانه گذاری یک آبجکت که ویرایش شده است
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیت</typeparam>
        /// <param name="entity">آبجکت ارسالی</param>
        void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class;
        /// <summary>
        /// برای نشانه گذاری یک آبجکت که از کانتکس خارج  شده است
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیت</typeparam>
        /// <param name="entity">آبجکت ارسالی</param>
        void MarkAsDetached<TEntity>(TEntity entity) where TEntity : class;
        /// <summary>
        /// برای نشانه گذاری یک آبجکت که حذف شده است
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیت</typeparam>
        /// <param name="entity">آبجکت ارسالی</param>
        void MarkAsDeleted<TEntity>(TEntity entity) where TEntity : class;
        /// <summary>
        /// برای نشانه گذاری یک آبجکت که درج شده است
        /// </summary>
        /// <typeparam name="TEntity">نوع موجودیت</typeparam>
        /// <param name="entity">آبجکت ارسالی</param>
        void MarkAsAdded<TEntity>(TEntity entity) where TEntity : class;
        ///// <summary>
        ///// برای ویرایش ارتباط های چند به چند استفاده میشود
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="entity"></param>
        ///// <param name="mapping"></param>
        ///// <returns></returns>
        //T Update<T>(T entity, Expression<Func<RefactorThis.GraphDiff.IUpdateConfiguration<T>, object>> mapping) where T : class, new();
        /// <summary>
        /// برای درج لیستی از موجودیت ها استفاده میشود
        /// </summary>
        /// <typeparam name="TEntity">نوع مدل</typeparam>
        /// <param name="entities">لیستی از مدل مورد نظر</param>
        void AddThisRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
    }
}
