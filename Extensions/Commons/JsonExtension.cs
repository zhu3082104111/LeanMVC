using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Extensions
{
    public static class JsonExtension
    {
        #region 用途:将数据转为json,适合一切Object的调用; 用法:object.ToJson(); 说明:兼容ie及其他不支持json的浏览器
        /// <summary>
        /// 转成json数据:将源数据包装成{rows:data,total:total}格式
        /// </summary>
        /// <param name="data"></param>
        /// <param name="total">总数</param>
        /// <returns></returns>
        public static JsonResult ToJson(this Object data,int total) 
        {
            JsonResult jr = new JsonResult();
            jr.Data = new { 
                rows=data,
                total=total
            };
            jr.ContentType = "text/html";
            return jr;
        }

        /// <summary>
        /// 转成json数据:将源数据直接包含在result中
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static JsonResult ToJson(this Object data) 
        {
            JsonResult jr = new JsonResult();
            jr.Data = new { 
                result=data
            };
            jr.ContentType = "text/html";
            return jr;
        }
        
        #endregion

    }
}
