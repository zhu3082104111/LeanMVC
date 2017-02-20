// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：IPickingListRepository.cs
// 文件功能描述：生产领料单信息 检索条件视图
// 
// 创建标识：代东泽 20131127
//
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;
namespace Model.Produce
{

    /// <summary>
    /// 代东泽 20131127
    /// </summary>
    public class VM_ProduceMaterRequestForSearch
    {
        public string ComeFromNo { get; set; }//来源单号

        //对应entityModel中的MaterReqNo属性，操作  为包含
        [EntityProperty("MaterReqNo", PropertyOperator.CONTAINS)]
        public string PickingNo { get; set; }//领料单号

        //对应entityModel中的RequestDate属性，操作 为大于等于
        [EntityProperty("RequestDate", PropertyOperator.GE)]
        public DateTime? TimeBegin { get; set; }

        //对应entityModel中的RequestDate属性，操作  为小于等于
        [EntityProperty("RequestDate", PropertyOperator.LE)]
        public DateTime? TimeEnd { get; set; }

        //对应entityModel中的DeptID属性，操作  为默认的 包含(contants)
        [EntityProperty("DeptID")]
        public string PickingUnit { get; set; }//领用部门


        public string Type { get; set; }//类型

        [EntityProperty("MaterReqType",PropertyOperator.EQUAL)]
        public string ComeFrom { get; set; }//领料来源
    }
    public class VM_ProduceMaterRequestForTableShow
    {

       /// <summary>
       /// 物料领用单号
       /// </summary>
        public string PickingNo { get; set; }
        /// <summary>
        /// 来源单号-流转卡id
        /// </summary>
        public string ComeFromNo { get; set; }
        /// <summary>
        /// 来源单号-总装大工票id
        /// </summary>
        public string ComeFromNoW { get; set; }
        /// <summary>
        /// 领料来源
        /// </summary>
        public string ComeFrom { get; set; }//领料来源
        /// <summary>
        /// 实领数量
        /// </summary>
        public decimal RealPickingCount { get; set; }
        /// <summary>
        /// //领用部门
        /// </summary>
        public string PickingUnit { get; set; }
        /// <summary>
        /// 用途
        /// </summary>
        public string Use { get; set; }//用途
        /// <summary>
        /// 领料人
        /// </summary>
        public string UsePerson { get; set; }
        /// <summary>
        /// 审核者
        /// </summary>
        public string Auditor { get; set; }
        /// <summary>
        /// 仓管员
        /// </summary>
        public string StoreManager { get; set; }
        /// <summary>
        /// 当前状态
        /// </summary>
        public string CurrentState { get; set; }//当前状态
        /// <summary>
        /// 领用时间
        /// </summary>
        public DateTime? PickingTime { get; set; }
    }

    public class VM_ProduceMaterDetailForDetailShow 
    {
        /// <summary>
        /// 仓管员
        /// </summary>
        public string WHManager { get; set; }
        /// <summary>
        /// 审核者
        /// </summary>
        public string Auditor { get; set; }
        /// <summary>
        /// 领料人
        /// </summary>
        public string Picker { get; set; }
        /// <summary>
        /// 物料领用单号
        /// </summary>
        public string PickingNo { get; set; }
        /// <summary>
        /// 领料单详细号
        /// </summary>
        public string MaterReqDetailNo { get; set; }
        /// <summary>
        /// 来源单号-流转卡id
        /// </summary>
        public string ComeFromNo { get; set; }
        /// <summary>
        /// 来源单号-总装大工票id
        /// </summary>
        public string ComeFromNoW { get; set; }

        /// <summary>
        /// 客户订单号
        /// </summary>
        public string ClnOdrID { set; get; }

        /// <summary>
        /// 客户订单明细
        /// </summary>
        public string ClnOdrDtl { set; get; }
        /// <summary>
        /// 输出品ID
        /// </summary>
        public string ExportID { set; get; }
        /// <summary>
        /// 零件号
        /// </summary>
        public string MaterialID { get; set; }
        /// <summary>
        /// 本次最大领用数量
        /// </summary>
        public decimal MaxPickingCount { get; set; }
        /// <summary>
        /// 实领数量
        /// </summary>
        public decimal RealPickingCount { get; set; }
        /// <summary>
        /// 请领数量
        /// </summary>
        public decimal PleasePickingCount { get; set; }
        /// <summary>
        /// 材料型号
        /// </summary>
        public string PartModel { get; set; }
        /// <summary>
        /// 材料名称
        /// </summary>
        public string PartName { get; set; }
        /// <summary>
        /// 规格型号
        /// </summary>
        public string PdtSpec { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        public string BthID { get; set; }
        /// <summary>
        /// 预约详细号
        /// </summary>
        public int OrdeBthDtailListID { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// //领用部门
        /// </summary>
        public string PickingUnit { get; set; }
        /// <summary>
        /// 领用时间
        /// </summary>
        public DateTime? PickingTime { get; set; }
        /// <summary>
        /// 领料来源
        /// </summary>
        public string ComeFrom { get; set; }//领料来源
        /// <summary>
        /// 仓库id
        /// </summary>
        public string WHID { get; set; }
        /// <summary>
        /// 用途描述
        /// </summary>
        public string Use { get;set;}
    }

    public class VM_ProduceMaterRequestForDetailShow 
    {
    
    
    }
}
