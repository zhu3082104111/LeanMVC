/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：VM_SupplierAccouting.cs
// 文件功能描述：
//          外协计划台账的视图Model
//      
// 修改履历：2013/12/06 吴飚 新建
/*****************************************************************************/

using Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

    /// <summary>
    /// 外协计划台帐一览画面的查询条件的视图Model类
    /// </summary>
    public class VM_SupplierAccoutingList4Search
    {
        // 客户订单号
        public string CustomerOrder { get; set; }

        // 外协单号
        [EntityProperty("SupOrderID", PropertyOperator.CONTAINS, typeof(MCSupplierOrderDetail))]
        public string SupOrderID { get; set; }

        // 物料编号
        public string MaterielNo { get; set; }

        // 物料名称
        public string MaterielName { get; set; }

        // 外协单位
        [EntityProperty("CompName", PropertyOperator.CONTAINS, typeof(CompInfo))]
        public string SupCompName { get; set; }

        // 完成状态
        [EntityProperty("CompleteStatus", PropertyOperator.EQUAL, typeof(MCSupplierOrderDetail))]
        public string Status { get; set; }

        // 紧急状态
        [EntityProperty("UrgentStatus", PropertyOperator.EQUAL, typeof(MCSupplierOrder))]
        public string UrgentStatus { get; set; }

        // 交货日期
        [EntityProperty("DeliveryDate", PropertyOperator.GE, typeof(MCSupplierOrderDetail))]
        public DateTime? DeliveryDateS { get; set; }

        // 交货日期
        [EntityProperty("DeliveryDate", PropertyOperator.LE, typeof(MCSupplierOrderDetail))]
        public DateTime? DeliveryDateE { get; set; }
    }

    /// <summary>
    /// 外协计划台帐一览画面的列表的视图Model类
    /// </summary>
    public class VM_SupplierAccoutingList4Table
    {
        // 紧急状态
        public string UrgentStatus { get; set; }

        // 外协单号
        public string SupOrderNo { get; set; }

        // 外协单位
        public string SupCompName { get; set; }

        // 物料编号
        public string MaterialNo { get; set; }

        // 物料名称
        public string MaterialName { get; set; }

        // 订货数量
        public decimal PlanQuantity { get; set; }

        // 加工工艺
        public string ProcessID { get; set; }

        // 材料规格及要求
        public string MaterialsSpecReq { get; set; }

        // 交货日期
        public DateTime? DeliveryDate { get; set; }

        // 到货累计
        public decimal ArrivalQuantity { get; set; }

        // 订单差额
        public decimal MarginQuantity { get; set; }

        // 完成状态显示名
        public string CompletStatus { get; set; }

        // 完成状态Code
        public string CompletStatusCd { get; set; }

        // 备注
        public string Remarks { get; set; }

        // 背景色flag
        public string BGColorFlag { get; set; }
    }

    /// <summary>
    /// 外协计划台账详细画面的订单信息的视图Model类
    /// </summary>
    public class VM_SupplierAccoutingDetailInfo
    {
        // 外协单号
        public string SupOrderNo { set; get; }

        // 紧急状态
        public string UrgentStatus { set; get; }

        // 下单日期
        public DateTime? MarkDate { set;get; }

        // 生产部门
        public string Department { set; get; }

        // 外协单位
        public string SupCompName { set; get; }

    }

    /// <summary>
    /// 外协计划台账详细画面的TableShow 视图Model类
    /// </summary>
    public class VM_SupplierAccoutingDetail4Table
    {
        // 物料编号
        public string MaterialNo { get; set; }

        // 物料名称
        public string MaterialName { get; set; }

        // 材料规格及要求
        public string MaterialsSpecReq { get; set; }

        // 加工工艺
        public string ProcessingNo { set; get; }

        // 订货数量
        public decimal OrderQuantity { get; set; }

        // 交货日期
        public DateTime? OrderDate { get; set; }

        // 送货单号
        public string DeliveryOrderNo { set; get; }

        // 送货日期
        public DateTime? DeliveryDate { set; get; }

        // 送货数量
        public decimal? DeliveryQuantity { set; get; }

        // 质检单号
        public string QCOrderNo { set; get; }

        // 质检日期
        public DateTime? QCDate { set; get; }

        // 质检结果
        public string QCResult { set; get; }

        // 入库单号
        public string StorageOrderNo { set; get; }

        // 入库日期
        public DateTime? StorageDate { set; get; }

        // 入库数量
        public decimal? StorageQuantity { set; get; }
    }
}