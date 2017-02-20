/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：VM_deliveryOrderList.cs
// 文件功能描述：
//          产品送货单一览画面的Model
//      
// 修改履历：2013/11/13 姬思楠 新建
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
    /// 查询条件的视图Model类
    /// </summary>
    public class VM_DeliveryOrderListForSearch
    {
        // 采购类别
        [EntityProperty("DeliveryOrderType", PropertyOperator.EQUAL, typeof(MCDeliveryOrder))]
        public string DeliveryOrderType { get; set; }

        // 订单号
        [EntityProperty("OrderNo", PropertyOperator.CONTAINS, typeof(MCDeliveryOrder))]
        public string OrderNo { get; set; }

        // 送货单号
        [EntityProperty("DeliveryOrderID", PropertyOperator.CONTAINS, typeof(MCDeliveryOrder))]
        public string DeliveryOrderID { get; set; }

        // 送货单状态
        [EntityProperty("DeliveryOrderStatus", PropertyOperator.EQUAL, typeof(MCDeliveryOrder))]
        public string DeliveryOrderStatus { get; set; }

        // 供货商
        [EntityProperty("CompName", PropertyOperator.CONTAINS, typeof(CompInfo))]
        public string DeliveryCompany { get; set; }

        // 送货人
        [EntityProperty("DeliveryUID", PropertyOperator.CONTAINS, typeof(MCDeliveryOrder))]
        public string DeliveryMan { get; set; }

        // 送货日期
        [EntityProperty("DeliveryDate", PropertyOperator.GE, typeof(MCDeliveryOrder))]
        public DateTime? DeliveryDateFrom { get; set; }

        // 送货日期
        [EntityProperty("DeliveryDate", PropertyOperator.LE, typeof(MCDeliveryOrder))]
        public DateTime? DeliveryDateTO { get; set; }

        // 生产部门
        [EntityProperty("DepartID", PropertyOperator.EQUAL, typeof(MCDeliveryOrder))]
        public string DepartID { get; set; }
    }
    #endregion

    #region 查询结果的视图Model类
    /// <summary>
    /// 查询结果的视图Model类
    /// </summary>
    public class VM_DeliveryOrderListForTableShow
    {

        //送货单号
        public string DeliveryOrderID { get; set; }

        //送货单区分
        public string DeliveryOrderType { get; set; }

        //订单号
        public string OrderNo { get; set; }

        //供货商名称
        public string DeliveryCompanyID { get; set; }

        // 送货单状态
        public string BatchID { get; set; }

        //送货人
        public string DeliveryUID { get; set; }

        // 送货日期
        public DateTime? DeliveryDate { get; set; }

        //审核人
        public string VerifyUID { get; set; }

        // 审核日期
        public DateTime? VerifyDate { get; set; }

        //检查员
        public string IspcUID { get; set; }

        //核价员
        public string PrccUID { get; set; }

        // 仓管员
        public string WhkpUID { get; set; }
    }
    #endregion

}