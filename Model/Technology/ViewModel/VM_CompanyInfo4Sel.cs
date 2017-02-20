/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：VM_CompanyInfo4Sel.cs
// 文件功能描述：
//          供货商信息的视图Model（Popup子查询画面专用）
//      
// 修改履历：2013/12/9 宋彬磊 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 供货商信息子查询画面的查询条件的视图Model（Popup子查询画面专用）
    /// </summary>
    public class VM_CompSearchCondition
    {
        // 供货商种类
        public string CompType { get; set; }

        // 供货商ID
        public string CompID { get; set; }

        // 供货商名称
        public string CompName { get; set; }

        // 供货商可提供的产品零件ID
        public string PdtID { get; set; }
    }

    /// <summary>
    /// 供货商信息的视图Model（Popup子查询画面专用）
    /// </summary>
    public class VM_CompanyInfo4Sel
    {
        // 供货商ID
        public string CompID { get; set; }

        // 供货商名称
        public string CompName { get; set; }
    }
}
