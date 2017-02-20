/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：VM_PurchaseOrderList.cs
// 文件功能描述：
//          产品外购单一览画面的视图Model
//      
// 修改履历：2013/10/28 刘云 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;

namespace Model
{
    /// <summary>
    /// 产品外购单一览画面的查询条件的视图Model类
    /// </summary>
    public class VM_PurchaseOrderListForSearch
    {
        // 客户订单号
        public string CustomerOrder { get; set; }

        // 外购单号
        [EntityProperty("OutOrderID", PropertyOperator.CONTAINS, typeof(MCOutSourceOrder))]
        public string OutOrderID { get; set; }

        // 编制时间 用于查询 初始时间
        [EntityProperty("EstablishDate", PropertyOperator.GE, typeof(MCOutSourceOrder))]
        public DateTime? EstablishDateS { get; set; }

        // 编制时间 用于查询 结束时间
        [EntityProperty("EstablishDate", PropertyOperator.LE, typeof(MCOutSourceOrder))]
        public DateTime? EstablishDateE { get; set; }

        // 编制人
        [EntityProperty("RealName", PropertyOperator.CONTAINS, typeof(UserInfo))]
        public string UName { get; set; }

        // 供货商
        [EntityProperty("CompName", PropertyOperator.CONTAINS, typeof(CompInfo))]
        public string OutCompanyName { get; set; }

        // 批准时间 用于查询 初始时间
        [EntityProperty("ApproveDate", PropertyOperator.GE, typeof(MCOutSourceOrder))]
        public DateTime? ApproveDateS { get; set; }

        // 批准时间 用于查询 结束时间
        [EntityProperty("ApproveDate", PropertyOperator.LE, typeof(MCOutSourceOrder))]
        public DateTime? ApproveDateE { get; set; }

        // 生产部门
        [EntityProperty("DepartmentID", PropertyOperator.EQUAL, typeof(MCOutSourceOrder))]
        public string DepartmentID { get; set; }

        // 外购单状态 当前状态
        [EntityProperty("OutOrderStatus", PropertyOperator.EQUAL, typeof(MCOutSourceOrder))]
        public string OutOrderStatus { get; set; }

        // 紧急状态
        [EntityProperty("UrgentStatus", PropertyOperator.EQUAL, typeof(MCOutSourceOrder))]
        public string UrgentStatus { get; set; }
    }

    /// <summary>
    /// 产品外购单一览画面的查询结果的视图Model类
    /// </summary>
    public class VM_PurchaseOrderListForTableShow
    {
        // 外购单号
        public string OutOrderID { get; set; }

        // 紧急状态
        public string UrgentStatus { get; set; }

        // 生产部门
        public string DeptName { get; set; }

        // 供货商
        public string CompName { get; set; }

        // 外购单状态 当前状态
        public string OutOrderStatus { get; set; }

        // 编制人
        public string EstablishUName { get; set; }

        // 编制时间
        public DateTime? EstablishDate { get; set; }

        // 批准时间
        public DateTime? ApproveDate { get; set; }

        // 备注
        public string Remarks { get; set; }

        // 当前状态
        public string OrderStatus { get; set; }
    }
}