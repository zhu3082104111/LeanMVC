/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：VM_PWaitingWarehouseList.cs
// 文件功能描述：成品交仓画面的视图model集
//     
// 修改履历：2013/12/27 潘军 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;

namespace Model
{
    #region 查询条件字段
    /// <summary>
    /// 成品交仓一览画面查询条件
    /// </summary>
    public class VM_PWaitingWarehouseListForSearch
    {
        /// <summary>
        /// 生产单元 
        /// </summary>
        [EntityProperty("DepartId", PropertyOperator.CONTAINS)]
        public string txtProductionUnits{get;set;}

        /// <summary>
        /// 调度单号<总装调度单表>
        /// </summary>
        [EntityProperty("AssemblyDispatchID", PropertyOperator.CONTAINS)]
        public string txtSchedulOrder{ get; set; }

        /// <summary>
        /// 客户订单号<总装调度单表> 
        /// </summary>
        [EntityProperty("CustomerOrderNum", PropertyOperator.CONTAINS)]
        public string txtCustomerOrderNum { get; set; }

        /// <summary>
        ///产品型号 <成品信息表>
        /// </summary>
        [EntityProperty("ProdAbbrev", PropertyOperator.CONTAINS,typeof(ProdInfo))]
        public string ProductType { get; set; }
    }
    #endregion
    #region 表格字段
    /// <summary>
    /// 成品交仓一览画面详细数据表显示
    /// </summary>
    public class VM_PWaitingWarehouseListForTableShow
    {
        /// <summary>
        /// 总装调度单号
        /// </summary>
        public string AssemblyOrderNo { get; set; }
        /// <summary>
        /// 客户订单号
        /// </summary>
        public string CustomerOrderID { get; set; }
        /// <summary>
        /// 客户订单明细
        /// </summary>
        public string ClientOrderDetail { get; set; }
        /// <summary>
        /// 产品型号
        /// </summary>
        public string ProductType { get; set; }
        /// <summary>
        /// 可交仓数量
        /// </summary>
        public decimal WarehouseQuantityAvailable { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string Specification { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 每箱数量
        /// </summary>
        public decimal Packing { get; set; }
        /// <summary>
        /// 箱数
        /// </summary>
        public decimal Cartons { get; set; }
        /// <summary>
        /// 零头
        /// </summary>
        public decimal Odd { get; set; }
        /// <summary>
        /// 交仓单号
        /// </summary>
        public string WarehouseLineNO { get; set; }
        /// <summary>
        /// 班组
        /// </summary>
        public string TeamIDs { get; set; }
        /// <summary>
        /// 成品交仓单的’箱数‘
        /// </summary>
        public decimal BoxQuantitys { get; set; }
        /// <summary>
        /// 成品交仓单的‘零头’
        /// </summary>
        public decimal RemianQuantitys { get; set; }
        /// <summary>
        /// 交仓单号
        /// </summary>
        public string WarehouseLineNO_NO { get; set; }
        /// <summary>
        /// 生产部门
        /// </summary>
        public string DepartmentID { get; set; }
    }
    #endregion
}
