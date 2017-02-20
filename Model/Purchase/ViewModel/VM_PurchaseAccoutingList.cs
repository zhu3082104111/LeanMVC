using Extensions;
/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：VM_PurchaseAccoutingList.cs
// 文件功能描述：外购计划台账一览画面的Model
//          
//      
// 修改履历：2013/12/06 吴飚 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 查询条件的视图Model类
    /// </summary>
    public class VM_PurchaseAccoutingListForSearch
    {
        // 客户订单号
        public string CustomerOrder { get; set; }

        // 外购单号
        [EntityProperty("OutOrderID", PropertyOperator.CONTAINS, typeof(MCOutSourceOrderDetail))]
        public string OutOrderID { get; set; }

        // 物料编号
        public string MaterielNo { get; set; }

        // 物料名称
        public string MaterielName { get; set; }

        // 供货商
        [EntityProperty("CompName", PropertyOperator.CONTAINS, typeof(CompInfo))]
        public string CompName { get; set; }

        // 完成状态
        [EntityProperty("CompleteStatus", PropertyOperator.EQUAL, typeof(MCOutSourceOrderDetail))]
        public string Status { get; set; }

        // 紧急状态
        [EntityProperty("UrgentStatus", PropertyOperator.EQUAL, typeof(MCOutSourceOrder))]
        public string UrgentStatus { get; set; }
       
        // 交货日期
        [EntityProperty("DeliveryDate", PropertyOperator.GE, typeof(MCOutSourceOrderDetail))]
        public DateTime? DeliveryDateS { get; set; }

        // 交货日期
        [EntityProperty("DeliveryDate", PropertyOperator.LE, typeof(MCOutSourceOrderDetail))]
        public DateTime? DeliveryDateE { get; set; }
    }

    /// <summary>
    /// table的视图Model类
    /// </summary>
    public class VM_PurchaseAccoutingListForTableShow
    {
        // 紧急状态
        public string UrgentStatus { get; set; }

        // 外购单号
        public string OutOrderNo { get; set; }

        // 供货商名称
        public string CompName { get; set; }

        // 物料编号
        public string MaterialNo { get; set; }

        // 物料名称
        public string MaterialName { get; set; }

        // 订货数量
        public decimal OrderedQuantity { get; set; }
        
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
}