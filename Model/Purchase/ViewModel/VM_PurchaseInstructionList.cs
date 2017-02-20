/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：VM_PurchaseInstructionList.cs
// 文件功能描述：
//          外购计划一览画面的视图Model
//          
// 修改履历：2013/11/18 陈阵 新建
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
    /// 外购计划一览画面的查询条件的视图Model类
    /// </summary>
    public class VM_PurchaseInstructionList4Search
    {
        // 客户订单号 
        public string CustOrderNo { get; set; }

        // 产品型号
        [EntityProperty("ProdName", PropertyOperator.CONTAINS, typeof(ProdInfo))]
        public string PrdtType { get; set; }

        // 物料编号
        public string MaterialNo { get; set; }

        // 物料名称
        public string MaterialName { get; set; }

        // 供货商
        [EntityProperty("CompName", PropertyOperator.CONTAINS, typeof(CompInfo))]
        public string CompName { get; set; }

        // 提供日期（开始）
        [EntityProperty("ProvideDate", PropertyOperator.GE, typeof(MaterialDecompose))]
        public DateTime? SupplyDateS { set; get; }

        // 提供日期（结束）
        [EntityProperty("ProvideDate", PropertyOperator.LE, typeof(MaterialDecompose))]
        public DateTime? SupplyDateE { set; get; }

        // 排产状态
        [EntityProperty("ReceiveFlag", PropertyOperator.EQUAL, typeof(PurchaseInstruction))]
        public string ScheduleStatus { get; set; }

        // 生产部门
        [EntityProperty("DepartmentID", PropertyOperator.EQUAL, typeof(PurchaseInstruction))]
        public string DeptID { get; set; }
    }

    /// <summary>
    /// 外购计划一览画面的显示列表的视图Model类
    /// </summary>
    public class VM_PurchaseInstructionList4Table
    {
        // 部门名称
        public string DeptName { get; set; }

        // 产品零件ID
        public string ProductPartID { get; set; }

        // 客户订单号
        public string CustOrderNo { get; set; }

        // 产品型号
        public string PrdtType { get; set; }

        // 物料编号
        public string MaterialNo { get; set; }

        // 物料名称
        public string MaterialName { get; set; }

        // 材料规格和要求
        public string MaterialsSpecReq { get; set; }

        // 计划数量
        public decimal PlanQuantity { get; set; }

        // 待购数量
        public decimal WaitingQuantity { get; set; }

        // 开始日
        public DateTime? PlanDateS { set; get; }

        // 结束日
        public DateTime? PlanDateE { set; get; }

        // 外购计划单号
        public IEnumerable<VM_OutSourceOrderNo> OutOrderList { get; set; }
    }

    /// <summary>
    /// 外购单号的视图Model类
    /// </summary>
    public class VM_OutSourceOrderNo
    {
        // 外购计划单号
        public string OutOrderID { get; set; }
    }
}
