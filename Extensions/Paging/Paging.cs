using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    /// <summary>
    /// 分页排序属性类
    /// </summary>
    public class Paging
    {
        public int page { get; set; }//页数
        public int rows { get; set; }//每页行数
        public string sort { get; set; }//排序字段
        public string order { get; set; }//排序规则
        public int total { get; set; }//满足条件的总记录数

        public Paging() 
        {
            page = 1;
            rows = 10;
        }
    }
}
