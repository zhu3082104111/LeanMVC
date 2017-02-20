using Extensions;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRepository<T> : IDisposable where T:Entity
    {
        /// <summary>
        /// 获取 T 类型的DbSet
        /// </summary>
        /// <returns>T类型的IDbSet集合</returns>
        //IDbSet<T> GetSet();

        /// <summary>
        /// 根据主键Guid获取 T 对象
        /// </summary>
        /// <param name="id" type="Guid"></param>
        /// <returns>T类型对象的实例</returns>
        //T Get(Guid id);


        /// <summary>
        /// 根据传入对象查找是否存在这个对象，如果存在则返回这个对象
        /// 代东泽 20131220
        /// </summary>
        /// <param name="entityHasKey"></param>
        /// <returns></returns>
        T Find(T entityHasKey);



        /// <summary>
        /// 根据参数传入条件获取满足条件的第一个实例
        /// </summary>
        /// <param name="condition">参数是一个lambda表达式</param>
        /// <returns>T类型对象的实例</returns>
        T First(Expression<Func<T, bool>> condition);

        /// <summary>
        /// 根据传入条件获取满足条件的第一个实例，不存在时返回空
        /// C:梁龙飞
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        T FirstOrDefault(Expression<Func<T, bool>> condition);

        /// <summary>
        /// 根据int类型主键id获取 T 对象
        /// </summary>
        /// <param name="id" type="int"></param>
        /// <returns>T类型对象的实例</returns>
        //T GetEntityById(int id);

        /// <summary>
        /// 根据string类型主键id获取 T 对象
        /// </summary>
        /// <param name="id" type="string"></param>
        /// <returns>T类型对象的实例</returns>
        T GetEntityById(string id);

        /// <summary>
        /// 根据传入条件获取满足条件的唯一实例
        /// </summary>
        /// <param name="condition">lambda表达式</param>
        /// <returns>T类型对象的实例</returns>
        T Get(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 根据传入条件获取满足条件的所有实例
        /// </summary>
        /// <param name="condition">lambda表达式</param>
        /// <returns type="IQueryable">T类型对象的实例集合</returns>
        IEnumerable<T> GetListByCondition(Expression<Func<T, bool>> condition);

        /// <summary>
        /// 根据查询条件和排序依据获取满足条件的所有实例排序后的结果
        /// </summary>
        /// <param name="condition">lambda表达式，检索的条件</param>
        /// <param name="orderBy">lambda表达式，排序的条件</param>
        /// <returns type="IQueryable">T类型对象的实例集合</returns>
        IEnumerable<T> GetListByConditionWithOrderBy<TKey>(Expression<Func<T, bool>> condition, Expression<Func<T, TKey>> orderBy);

        /// <summary>
        /// 根据排序条件获取排序后的所有实例
        /// </summary>
        /// <param name="orderBy">lambda表达式，排序的条件</param>
        /// <returns type="IQueryable">T类型对象的实例集合</returns>
        IEnumerable<T> GetListWithOrderBy<TKey>(Expression<Func<T, TKey>> orderBy);

        #region Old-Version
        /// <summary>
        /// 获取T类型所有实例
        /// </summary>
        /// <returns type="IQueryable">T类型对象的实例集合</returns>
        //IEnumerable<T> GetList();

        /// <summary>
        /// 获取T类型排序，分页后实例集合
        /// </summary>
        /// <returns type="IEnumerable">T类型对象的实例集合</returns>
        //IEnumerable<T> GetPageList<TOrderBy>(Expression<Func<T, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending);

        /// <summary>
        /// 获取满足条件的 T类型排序，分页后实例集合
        /// </summary>
        /// <returns type="IEnumerable">T类型对象的实例集合</returns>
        //IEnumerable<T> GetPageList<TOrderBy>(Expression<Func<T, bool>> where, Expression<Func<T, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending);

        #endregion

        /// <summary>
        /// 添加，默认使用事务,如果当前不在事务中，则不使用事务
        /// </summary>
        /// <param name="entity">需要添加的对象</param>
        /// <returns type="bool">表示是否成功</returns>
        bool Add(T entity);

       
        /// <summary>
        /// 修改，默认使用事务,如果当前不在事务中，则不使用事务
        /// </summary>
        /// <param name="entity">需要修改的对象</param>
        /// <returns type="bool">表示是否成功</returns>
        bool Update(T entity);

        /// <summary>
        /// 修改，默认使用事务,如果当前不在事务中，则不使用事务
        /// 修改对象的指定列
        /// </summary>
        /// <param name="entity">需要修改的对象</param>
        /// <param name="columns">需要修改的列</param>
        /// <returns type="bool">表示是否成功</returns>
        bool Update(T entity, string[] columns);


        /// <summary>
        /// 修改对象的不为空的列，把数据库中的值修改为对象中的值，必须要有主键
        /// </summary>
        /// <param name="entity">要修改的对象</param>
        /// <returns></returns>
        bool UpdateNotNullColumn(T entity);

        /// <summary>
        /// 删除，默认使用事务,如果当前不在事务中，则不使用事务
        /// </summary>
        /// <param name="entity">需要删除的对象</param>
        /// <returns type="bool">表示是否成功</returns>
        bool Delete(T entity);

        /// <summary>
        /// 批量删除，默认使用事务,如果当前不在事务中，则不使用事务
        /// </summary>
        /// <param name="entity">批量需要删除的条件</param>
        /// <returns type="bool">表示是否成功</returns>
        bool BatchDelete(Expression<Func<T, bool>> criteria);

        #region Old-version
        /// <summary>
        /// 将元数据分页
        /// </summary>
        /// <param name="source">元数据</param>
        /// <param name="_keyName">分页前的排序字段</param>
        /// <param name="paging">分页属性</param>
        /// <param name="total">存放元数据的总数</param>
        /// <returns></returns>
        //IQueryable<T> ToPageList(IQueryable<T> source, string _keyName, Paging paging, out int total);


        /// <summary>
        /// 将元数据分页
        /// </summary>
        /// <param name="source">元数据</param>
        /// <param name="_keyName">分页前的排序字段</param>
        /// <param name="paging">分页属性</param>
        /// <param name="total">存放元数据的总数</param>
        /// <returns></returns>
        //IQueryable<T> ToPageList(IEnumerable<T> source, string _keyName, Paging paging, out int total);
        #endregion

        /// <summary>
        /// 获取自动检索数据。
        /// 根据searcher对象自动检索，由需要的实体Repository自行实现
        /// </summary>
        /// <param name="searcher">条件对象</param>
        /// <returns>自动检索数据</returns>
        IEnumerable<Searcher> GetAutoSearchData(Searcher searcher);

       


    }
}
