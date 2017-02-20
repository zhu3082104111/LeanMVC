/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：VM_SupplierInstructionList.cs
// 文件功能描述：
//          外协计划一览画面的视图Model
//      
// 创建标识：2013/11/13 宋彬磊 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;

namespace Model
{

    #region 查询条件的视图Model类
    /// <summary>
    /// 外协计划一览画面的查询条件的视图Model类
    /// </summary>
    public class VM_SupplierInstructionList4Search
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

        // 外协单位
        [EntityProperty("CompName", PropertyOperator.CONTAINS, typeof(CompInfo))]
        public string CompName { get; set; }

        // 提供日期（开始）
        [EntityProperty("StartDate", PropertyOperator.GE, typeof(MaterialDecompose))]
        public DateTime? SupplyDateS { get; set; }

        // 提供日期（结束）
        [EntityProperty("ProvideDate", PropertyOperator.LE, typeof(MaterialDecompose))]
        public DateTime? SupplyDateE { get; set; }

        // 生产部门
        [EntityProperty("DepartId", PropertyOperator.CONTAINS, typeof(ProdInfo))]
        public string DeptID { get; set; }
        
        // 排产状态
        [EntityProperty("ReceiveFlag", PropertyOperator.CONTAINS, typeof(AssistInstruction))]
        public string SchejuleStatus { get; set; }
    }
    #endregion

    #region 查询结果的视图Model类
    /// <summary>
    /// 外协计划一览画面的显示列表的视图Model类
    /// </summary>
    public class VM_SupplierInstructionList4Table
    {
        // 客户订单号
        public string CustOrderNo { get; set; }

        // 外协需求量
        public decimal WaitingQuantity { get; set; }

        // 产品零件ID 隐藏
        public string ProductPartID { get; set; }

        // 产品型号
        public string PrdtType { get; set; }

        // 物料编号
        public string MaterialNo { get; set; }

        // 物料名称
        public string MaterialName { get; set; }

        // 材料规格和要求
        public string MaterialsSpecReq { get; set; }

        // 本道工序名称
        public string PdProcName { get; set; }

        // 计划数量
        public Decimal PlanQuantity { get; set; }

        // 预排产计划-开始日
        public DateTime? PlanDateS { get; set; }

        // 预排产计划-结束日
        public DateTime? PlanDateE { get; set; }

        // 生产部门
        public string DeptID { get; set; }

        // 外协加工单号
        public IEnumerable<VM_SupplierOrderNo> SupplierOrderList { get; set; }
    }
    #endregion

    #region 外协加工单号的视图Model类
    /// <summary>
    /// 外协加工单号的视图Model类
    /// </summary>
    public class VM_SupplierOrderNo
    {
        // 外协加工单号
        public string SuppOrderID { get; set; }
    }
    #endregion
}
