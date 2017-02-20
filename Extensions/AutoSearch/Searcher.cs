using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    
    /// <summary>
    /// 自动搜索器
    /// </summary>
    public class Searcher
    {
        public string Type { get; set; }//类型
        public string Keyword { get; set; }//关键词
        public string Id { get; set; }//返回的主键
        public string Name { get; set; }//返回的主机对应的名称
    }
}
