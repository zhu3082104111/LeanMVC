using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Util;

namespace Extensions
{
    public static class PagingExtension
    {
        /// <summary>
        /// 分页(泛型)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="keyName">键名称(分页前需先按某一字段排序)</param>
        /// <param name="paging">分页参数类</param>
        /// <returns></returns>
        public static IEnumerable<T> ToPageList<T>(this IQueryable<T> source, string keyName, Paging paging)
        {
           
            int skip = (paging.page - 1) * paging.rows;//跳过的记录数
            if (!String.IsNullOrEmpty(paging.sort))//排序
            {
                string[] sortNames = paging.sort.Split(',');//字段
                string[] sortOrders =String.IsNullOrEmpty(paging.order)?(new string[]{"asc"} ): paging.order.Split(',');//规则（asc,desc）
                string orderby = "";//暂存排序规则
                if (sortNames.Length > sortOrders.Length)//规则与字段数目不符
                {
                    List<string> sortOrderList = sortOrders.ToList();
                    for (int i = sortOrders.Length, len = sortNames.Length; i < len; i++)//为默认字段创建规则asc
                    {
                        sortOrderList.Add("asc");
                    }
                    sortOrders = sortOrderList.ToArray();
                }
                for (int i = 0, len = sortNames.Length; i < len; i++)
                {
                    orderby += (sortNames[i] + " " + sortOrders[i] + ",");
                }
                orderby = orderby.Substring(0, orderby.Length-1);//去掉最后一个 逗号
                source = source.OrderBy(orderby);//排序
            }
            else
            {
                source = source.OrderBy(keyName);//后备排序，防止使用Skip出错
            }
            source = source.Skip(skip).Take(paging.rows);
            return source.AsEnumerable();
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="source"></param>
        /// <param name="keyName">键名称(分页前需先按某一字段排序)</param>
        /// <param name="paging">分页参数类</param>
        /// <returns></returns>
        public static IEnumerable ToPageList(this IQueryable<Object> source, string keyName, Paging paging)
        {
            return source.ToPageList<Object>(keyName, paging);
        }
    }
}
