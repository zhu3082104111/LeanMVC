/*****************************************************************************/
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。
//
// 文件名：VM_DeliveryOrder.cs
// 文件功能描述：送货单画面的Model
//      
// 修改履历：2013/12/10 刘云 新建
/*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    #region 查询结果的视图Model类
    /// <summary>
    /// 查询结果的视图Model类
    /// </summary>
    public class VM_DeliveryOrderForTableShow
    {
        public string RowIndex { get; set; }//存放第几行  行数
        public string OrderNo { get; set; }//订单号  采购计划单号
        public string MaterielID { get; set; }//物料编码
        public string Materiel { get; set; }//物料ID 隐藏项
        public string MaterielName { get; set; }//物料名称
        public string MaterialsSpec { get; set; }//规格
        public string WarehouseID { get; set; }// 仓库编号
        public string Unit { get; set; }//单位
        public string UnitID { get; set; }//单位id 隐藏项
        public string Quantity { get; set; }//数量
        public string PriceWithTax { get; set; }//含税价格
        public string CkPriceWithTax { get; set; }//核实含税价格
        public string ActualQuantity { get; set; }//实收数量
        public string Remarks { get; set; }//备注
        public string InnumQuantity { get; set; }// 每件数量
        public string Num { get; set; }// 件数
        public string Scrap { get; set; }// 零头
        public string DeliveryCompanyID { get; set; }//送货单位
    }
    #endregion

    #region 查询所有的视图Model类
    /// <summary>
    /// 查询所有的视图Model类
    /// </summary>
    public class VM_DeliveryOrderForShow
    {
        public string OrderNo { get; set; }//采购计划单号
        public string OrderNumber { get; set; }//隐藏 采购计划单号
        public DateTime DeliveryDate { get; set; }// 送货日期
        public string DeliveryOrderID { get; set; }//送货单号
        public string DeliveryCompanyName { get; set; }//送货单位名称
        public string DeliveryCompanyID { get; set; }//送货单位
        public string DeliveryUID { get; set; }// 送货人
        public string TelNo { get; set; }//联系电话
        public List<VM_DeliveryOrderForTableShow> DeliveryList { get; set; }//tableShow的list
    }
    #endregion

}
