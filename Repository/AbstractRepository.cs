using Model;
using Repository.database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Extensions;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;

namespace Repository
{
    public abstract class AbstractRepository<C,T> : IRepository<T>, IDisposable where C :DbContext,IDB,ISql, new() where T :Entity
    {
        private C db;

        public AbstractRepository() {
            this.db = IOCHelper.ResolverClass<C>();
        }

        public AbstractRepository(C db) { 
            this.db=db;
        }

        /*public  C  Db
        {
            get
            {
                return db;
            }
        }*/

        /// <summary>
        /// 根据传入对象查找是否存在这个对象，如果存在则返回这个对象
        /// 代东泽 20131205
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="entityHasKey"></param>
        /// <returns></returns>
        protected E Find<E>(E entityHasKey) where E:Entity
        {
            if (db.Set<E>() == null)
            {
                return null;
            }
            IList<Object> list = new List<Object>();
            PropertyInfo[] properties = entityHasKey.GetType().GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                var property = properties[i];
                KeyAttribute key = property.GetCustomAttribute<KeyAttribute>();
                if (key != null)
                {
                    object value = property.GetValue(entityHasKey);
                    if (value != null)
                    {
                        //Type type = property.PropertyType;
                        list.Add(value);
                    }
                    else
                    {
                        throw new NullValueKeyException(property.Name); //MissingPrimaryKeyException();
                    }
                }
            }
            if (list.Count==0) 
            {
                throw new MissingPrimaryKeyException();
            }
            return db.Set<E>().Find(list.ToArray());
        } 

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected IDbSet<T> GetSet() 
        {
            return db.Set<T>(); 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected IDbSet<E> GetSet<E>() where E : class
        {
            return db.Set<E>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual IQueryable<T> GetList()
        {
            return db.Set<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual IQueryable<T> GetList(Expression<Func<T, bool>> condition)
        {
           return db.Set<T>().Where(condition);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual IQueryable<T> GetList<TKey>(Expression<Func<T, bool>> condition, Expression<Func<T, TKey>> orderBy)
        {
           return db.Set<T>().Where(condition).OrderBy(orderBy);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual IQueryable<T> GetList<TKey>(Expression<Func<T, TKey>> orderBy)
        {
           return db.Set<T>().OrderBy(orderBy);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <returns></returns>
        protected virtual IQueryable<E> GetList<E>()where E:class
        {
           return db.Set<E>();
        }

        /// <summary>
        /// 返回未删除、有效的列表：无结果时返回空
        /// 为方便调试，目前采用黑名单模式：非有效的状态默认为有效，非删除的状态默认为未删除
        /// C:梁龙飞 
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <returns></returns>
        protected virtual IQueryable<E> GetAvailableList<E>() where E : Entity
        {
            IQueryable<E> finaDatas=db.Set<E>().Where(a =>
                ((a.EffeFlag != Constant.CONST_FIELD.UN_EFFECTIVE || a.EffeFlag == null) &&
                (a.DelFlag != Constant.CONST_FIELD.DELETED || a.DelFlag == null)));
            if (finaDatas.Count()==0)
            {
                return null;
            }
            return finaDatas;
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        protected virtual E GetEntityById<E>(string id) where E : class
        {

            return db.Set<E>().Find(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        protected E First<E>(Expression<Func<E, bool>> condition) where E : class
        {
            try
            {
                return db.Set<E>().First(condition);
            }
            catch (InvalidOperationException e)
            {
                if (db.Set<E>() != null)
                {
                    return null;
                }
                else 
                {
                    throw e;
                }
                
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual bool Add<E>(E entity) where E : class
        {       
            if (entity != null)
            {
                db.Set<E>().Add(entity);

                return db.SaveChanges() > 0;

            }
            return false;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual bool Update<E>(E entity) where E : class
        {
                db.Set<E>().Attach(entity);

                if (entity != (E)null)
                {
                    db.SetModified<E>(entity);
                }

                return db.SaveChanges() > 0;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="entity"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        protected virtual bool Update<E>(E entity, string[] columns) where E : class
        {

                if (entity != (E)null)
                {
                    db.Set<E>().Attach(entity);
                    var stateEntry = db.Entry<E>(entity);

                    if (columns != null)
                    {
                        foreach (string s in columns)
                        {
                            stateEntry.Property(s).IsModified = true;
                        }
                    }
                }
                return db.SaveChanges() > 0;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="entity"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        protected virtual bool UpdateNotNullColumn<E>(E entity) where E : class
        {
            StringBuilder stb = new StringBuilder("update ");
            TableAttribute table= entity.GetType().GetCustomAttribute<TableAttribute>();
            stb.Append(table.Name+" set ");
            PropertyInfo[] properties = entity.GetType().GetProperties();
            StringBuilder keyBuild = new StringBuilder(" where ");
            bool keyBegin = false;
            bool columnBegin = false;
            for (int i = 0; i < properties.Length; i++)
            {
                var property = properties[i];
                object value = property.GetValue(entity);
                if (value != null)
                {
                    ColumnAttribute column = property.GetCustomAttribute<ColumnAttribute>();
                    KeyAttribute key = property.GetCustomAttribute<KeyAttribute>();
                    if (key != null)
                    {
                        Type type = property.PropertyType;
                        if (type.Equals(typeof(String)))
                        {
                            if (!keyBegin)
                            {
                                keyBegin = true;
                            }
                            else {
                                keyBuild.Append(" and ");
                            }
                            keyBuild.Append(column.Name);
                            keyBuild.Append("= '");
                            keyBuild.Append(value);
                            keyBuild.Append("' ");
                        }
                        else if (type.Equals(typeof(int)))
                        {
                            if (!keyBegin)
                            {
                                keyBegin = true;
                            }
                            else
                            {
                                keyBuild.Append(" and ");
                            }
                            keyBuild.Append(column.Name);
                            keyBuild.Append("= ");
                            keyBuild.Append(value);
                            keyBuild.Append(" ");
                        }
                        else if (type.Equals(typeof(DateTime)))
                        {
                            if (!keyBegin)
                            {
                                keyBegin = true;
                            }
                            else
                            {
                                keyBuild.Append(" and ");
                            }
                            keyBuild.Append(column.Name);
                            keyBuild.Append("= '");
                            keyBuild.Append(value);
                            keyBuild.Append("' ");

                        }
                        else if (type.Equals(typeof(Decimal)))
                        {
                            if (!keyBegin)
                            {
                                keyBegin = true;
                            }
                            else
                            {
                                keyBuild.Append(" and ");
                            }
                            keyBuild.Append(column.Name);
                            keyBuild.Append("= ");
                            keyBuild.Append(value);
                            keyBuild.Append(" ");
                        }
                        else if (type.Equals(typeof(Char)) && ((char)value).CompareTo('\0') != 0)
                        {
                            if (!keyBegin)
                            {
                                keyBegin = true;
                            }
                            else
                            {
                                keyBuild.Append(" and ");
                            }
                            keyBuild.Append(column.Name);
                            keyBuild.Append("= '");
                            keyBuild.Append(value);
                            keyBuild.Append("' ");
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        string colunName = column.Name;
                        if (column != null && !colunName.Equals("CRE_DT"))
                        {
                            Type type = property.PropertyType;
                            if (type.Equals(typeof(String)) && !value.Equals(""))
                            {
                                if (!columnBegin)
                                {
                                    columnBegin = true;
                                }
                                else {
                                    stb.Append(" , ");
                                }
                                stb.Append(colunName);
                                stb.Append("= '");
                                stb.Append(value);
                                stb.Append("' ");
                            }
                            else if (type.Equals(typeof(int)))
                            {

                                IntegerAttribute intAttr = property.GetCustomAttribute<IntegerAttribute>();
                                if (intAttr != null)
                                {
                                    if (((int)value) != 0 || (intAttr.IsZeroUpdate && ((int)value) == 0))
                                    {
                                        if (!columnBegin)
                                        {
                                            columnBegin = true;
                                        }
                                        else
                                        {
                                            stb.Append(" , ");
                                        }
                                        stb.Append(colunName);
                                        stb.Append("= ");
                                        stb.Append(value);
                                        stb.Append(" ");
                                    }
                                }
                                else {
                                    if (!columnBegin)
                                    {
                                        columnBegin = true;
                                    }
                                    else
                                    {
                                        stb.Append(" , ");
                                    }
                                    stb.Append(colunName);
                                    stb.Append("= ");
                                    stb.Append(value);
                                    stb.Append(" ");
                                }
                            }
                            else if (type.Equals(typeof(DateTime)))
                            {
                                if (((DateTime)value).CompareTo(Constant.CONST_FIELD.INIT_DATETIME) > 0)
                                {
                                    if (!columnBegin)
                                    {
                                        columnBegin = true;
                                    }
                                    else
                                    {
                                        stb.Append(" , ");
                                    }
                                    stb.Append(colunName);
                                    stb.Append("= '");
                                    stb.Append(value);
                                    stb.Append("' ");
                                }
                                else
                                {
                                    continue;
                                }

                            }
                            else if (type.Equals(typeof(DateTime?)))
                            {
                                if (((DateTime?)value)!=null)
                                {
                                    if (!columnBegin)
                                    {
                                        columnBegin = true;
                                    }
                                    else
                                    {
                                        stb.Append(" , ");
                                    }
                                    stb.Append(colunName);
                                    stb.Append("= '");
                                    stb.Append(value);
                                    stb.Append("' ");
                                }
                                else
                                {
                                    continue;
                                }

                            }
                            else if (type.Equals(typeof(Decimal)))
                            {
                                DecimalPrecisionAttribute decimalAttr = property.GetCustomAttribute<DecimalPrecisionAttribute>();
                                if (decimalAttr != null)
                                {
                                    if (((decimal)value) != 0M || (decimalAttr.IsZeroUpdate && ((decimal)value) == 0M))
                                    {
                                        if (!columnBegin)
                                        {
                                            columnBegin = true;
                                        }
                                        else
                                        {
                                            stb.Append(" , ");
                                        }
                                        stb.Append(colunName);
                                        stb.Append("= ");
                                        stb.Append(value);
                                        stb.Append(" ");
                                    }
                                }
                                else {
                                    continue;
                                }
                            }
                            else if (type.Equals(typeof(Char)) && ((char)value).CompareTo('\0') != 0)
                            {
                                if (!columnBegin)
                                {
                                    columnBegin = true;
                                }
                                else
                                {
                                    stb.Append(" , ");
                                }
                                stb.Append(colunName);
                                stb.Append("= '");
                                stb.Append(value);
                                stb.Append("' ");
                            }
                            else {
                                continue;
                            }
                           
                        }
                    }
                }
            }
            stb.Append(keyBuild.ToString());
            return db.Database.ExecuteSqlCommand(stb.Append(" ;").ToString(), new string []{}) > 0;
        }




        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Entity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual bool Delete<E>(E entity) where E : class
        {

            db.Set<E>().Attach(entity);
            db.SetEntityState<E>(entity, EntityState.Deleted);
            //db.Entry<T>(entity).State = EntityState.Deleted;
            return db.SaveChanges() > 0;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="criteria"></param>
        /// <returns></returns>
        protected virtual bool BatchDelete<E>(Expression<Func<E, bool>> criteria) where E : class
        {
            IEnumerable<E> records = db.Set<E>().Where(criteria);
            foreach (E record in records)
            {
                Delete(record);
            }
            return true;


        }



        /// <summary>
        /// 根据传入对象查找是否存在这个对象，如果存在则返回这个对象
        /// 代东泽 20131205
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="entityHasKey"></param>
        /// <returns></returns>
        public T Find(T entityHasKey) 
        {
            if (db.Set<T>() == null)
            {
                return null;
            }
            IList<Object> list = new List<Object>();
            PropertyInfo[] properties = entityHasKey.GetType().GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                var property = properties[i];
                KeyAttribute key = property.GetCustomAttribute<KeyAttribute>();
                if (key != null)
                {
                    object value = property.GetValue(entityHasKey);
                    if (value != null)
                    {
                        //Type type = property.PropertyType;
                        list.Add(value);
                    }
                    else
                    {
                        throw new NullValueKeyException(property.Name); //MissingPrimaryKeyException();
                    }
                }
            }
            if (list.Count == 0)
            {
                throw new MissingPrimaryKeyException();
            }
            return db.Set<T>().Find(list.ToArray());
        } 




        public virtual T GetEntityById(string id)
        {

            return db.Set<T>().Find(id);
        }

        public T First(Expression<Func<T, bool>> condition)
        {
            try
            {
                T t = db.Set<T>().First(condition);
                return t;
            }
            catch (InvalidOperationException e)
            {
                return null;
            }
            
        }

        public T FirstOrDefault(Expression<Func<T, bool>> condition)
        {
            try
            {
                T t = db.Set<T>().FirstOrDefault(condition);
                return t;
            }
            catch (InvalidOperationException e)
            {
                return null;
            }
        }

        public virtual T Get(Expression<Func<T, bool>> predicate) 
        {
            if (predicate != null)
            {
                return db.Set<T>().Where(predicate).SingleOrDefault();
            }
            else 
            {
                throw new ApplicationException("The param cant be null !");
            }
            
        }




        public  IEnumerable<T> GetListByCondition(Expression<Func<T, bool>> condition)
        {
            if (condition != null) 
            {
                return db.Set<T>().Where(condition);
            }
            throw new ApplicationException("The param cant be null !");
        }

        public  IEnumerable<T> GetListByConditionWithOrderBy<TKey>(Expression<Func<T, bool>> condition,
            Expression<Func<T, TKey>> orderBy) 
        {
            return db.Set<T>().Where(condition).OrderBy(orderBy);
        }

        public  IEnumerable<T> GetListWithOrderBy<TKey>(Expression<Func<T, TKey>> orderBy) 
        {
                return db.Set<T>().OrderBy(orderBy);
        }

        /*public virtual IEnumerable<T> GetList() 
        {
            try
            {

                return db.Set<T>();
            }
            catch (Exception ex)
            {
                //Log error
            }
            return null;
        }*/

        protected virtual void TrackEntity(T item)
        {
            if (item != null)
            {
                GetSet().Attach(item);
            }
        }


        public virtual bool Add(T entity)
        {

                if (entity != null)
                {
                    GetSet().Add(entity);
                
                    return db.SaveChanges() > 0;
                    
                }
                return false;
        }

      
        public virtual bool Update(T entity)
        {
                GetSet().Attach(entity);

                if (entity != (T)null)
                {
                    db.SetModified<T>(entity);
                    return db.SaveChanges() > 0;
                }
                return false;
               
        }

       

        public virtual bool Update(T entity,string[] columns)
        {

                if (entity != (T)null)
                {
                    GetSet().Attach(entity);
                    var stateEntry = db.Entry<T>(entity);

                    if (columns != null)
                    {
                        foreach (string s in columns)
                        {
                            stateEntry.Property(s).IsModified = true;
                        }
                    }
                }
                return db.SaveChanges() > 0;

        }

        public virtual bool UpdateNotNullColumn(T entity)
        {
            return this.UpdateNotNullColumn<T>(entity);
        }


        public virtual bool Delete(T entity) 
        {

                GetSet().Attach(entity);
                db.SetEntityState<T>(entity,EntityState.Deleted);
                //db.Entry<T>(entity).State = EntityState.Deleted;
                return db.SaveChanges() > 0;
        }


        public virtual bool BatchDelete(Expression<Func<T, bool>> criteria) 
        {
            IEnumerable<T> records = db.Set<T>().Where(criteria);
            foreach (T record in records)
            {
                Delete(record);
            }
            return true;
        }


        protected bool ExecuteStoreCommand(string cmdText, params object[] parameters)
        {
           return db.ExecuteCommand(cmdText, parameters) > 0;
        }

        protected IEnumerable<T> ExecuteQuery(string cmdText, params object[] parameters)
        {
           return db.ExecuteQuery<T>(cmdText, parameters);

        }

        protected IEnumerable<ViewModel> ExecuteQuery<ViewModel>(string cmdText, params object[] parameters)
        {

           return db.ExecuteQuery<ViewModel>(cmdText, parameters);

        }


        /*public IEnumerable<T> GetPageList<TOrderBy>(Expression<Func<T, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending) 
        {
            if (sortOrder == SortOrder.Ascending)
            {
                return GetSet().OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
            return GetSet().OrderByDescending(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public IEnumerable<T> GetPageList<TOrderBy>(Expression<Func<T, bool>> where, Expression<Func<T, TOrderBy>> orderBy, int pageIndex, int pageSize, SortOrder sortOrder = SortOrder.Ascending)
        {
            if (sortOrder == SortOrder.Ascending)
            {
                return GetList(where).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
            }
            return GetList(where).OrderByDescending(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        */


        /// <summary>
        /// <see cref="M:System.IDisposable.Dispose"/>
        /// </summary>
        public void Dispose()
        {

            //if (db != null)
                //db.Dispose();
        }

       /* public IQueryable<T> ToPageList(IQueryable<T> source, string _keyName, Paging paging, out int total)
        {
            int skip = (paging.page - 1) * paging.rows;
            IQueryable<T> list = source;
            total = source.Count();

            if (!String.IsNullOrEmpty(_keyName))
            {
                list = list.OrderBy(_keyName);
            }
            list = list.Skip(skip).Take(paging.rows);
            if (!String.IsNullOrEmpty(paging.sort))
            {
                string orderby = paging.sort + " ";
                if (String.IsNullOrEmpty(paging.sort))
                {
                    orderby += "asc";
                }
                else
                {
                    orderby += (paging.order);
                }
                list = list.OrderBy(orderby);
            }
            return list;
        }


        public IQueryable<T> ToPageList(IEnumerable<T> source, string _keyName, Paging paging, out int total)
        {
            IQueryable<T> list = source.AsQueryable<T>();
            return ToPageList(list, _keyName, paging, out total);
        }
        */

        public virtual IEnumerable<Searcher> GetAutoSearchData(Searcher searcher)
        {
            return null;
        }



    }
}
