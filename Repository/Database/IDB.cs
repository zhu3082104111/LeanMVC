using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Repository.database
{
    public interface IDB
    {

        void SetModified<T>(T item) where T : class;

        void SetEntityState<T>(T item, EntityState state) where T : class;

        void ApplyCurrentValues<TEntity>(TEntity original, TEntity current)
           where TEntity : class;

        void BeginTransaction();

        void EndTransaction();

        /// <summary>
        /// 强制提交，如果失败则刷新数据，再次提交
        /// </summary>
        ///<remarks>
        ///
        ///</remarks>
        void CommitAndRefreshChanges();



        void RollbackChanges();




        /// <summary>
        /// 根据传入条件获取满足条件的所有实例
        /// </summary>
        /// <param name="condition">lambda表达式</param>
        /// <returns type="IQueryable">T类型对象的实例集合</returns>
        IQueryable<T> GetList<T>(Expression<Func<T, bool>> condition) where T : class;
        /// <summary>
        /// 添加，默认使用事务,如果当前不在事务中，则不使用事务
        /// </summary>
        /// <param name="entity">需要添加的对象</param>
        /// <returns type="bool">表示是否成功</returns>
        bool Add<T>(T entity) where T : class;


        /// <summary>
        /// 修改，默认使用事务,如果当前不在事务中，则不使用事务
        /// </summary>
        /// <param name="entity">需要修改的对象</param>
        /// <returns type="bool">表示是否成功</returns>
        bool Update<T>(T entity) where T : class;

        /// <summary>
        /// 修改，默认使用事务,如果当前不在事务中，则不使用事务
        /// 修改对象的指定列
        /// </summary>
        /// <param name="entity">需要修改的对象</param>
        /// <param name="columns">需要修改的列</param>
        /// <returns type="bool">表示是否成功</returns>
        bool Update<T>(T entity, string[] columns) where T : class;

        /// <summary>
        /// 删除，默认使用事务,如果当前不在事务中，则不使用事务
        /// </summary>
        /// <param name="entity">需要删除的对象</param>
        /// <returns type="bool">表示是否成功</returns>
        bool Delete<T>(T entity) where T : class;

        /// <summary>
        /// 批量删除，默认使用事务,如果当前不在事务中，则不使用事务
        /// </summary>
        /// <param name="entity">批量需要删除的条件</param>
        /// <returns type="bool">表示是否成功</returns>
        bool BatchDelete<T>(Expression<Func<T, bool>> criteria) where T : class;
    }
}
