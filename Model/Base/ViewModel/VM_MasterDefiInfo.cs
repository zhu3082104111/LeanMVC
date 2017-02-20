using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Base
{
    public class VM_MasterInfoForSelect 
    {
        public VM_MasterInfoForSelect() 
        {
            selected = false;
        }
        /// <summary>
        /// 属性代码
        /// </summary>
        public string AttrCd { get; set; }

        /// <summary>
        /// 属性名称
        /// </summary>
        public string AttrValue { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool selected;
    
    }
}
