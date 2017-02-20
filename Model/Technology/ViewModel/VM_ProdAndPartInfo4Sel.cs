/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：VM_ProdAndPartInfo4Sel.cs
// 文件功能描述：
//          产品零件信息的视图Model（Popup子查询画面专用）
//      
// 修改履历：2013/12/16 宋彬磊 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 产品零件信息的视图Model（Popup子查询画面专用）
    /// </summary>
    public class VM_ProdAndPartInfo4Sel
    {
        // 产品零件ID
        public string Id { get; set; }

        // 产品零件略称
        public string Abbrev { get; set; }

        // 产品零件名称
        public string Name { get; set; }
    }
}
