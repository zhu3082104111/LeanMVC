/*----------------------------------------------------------------
// Copyright (C) 2013 北京思元软件有限公司
// 版权所有。 
//
// 文件名：VM_BatchStock.cs
// 文件功能描述：产品零件锁存功能中的批次检索显示结构
// 
// 创建标识：20131210 梁龙飞
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 零件已锁库、未锁库混合检索条件
    /// C：梁龙飞
    /// </summary>
    public class VM_MatBtchStockSearch
    {

        /// <summary>
        /// 单据类型
        /// </summary>
        public string BillType { set; get; }

        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchID { set; get; }

        /// <summary>
        /// 仓库编号
        /// </summary>
        public string WarehouseID { set; get; }

        /// <summary>
        /// 客户订单号
        /// </summary>
        public string ClientOrderID { get; set; }

        /// <summary>
        /// 客户订单明细
        /// </summary>
        public string OrderDetail { get; set; }

        /// <summary>
        /// 零件ID
        /// </summary>
        public string MaterialID { set; get; }

        /// <summary>
        /// 规格型号
        /// </summary>
        public string Specification { set; get; }

        /// <summary>
        /// 需要返回的总行数
        /// </summary>
        public int Rows { get; set; }

        /// <summary>
        /// 需要总数量
        /// </summary>
        public decimal QutityInNeed { get; set; }


    }

    /// <summary>
    /// enum:批次来源
    /// </summary>
    public struct BatchOrigin
    {
        public static string Default = "0";
        public static string Locked="1";
        public static string Unlocked = "2";

    }

    /// <summary>
    /// 仓库批次显示视图,单配一直存在，正常品只有指定型号时显示
    /// 在订单排产界面，显示已锁(预约信息)和未锁批次（排除预约已占用的批次号）的混合结果（具有相同的数据结构）
    /// C：梁龙飞
    /// </summary>
    public class VM_LockReserveShow
    {
        //public BatchOrigin getFlagFromString(string
        public VM_LockReserveShow()
        {
            OriginFlag = BatchOrigin.Default;
        }
        /// <summary>
        /// 来源标记， 已锁和未锁
        /// </summary>
        public string OriginFlag { get; set; }

        /// <summary>
        /// 仓库编号
        /// </summary>
        public string WhID { get; set; }

        /// <summary>
        /// 客户订单号
        /// </summary>
        public string ClientOrderID { get; set; }

        /// <summary>
        /// 客户订单明细
        /// </summary>
        public string OrderDetail { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        public string ProductID { get; set; }

        /// <summary>
        /// 零件ID
        /// </summary>
        public string MaterialID { get; set; }

        /// <summary>
        /// 批次号：显示
        /// </summary>
        public string BthID { set; get; }

        //----------------------以上为识别字段

        /// <summary>
        /// 可用数量：显示
        /// </summary>
        public decimal TotAvailable { set; get; }

        /// <summary>
        /// 让步说明 or 规格要求：显示
        /// </summary>
        public string Specification { set; get; }

        /// <summary>
        /// 让步区分号
        /// </summary>
        public string GiveinCatID { set; get; }

        /// <summary>
        /// 锁存数量：显示
        /// </summary>
        public decimal OrderQuantity { set; get; }

        /// <summary>
        /// 订单中对此零件需求的总量(不包括单配数量)
        /// </summary>
        public decimal TotNeeded { set; get; }

    }
    
}
