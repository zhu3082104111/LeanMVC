/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：IQueryableExtension.cs
// 文件功能描述：对IQueryable进行扩展
// 
// 创建标识：20131125 杜兴军
//
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/

using System;
using System.Linq;
using System.Reflection;
using Util;

namespace Extensions
{
    /// <summary>
    /// 对IQueryable的扩展
    /// </summary>
    public static class IQueryableExtension
    {
        /// <summary>
        /// 根据查询条件筛选数据
        /// </summary>
        /// <typeparam name="T">待筛选的数据表对应的类(EntityModel)</typeparam>
        /// <param name="source">待筛选的数据表</param>
        /// <param name="search">查询条件类</param>
        /// <returns>筛选后的IQueryable数据</returns>
        public static IQueryable<T> FilterBySearch<T>(this IQueryable<T> source,Object search )
        {
            PropertyInfo[] properties = search.GetType().GetProperties();
            var query = source;
            foreach (var property in properties)
            {
                Type propertype = property.PropertyType;

                object value = property.GetValue(search);

                if (value!=null)
                {
                    var veAttribute = property.GetCustomAttribute<EntityPropertyAttribute>();
                    string column = "";
                    string veOperator = "";
                    if (veAttribute == null || (veAttribute.ClassType!=null)&& !veAttribute.ClassType.Equals(typeof(T)))
                    {
                        continue;
                    }
                    column = veAttribute.Column;
                    if (typeof (T).GetProperty(column) == null)//检查当前实体model有无此字段，无则跳出
                    {
                        continue;
                    }
                    veOperator = veAttribute.VeOperator;
                    if (propertype.Equals(typeof(String)))//字符串类型默认为CONTAINS
                    {
                        if (veOperator == PropertyOperator.NULL)
                        {
                            veOperator = PropertyOperator.CONTAINS;
                        }
                        query = query.Where(column + veOperator, value.ToString());
                        continue;
                    }
                    if (propertype.Equals(typeof(DateTime?)))//可空日期类型默认为EQUAL
                    {
                        if (veOperator == PropertyOperator.LE)
                        {
                            DateTime d = ((DateTime)value).AddDays(1.0);
                            query = query.Where(column + PropertyOperator.LT, d);
                        }
                        else
                        {
                            if (veOperator == PropertyOperator.NULL)
                            {
                                veOperator = PropertyOperator.EQUAL;
                            }
                            query = query.Where(column + veOperator, (DateTime)value);
                        }
                        continue;
                    }
                    if (propertype.Equals(typeof(DateTime)))//非空日期类型默认为EQUAL
                    {
                        try
                        {
                            if (!((DateTime)value).Equals(OperatorConstant.INIT_DATETIME))//空判断
                            {

                                if (veOperator == PropertyOperator.LE)
                                {
                                    DateTime d = ((DateTime)value).AddDays(1.0);
                                    query = query.Where(column + PropertyOperator.LT, d);
                                }
                                else {
                                    if (veOperator == PropertyOperator.NULL)
                                    {
                                        veOperator = PropertyOperator.EQUAL;
                                    }
                                    query = query.Where(column + veOperator, (DateTime)value);
                                }

                            }
                        }
                        catch (Exception e)
                        {
                        }
                        continue;
                    }
                    if (propertype.Equals(typeof(decimal)))//数字类型默认为EQUAL
                    {
                        if (veOperator == PropertyOperator.NULL)
                        {
                            veOperator = PropertyOperator.EQUAL;
                        }
                        query = query.Where(column + veOperator, (decimal)value);
                    } 
                }    
            }
            return query;
        }   
    }


    /// <summary>
    /// 转换viewModel与entityModel的属性类
    /// 默认：数字和日期为相等(EQUAL),字符串为包含(CONTAINS)
    /// </summary>
    public class EntityPropertyAttribute : Attribute
    {
        public string Column { get; set; }//对应的entityModel的字段名
        public string VeOperator { get; set; }//运算符
        public Type ClassType { get; set; }

        public EntityPropertyAttribute(string column,string propertyOperator=PropertyOperator.NULL,Type classType=null)
        {
            this.Column = column;
            this.VeOperator = propertyOperator;
            this.ClassType = classType;
        }
    }


    /// <summary>
    /// viewModel与entityModel之间的运算符(等于EQUAL、大于GT、大于等于GE、小于LT、小于等于LE及包含CONTAINS)
    /// </summary>
    public struct PropertyOperator
    {
        /// <summary>
        /// 相等
        /// </summary>
        public const string EQUAL = " == @0";//entityModel属性值 等于 viewModel属性值

        /// <summary>
        /// 大于
        /// </summary>
        public const string GT = " > @0";//entityModel属性值 大于 viewModel属性值

        /// <summary>
        /// 大于等于
        /// </summary>
        public const string GE = " >= @0";//entityModel属性值 大于等于 viewModel属性值

        /// <summary>
        /// 小于
        /// </summary>
        public const string LT = " < @0";//entityModel属性值 小于 viewModel属性值

        /// <summary>
        /// 小于等于
        /// </summary>
        public const string LE = " <= @0";//entityModel属性值 小于等于 viewModel属性值

        /// <summary>
        /// 包含
        /// </summary>
        public const string CONTAINS = ".Contains(@0)";//entityModel属性值 包含 viewModel属性值

        /// <summary>
        /// NULL值
        /// </summary>
        public const string NULL = null;
    }


    public class OperatorConstant
    {
        
        
        /// <summary>
        /// 时间字段初始化值（0001/1/1 00:00:00），只读
        /// </summary>
        public static readonly DateTime INIT_DATETIME = Convert.ToDateTime("0001/1/1 00:00:00");
    }
}
