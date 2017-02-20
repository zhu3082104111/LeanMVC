/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：VM_StoreFinInStoreForSearch.cs
// 文件功能描述：
//            内部成品库入库相关页面的ViewModel
//      
// 创建标识：2013/11/23 陈健 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;

namespace Model.Store
{

    #region 待入库一览
    /// <summary>
    /// 待入库一览画面查询ViewModel
    /// </summary>
    public class VM_StoreFinInStoreForSearch
        {
            [EntityProperty("ProductWarehouseID", PropertyOperator.EQUAL)]
            public string ProductWarehouseID { get; set; }  //成品交仓单号
        }
    /// <summary>
    /// 待入库一览画面数据显示ViewModel
    /// </summary>
    public class VM_StoreFinInStoreForTableShow
        {
            public string ProductWarehouseID { get; set; }  //成品交仓单号
            public DateTime? WarehouseDT { get; set; }  //交仓日期
        }
    #endregion

    #region 入库履历一览
    /// <summary>
    /// 入库履历一览画面查询ViewModel
    /// </summary>
    public class VM_StoreFinInRecordForSearch
        {
            [EntityProperty("PlanID", PropertyOperator.CONTAINS)]
            public string PlanID { get; set; }  //成品交仓单号
           [EntityProperty("PartAbbrevi", PropertyOperator.CONTAINS)]
            public string PartAbbrevi { get; set; }  //产品ID
        [EntityProperty("InDate", PropertyOperator.GE, typeof(FinInRecord))]
            public DateTime? InDate1 { get; set; }  //入库日期
        [EntityProperty("InDate", PropertyOperator.LE, typeof(FinInRecord))]
            public DateTime? InDate2 { get; set; }  //入库日期
        [EntityProperty("InMoveCls", PropertyOperator.EQUAL)]
            public string InMoveCls { get; set; }  //入库移库
      
            public string ClientOrderID { get; set; }  //客户订单号
        }
    /// <summary>
    /// 入库履历一览画面数据显示ViewModel
    /// </summary>
    public class VM_StoreFinInRecordForTableShow
        {
            public string PlanID { get; set; }  //成品交仓单号
            public string BatchID { get; set; }  //批次号
            public string WareHouseID { get; set; }  //仓库编号
            public string WareHousePositionID { get; set; }  //仓位
            public string InMoveCls { get; set; }  //入库移动区分
            public DateTime InDate { get; set; }  //入库日期
            public string WareHouseKpID { get; set; }  //仓管员ID
            public string Remarks { get; set; }  //备注
        }
    #endregion

    #region 入库履历详细
    /// <summary>
    /// 入库履历详细画面数据显示ViewModel
    /// </summary>
    public class VM_StoreFinInRecordForDetailShow
        {
            public string ProductWarehouseID { get; set; }  //成品交仓单号
            public string BatchID { get; set; }  //批次号
            public string WareHouseID { get; set; }  //仓库编号
            public string InMoveCls { get; set; }  //入库移动区分
        
            public string PlanID { get; set; }  //计划单号
            public string ProductCheckID { get; set; }  //检验单号

            public string TeamID { get; set; }  //装配小组
            public string OrderProductID { get; set; }  //物料编号(略称)
            public string ProductName { get; set; }  //产成品名称
            public string ProductSpecification { get; set; }  //规格
            //public string Unit { get; set; }  //单位
            public decimal QualifiedQuantity { get; set; }  //合格数量
            public decimal EachBoxQuantity { get; set; }  //每箱数量
            public decimal BoxQuantity { get; set; }  //箱数
            public decimal RemianQuantity { get; set; }  //零头
            public string GiclsProduct { get; set; }  //是否使用过让步品
            public string Remarks { get; set; }  //备注

            public DateTime InDate { get; set; }  //入库日期
            public string WareHouseKpID { get; set; }  //仓管员ID
            public string MRemarks { get; set; }  //备注

            public string ClientOrderID { get; set; }//客户订单号
            public string ClientOrderDetail { get; set; }//客户订单明细

            public string PartID { get; set; }  //物料ID
        }

#endregion

}
