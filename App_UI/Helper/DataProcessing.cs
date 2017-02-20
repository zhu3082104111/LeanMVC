using System;
using System.Linq;
using Extensions;
using System.Collections.Specialized;
using Util;
namespace  App_UI.Helper
{
    public static class DataProcessing
    {
        /// <summary>
        /// 根据传入参数 进行搜索 排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public static IQueryable<T> Processing<T>(this IQueryable<T> model, NameValueCollection queryString)
        {
            var propertyInfo = model.ElementType.GetProperties();

            //搜索
            foreach (string item in queryString)
            {
                if (!item.StartsWith("Search_") || string.IsNullOrEmpty(queryString[item]) || queryString[item] == ",")
                    continue;

                var proname = item.Replace("Search_", "");
                switch (propertyInfo.Single(a => a.Name.Equals(proname)).GetMethod.ReturnType.Name)
                {
                    case "Boolean":
                        model = model.Where(proname + " = @0", Boolean.Parse(queryString[item]));
                        break;
                    case "Int32":
                        var int32 = queryString[item];
                        if (!string.IsNullOrEmpty(int32.Split(',')[0]))
                        {
                            model = model.Where(proname + " >= @0 ", int.Parse(int32.Split(',')[0]));
                        }
                        if (!string.IsNullOrEmpty(int32.Split(',')[1]))
                        {
                            model = model.Where(proname + " <= @0 ", int.Parse(int32.Split(',')[1]));
                        }
                        break;
                    case "Decimal":
                        var val = queryString[item];

                        if (!string.IsNullOrEmpty(val.Split(',')[0]))
                        {
                            model = model.Where(proname + " >= @0 ", decimal.Parse(val.Split(',')[0]));
                        }
                        if (!string.IsNullOrEmpty(val.Split(',')[1]))
                        {
                            model = model.Where(proname + " <= @0 ", decimal.Parse(val.Split(',')[1]));
                        }
                        break;
                    case "DateTime":
                        var dateTime = queryString[item];
                        if (!string.IsNullOrEmpty(dateTime.Split(',')[0]))
                        {
                            model = model.Where(proname + " >= @0 ", DateTime.Parse(dateTime.Split(',')[0]));
                        }
                        if (!string.IsNullOrEmpty(dateTime.Split(',')[1]))
                        {
                            model = model.Where(proname + " <= @0 ", DateTime.Parse(dateTime.Split(',')[1]));
                        }
                        break;
                    default:
                        model = model.Where(proname + ".Contains(@0)", queryString[item]);
                        break;
                }
            }

            //排序
            if (!string.IsNullOrEmpty(queryString["ordering"]))
            {
                model = model.OrderBy(queryString["ordering"], null);
            }

            return model;
        }
    }
}