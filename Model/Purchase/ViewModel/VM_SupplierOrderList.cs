/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：VM_SupplierOrderList.cs
// 文件功能描述：
//            外协调度单的ViewModel
//      
// 创建标识：2013/10/31 廖齐玉 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;

namespace Model
{
    #region 外协单一览 + 数据显示VM_SupplierOrderList + 数据查询VM_SupplierOrderListForSearch

    /// <summary>
    /// 一览界面视图Model类
    /// </summary>
    public class VM_SupplierOrderList
    {
        // 外协加工调度单
        public string SupOrderID { set; get; }

        // 单据种类
        public string OrderType { set; get; }
 
        // 紧急状态
        public string UrgentStatus { set; get; }
        
        // 紧急状态CD
        public string SupOrderStatusCd { set; get; }

        // 当前状态
        public string SupOrderStatus { set; get; }

        // 生产部门
        public string DepartmentName { set; get; }

        // 调入单位
        public string InCompanyName { set; get; }

        // 制单人
        public string MarkName { set; get; }
        
        // 制单日期
        public DateTime? MarkDate { set; get; }

        // 生产主管
        public string PrdMngrName { set; get; }

        // 经办人
        public string OptrName { set; get; }

    }

    /// <summary>
    /// 一览界面查询ViewModel
    /// </summary>
    public class VM_SupplierOrderListForSearch
    {
        // 客户订单号
        public string CustomerOrderID{set;get;}

        // 调度单种类      
        public string OrderType{set;get;}

        // 调度单号
        [EntityProperty("SupOrderID", PropertyOperator.CONTAINS)]
        public string SupOrderID { set; get; }

        // 紧急状态
        public string UrgentStatus { set; get; }

        // 当前状态
        public string SupOrderStatus { set; get; }

        // 制单人
        public string MarkName { set; get; }
        
        // 开始日期
        [EntityProperty("MarkSignDate", PropertyOperator.GE)]
        public DateTime? CreatDateStart { set; get; }

        // 结束日期
        [EntityProperty("MarkSignDate", PropertyOperator.LE)]
        public DateTime? CreatDateEnd { set; get; }

        // 调入单位
        public string InCompanyName { set; get; }

        // 生产主管
        public string PrdMngrName { set; get; }

        // 所属部门
        public string DepartmentName { set; get; }
         
    } 
    #endregion

    #region 外协单 + 数据显示VM_SupplierOrder + 单信息VM_SupplierOrderInfor

    /// <summary>
    /// 外协加工调度单的视图Model类
     /// </summary>
    public class VM_SupplierOrder
    {
        // 行数
        public string RowIndex { get; set; }
        // 编码
        public string SupOrderID { set; get; }
        // 客户订单号
        public string CustomerOrderID { set; get; }
        // 产品零件ID
        public string ProductPartID { set; get; }
        // 产品略称
        public string ProductAbbrev { set; get; }
        // 产品Id
        public string ProdDtID { set; get; }
        // 产品名称
        public string ProductName { set; get; }
        // 材料规格及型号
        public string MaterialsSpecReq { set; get; }
        // 加工工艺
        public string PdProcDtID { set; get; }
        // 单位
        public string Unit { set; get; }
        // 数量
        public decimal ReqQty { set; get; }
        // 单价
        public decimal PriceUp { set; get; }
        // 估价
        public decimal Evaluate { set; get; }
        // 交货日期
        public DateTime  DlyDate { set; get; }
        // 备注
        public string Remarks { set; get; }
    }

    /// <summary>
    /// 调度单的相关信息ViewModel
     /// </summary>
    public class VM_SupplierOrderInfor
    {
        //调度单号
        public string SupOdrId { set; get; }
        // 状态
        public string OrderStatus { set; get; }
        // 紧急状态
        public string UrgentStatus { set; get; }
        // 客户定单号
        public string ClientOrderID { set; get; }
        // 所属部门
        public string Department { set; get; }
        // 调度单种类
        public string OrderType { set; get; }
        // 调入单位
        public string IncompId { set; get; }
        // 调出单位
        public string OutcompId { set; get; }
        // 生产主管
        public string PrdMngrName { set; get; }
        // 生产主管签字日期
        public DateTime? PrdMngrSignDate { set; get; }
        // 制单人
        public string MarkName { set; get; }
        // 制单日期
        public DateTime? MarkSignDate { set; get; }
        // 经办人
        public string OptrName { set; get; }
        // 经办日期
        public DateTime? OptrSignDate { set; get; }
        // 调度单明细信息
        public List<VM_SupplierOrder> OrderList { set; get; }
    }

    #endregion

    public class VM_SupplierOrderNewData 
    {
        // 调度单号
        public string SupOdrID { set; get; }

        // 状态
        public string OrderStatus { set; get; }

        // 紧急状态
        public string UrgentStatus { set; get; }

        // 客户定单号
        public string ClientOrderID { set; get; }

        // 客户订单明细号
        public string ClientOrderDetailID { set; get; }

        // 所属部门
        public string Department { set; get; }

        // 调度单种类
        public string OrderType { set; get; }

        // 调入单位
        public string IncompId { set; get; }

        // 生产主管
        public string PrdMngrName { set; get; }

        // 生产主管签字日期
        public DateTime? PrdMngrSignDate { set; get; }

        // 制单人
        public string MarkName { set; get; }

        // 制单日期
        public DateTime? MarkSignDate { set; get; }

        // 经办人
        public string OptrName { set; get; }

        // 经办日期
        public DateTime? OptrSignDate { set; get; }


    }
}
